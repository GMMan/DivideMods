using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using HarmonyLib;

namespace Divide.PlayerInvincibility
{
    [HarmonyPatch(typeof(PlayerCharacterController), nameof(PlayerCharacterController.GetHit))]
    internal static class PlayerCharacterControllerPatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            return new CodeMatcher(instructions, generator)
            // After:
            // PlayerStateVariables.currentHealth -= ...
            .MatchEndForward(
                new CodeMatch(i => i.opcode == OpCodes.Stsfld && ((FieldInfo)i.operand).Name == "currentHealth")
            ).Repeat(cm => {
                cm.Advance(1);
                // Insert:
                // if (PlayerStateVariables.currentHealth < 1) PlayerStateVariables.currentHealth = 1;
                cm.CreateLabel(out var falseLabel);
                cm.InsertAndAdvance(
                    CodeInstruction.LoadField(typeof(PlayerStateVariables), "currentHealth"),
                    new CodeInstruction(OpCodes.Ldc_I4_0),
                    new CodeInstruction(OpCodes.Bgt_S, falseLabel),
                    new CodeInstruction(OpCodes.Ldc_I4_1),
                    CodeInstruction.StoreField(typeof(PlayerStateVariables), "currentHealth")
                );
            })
            .InstructionEnumeration();
        }
    }
}
