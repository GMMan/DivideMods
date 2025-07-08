using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Divide.PCInput
{
    [HarmonyPatch(typeof(BasePlayerMode), "SetupRotationVector")]
    internal static class BasePlayerModeSetupRotationVectorPatch
    {
        static void Postfix()
        {
            if (PlayerStateVariables.m_RotationVector == Vector3.zero)
            {
                InputLogic.DoCommonMouseHandling(null);
            }
        }
    }
}
