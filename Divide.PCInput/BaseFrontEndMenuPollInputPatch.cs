using ETS.UI;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Divide.PCInput
{
    [HarmonyPatch(typeof(BaseFrontEndMenu), "PollInput")]
    internal static class BaseFrontEndMenuPollInputPatch
    {
        static void Postfix(ref Vector3 ___m_currentStickInput, float ___m_controllerDeadZone)
        {
            if (___m_currentStickInput == Vector3.zero)
            {
                ___m_currentStickInput = InputLogic.GetMousePosRelativeToScreenSpace();
                BasePlayerMode.ClipToDeadZone(ref ___m_currentStickInput, ___m_controllerDeadZone);
            }
        }
    }
}
