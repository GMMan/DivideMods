using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Divide.PCInput
{
    [HarmonyPatch(typeof(RemoteControlMode), "SetupRotationVector")]
    internal static class RemoteControlModeSetupRotationVectorPatch
    {
        static void Postfix()
        {
            if (PlayerStateVariables.m_RotationVector == Vector3.zero)
            {
                InputLogic.DoCommonMouseHandling(PlayerStateVariables.m_characterProperties.controllerOptions.AimingDeadZone);
            }
        }
    }
}
