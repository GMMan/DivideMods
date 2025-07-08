using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace Divide.PCInput
{
    [HarmonyPatch(typeof(ButtonCombo), "LateUpdate")]
    internal static class ButtonComboLateUpdatePatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return new CodeMatcher(instructions)
            .MatchStartForward(
                new CodeMatch(CodeInstruction.LoadField(typeof(PlayerStateVariables), nameof(PlayerStateVariables.m_leftStickComboButton))),
                new CodeMatch(CodeInstruction.LoadField(typeof(PlayerStateVariables), nameof(PlayerStateVariables.m_rightStickComboButton))),
                new CodeMatch(CodeInstruction.LoadField(typeof(PlayerStateVariables), nameof(PlayerStateVariables.m_MovementVector))),
                new CodeMatch(CodeInstruction.LoadField(typeof(PlayerStateVariables), nameof(PlayerStateVariables.m_RotationVector))),
                new CodeMatch(OpCodes.Callvirt, AccessTools.Method(typeof(StickAndClickComboData), nameof(StickAndClickComboData.EvaluateInput)))
            )
            .RemoveInstructions(5)
            .InsertAndAdvance(
                CodeInstruction.Call(typeof(InputLogic), nameof(InputLogic.EvaluateSynput))
            )
            .InstructionEnumeration();
        }
    }
}
