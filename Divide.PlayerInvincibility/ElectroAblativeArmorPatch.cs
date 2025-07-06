using ETS.Weapons;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Divide.PlayerInvincibility
{
    [HarmonyPatch(typeof(ElectroAblativeArmor), nameof(ElectroAblativeArmor.AbsorbDamage))]
    internal static class ElectroAblativeArmorPatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            return new CodeMatcher(instructions, generator)
            // After:
            // ... += dmgAmnt * ...
            // dmgAmnt = 0;
            .MatchEndForward(
                new CodeMatch(OpCodes.Mul),
                new CodeMatch(OpCodes.Add),
                new CodeMatch(i => i.opcode == OpCodes.Stfld && ((FieldInfo)i.operand).Name == nameof(AblativeArmorSlot.Health)),
                new CodeMatch(OpCodes.Ldc_I4_0),
                new CodeMatch(OpCodes.Stloc_0)
            )
            .Advance(1)
            // Insert:
            // if (m_Slots[i - 1].Health < 1 && SingletonBehaviour<GameManager>.Instance.Player.CurrentArmor == this) {
            //     m_Slots[i - 1].Health = 1;
            //     dmgAmnt = 0;
            // }
            .CreateLabel(out var falseLabel)
            .InsertAndAdvance(
                new CodeInstruction(OpCodes.Ldarg_0),
                CodeInstruction.LoadField(typeof(ElectroAblativeArmor), "m_Slots"),
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Ldc_I4_1),
                new CodeInstruction(OpCodes.Sub),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(List<AblativeArmorSlot>), "get_Item")),
                CodeInstruction.LoadField(typeof(AblativeArmorSlot), nameof(AblativeArmorSlot.Health)),
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Bgt_S, falseLabel),
                CodeInstruction.Call(typeof(SingletonBehaviour<GameManager>), "get_Instance"),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(GameManager), "get_Player")),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(PlayerCharacterController), "get_CurrentArmor")),
                new CodeInstruction(OpCodes.Ldarg_0),
                CodeInstruction.Call(typeof(UnityEngine.Object), "op_Equality"),
                new CodeInstruction(OpCodes.Brfalse_S, falseLabel),
                new CodeInstruction(OpCodes.Ldarg_0),
                CodeInstruction.LoadField(typeof(ElectroAblativeArmor), "m_Slots"),
                new CodeInstruction(OpCodes.Ldloc_1),
                new CodeInstruction(OpCodes.Ldc_I4_1),
                new CodeInstruction(OpCodes.Sub),
                new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeof(List<AblativeArmorSlot>), "get_Item")),
                new CodeInstruction(OpCodes.Ldc_I4_1),
                CodeInstruction.StoreField(typeof(AblativeArmorSlot), nameof(AblativeArmorSlot.Health)),
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Stloc_0)
            )
            .InstructionEnumeration();
        }
    }
}
