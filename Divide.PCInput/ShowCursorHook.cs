using HarmonyLib;
using Rewired;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Divide.PCInput
{
    [HarmonyPatch(typeof(PlayerStateVariables), "set_CurrentPlayer")]
    internal static class ShowCursorHook
    {
        static void Prefix(Player value)
        {
            Cursor.visible = value != null && value.controllers.joystickCount == 0;
        }
    }
}
