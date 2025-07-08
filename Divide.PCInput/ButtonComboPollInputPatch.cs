using HarmonyLib;
using Rewired;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Divide.PCInput
{
    [HarmonyPatch(typeof(ButtonCombo), "PollInput")]
    internal static class ButtonComboPollInputPatch
    {
        static void Postfix(ref bool ___m_wasAnyButtonDown)
        {
            var player = PlayerStateVariables.CurrentPlayer;
            if (player != null) {
                ___m_wasAnyButtonDown = ___m_wasAnyButtonDown || player.GetButtonDown(ActionIds.ACTION_ID_CUSTOM_LEFT_STICK_CLICK)
                    || player.GetButtonDown(ActionIds.ACTION_ID_CUSTOM_RIGHT_STICK_CLICK);
            }
        }
    }
}
