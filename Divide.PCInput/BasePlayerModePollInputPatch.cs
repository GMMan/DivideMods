using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Divide.PCInput
{
    [HarmonyPatch(typeof(BasePlayerMode), "PollInput")]
    internal static class BasePlayerModePollInputPatch
    {
        static void Postfix(ref float ___leftToRightStickAngle, ref float ___leftToRightStickDirection)
        {
            if (!PlayerStateVariables.ignoreInput)
            {
                InputLogic.PollSynput();
                if (!PlayerStateVariables.restrictMovement)
                {
                    if (PlayerStateVariables.m_MovementVector == Vector3.zero)
                    {
                        InputLogic.DoCommonKeyboardHandling(null);
                        if (PlayerStateVariables.m_MovementVector != Vector3.zero)
                        {
                            // Fixup some other (unused?) variables updated at the end of PollInput()
                            if (PlayerStateVariables.m_MovementVector == Vector3.zero || PlayerStateVariables.m_RotationVector == Vector3.zero)
                            {
                                ___leftToRightStickAngle = 0;
                                ___leftToRightStickDirection = 0;
                            }
                            else
                            {
                                PlayerStateVariables.m_MovementVector.AngleDir(PlayerStateVariables.m_RotationVector, Vector3.up,
                                    out ___leftToRightStickAngle, out ___leftToRightStickDirection);
                            }
                        }
                    }
                }
            }
            else
            {
                InputLogic.PollSynputBlank();
            }
        }
    }
}
