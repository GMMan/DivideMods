using ETS.UI;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace Divide.PCInput
{
    [HarmonyPatch(typeof(UISolusInput), "LateUpdate")]
    internal static class UISolusInputLateUpdatePatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codeMatcher = new CodeMatcher(instructions);
            codeMatcher.MatchStartForward(
                new CodeMatch(CodeInstruction.LoadField(typeof(PlayerStateVariables), nameof(PlayerStateVariables.m_leftStickComboButton)))
            )
            .Advance(1);
            var label = (Label)codeMatcher.Operand;
            codeMatcher.Advance(1)
            .InsertAndAdvance(
                CodeInstruction.Call(typeof(InputLogic), "get_SynputLeftStickClicked"),
                new CodeInstruction(OpCodes.Brtrue, label),
                CodeInstruction.Call(typeof(InputLogic), "get_SynputRightStickClicked"),
                new CodeInstruction(OpCodes.Brtrue, label)
            )
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
            );
            return codeMatcher.InstructionEnumeration();
        }
    }
}
