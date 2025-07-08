using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Divide.PCInput
{
    [HarmonyPatch(typeof(ThirdsCamera), "LateUpdate")]
    internal static class ThirdsCameraLateUpdatePatch
    {
        // Try to smooth out jittering on exiting staircases because of weird logic that tries to
        // keep the character looking forwards on stairs

        static float lastStairsTime;

        static void Prefix(out Vector3 __state)
        {
            __state = PlayerStateVariables.m_RotationVector;
            var player = GameManager.Instance.Player;
            if (player != null)
            {
                if (player.CurrentState is StairsClimbMode)
                {
                    lastStairsTime = Time.time;
                }
                if (Time.time - lastStairsTime < 0.25f)
                {
                    PlayerStateVariables.m_RotationVector.Set(0, 0, 0);
                }
            }
        }

        static void Postfix(Vector3 __state)
        {
            PlayerStateVariables.m_RotationVector.Set(__state.x, __state.y, __state.z);
        }
    }
}
