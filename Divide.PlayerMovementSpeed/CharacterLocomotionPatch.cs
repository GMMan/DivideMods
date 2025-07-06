using ETS.AI;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using UnityEngine;

namespace Divide.PlayerMovementSpeed
{
    [HarmonyPatch(typeof(CharacterLocomotion), "Awake")]
    [HarmonyDebug]
    internal static class CharacterLocomotionPatch
    {
        const float MULTIPLIER = 2.5f;

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            return new CodeMatcher(instructions, generator)
            // After:
            // LookForward();
            .MatchEndForward(
                new CodeMatch(OpCodes.Call, AccessTools.Method(typeof(CharacterLocomotion), nameof(CharacterLocomotion.LookForward)))
            )
            .Advance(1)
            // Insert:
            // if (GetComponent<PlayerCharacterController>() != null) {
            //     m_walkSpeed *= MULTIPLIER;
            //     m_runSpeed *= MULTIPLIER;
            //     m_aimingCombatSpeed *= MULTIPLIER;
            //     m_nonAimingCombatSpeed *= MULTIPLIER;
            // }
            .CreateLabel(out var falseLabel)
            .InsertAndAdvance(
                new CodeInstruction(OpCodes.Ldarg_0),
                CodeInstruction.Call(typeof(Component), nameof(Component.GetComponent), generics: new Type[] { typeof(PlayerCharacterController) }),
                new CodeInstruction(OpCodes.Ldnull),
                CodeInstruction.Call(typeof(UnityEngine.Object), "op_Inequality"),
                new CodeInstruction(OpCodes.Brfalse_S, falseLabel),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldarg_0),
                CodeInstruction.LoadField(typeof(CharacterLocomotion), "m_walkSpeed"),
                new CodeInstruction(OpCodes.Ldc_R4, MULTIPLIER),
                new CodeInstruction(OpCodes.Mul),
                CodeInstruction.StoreField(typeof(CharacterLocomotion), "m_walkSpeed"),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldarg_0),
                CodeInstruction.LoadField(typeof(CharacterLocomotion), "m_runSpeed"),
                new CodeInstruction(OpCodes.Ldc_R4, MULTIPLIER),
                new CodeInstruction(OpCodes.Mul),
                CodeInstruction.StoreField(typeof(CharacterLocomotion), "m_runSpeed"),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldarg_0),
                CodeInstruction.LoadField(typeof(CharacterLocomotion), "m_aimingCombatSpeed"),
                new CodeInstruction(OpCodes.Ldc_R4, MULTIPLIER),
                new CodeInstruction(OpCodes.Mul),
                CodeInstruction.StoreField(typeof(CharacterLocomotion), "m_aimingCombatSpeed"),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldarg_0),
                CodeInstruction.LoadField(typeof(CharacterLocomotion), "m_nonAimingCombatSpeed"),
                new CodeInstruction(OpCodes.Ldc_R4, MULTIPLIER),
                new CodeInstruction(OpCodes.Mul),
                CodeInstruction.StoreField(typeof(CharacterLocomotion), "m_nonAimingCombatSpeed")
            )
            .InstructionEnumeration();
        }
    }
}
