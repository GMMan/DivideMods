using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Divide.PCInput
{
    [HarmonyPatch(typeof(ErrorHandler), "PollInput")]
    internal class ErrorHandlerPollInputPatch
    {
        static void Postfix(ref Vector3 ___m_currentStickInput)
        {
            if (___m_currentStickInput == Vector3.zero)
            {
                ___m_currentStickInput = InputLogic.GetMousePosRelativeToScreenSpace();
                BasePlayerMode.ClipToDeadZone(ref ___m_currentStickInput, 0.25f);
            }
        }
    }
}
