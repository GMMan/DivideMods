using ETS.AI;
using ETS.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Divide.PCInput
{
    public static class InputLogic
    {
        public static float MOUSE_POS_SCALE_FACTOR = 1.9f;
        public static float MOUSE_COMBAT_LOG_SCALE = 0.4551196f; // Magnitude 1.0 at 5 units

        public static bool SynputLeftStickClicked { get; private set; }
        public static bool SynputRightStickClicked { get; private set; }

        public static Vector3 GetMousePosRelativeToScreenSpace()
        {
            // Use Unity mouse for simplicity
            Vector3 pos = Input.mousePosition;
            // Clamp to within window
            pos.x = Mathf.Clamp(pos.x, 0, Screen.width);
            pos.y = Mathf.Clamp(pos.y, 0, Screen.height);
            // Change center
            float halfScreenWidth = Screen.width / 2f;
            float halfScreenHeight = Screen.height / 2f;
            pos.x -= halfScreenWidth;
            pos.y -= halfScreenHeight;
            // Scale relative to height
            pos /= halfScreenHeight;
            // Scale up for GUI
            pos *= MOUSE_POS_SCALE_FACTOR;
            // Clamp values
            pos.x = Mathf.Clamp(pos.x, -1f, 1f);
            pos.y = Mathf.Clamp(pos.y, -1f, 1f);
            // Transfer y to z
            pos.z = pos.y;
            pos.y = 0;
            // Keep it in a circle radius
            if (pos.sqrMagnitude > 1) pos = pos.normalized;
            return pos;
        }

        public static Vector3 GetKeyboardPos()
        {
            var currentPlayer = PlayerStateVariables.CurrentPlayer;
            if (currentPlayer == null) return default;
            return new Vector3(currentPlayer.GetAxis(ActionIds.ACTION_ID_CUSTOM_MOVE_HORIZONTAL), 0, currentPlayer.GetAxis(ActionIds.ACTION_ID_CUSTOM_MOVE_VERTICAL));
        }

        public static void DoCommonMouseHandling(float? deadzone)
        {
            Vector3 rotationVector;
            // SOLUS or supervisor mode
            if (GameManager.Instance.Player.CurrentState is CodexMode ||
                PlayerStateVariables.CurrentPlayerMode == PlayerStateVariables.CurrentMode.EXPLORE && PlayerStateVariables.currentLOGDMode == LOGDNodeBehaviour.LOGDNodeType.COMBAT)
            {
                rotationVector = GetMousePosRelativeToScreenSpace();
                if (deadzone.HasValue)
                {
                    BasePlayerMode.ClipToDeadZone(ref rotationVector, deadzone.Value);
                }
                else
                {
                    BasePlayerMode.ClipToDeadZone(ref rotationVector);
                }
            }
            else
            {
                Vector3 eyePosition = GameManager.Instance.Player.m_eyeLevelTransform.position;
                Plane eyePlane;
                if (LOGDUI.Instance.MattePainting)
                {
                    var planeDirection = PlayerStateVariables.m_cameraTransform.position - eyePosition;
                    eyePlane = new Plane(planeDirection.normalized, eyePosition);
                }
                else
                {
                    if (GameManager.Instance.Player.CurrentState is RemoteControlMode remoteControl)
                    {
                        var locomotion = remoteControl.ControlledNPC.GetComponent<CharacterLocomotion>();
                        eyePosition = locomotion.HeadPosition;
                    }
                    eyePlane = new Plane(Vector3.up, eyePosition);
                }
                Ray ray = PlayerStateVariables.m_activeCamera.ScreenPointToRay(Input.mousePosition);
                if (eyePlane.Raycast(ray, out var distance))
                {
                    Vector3 worldPoint = ray.GetPoint(distance);
                    Vector3 lookDirection = worldPoint - eyePosition;
                    if (!LOGDUI.Instance.MattePainting)
                    {
                        if (PlayerStateVariables.CurrentPlayerMode == PlayerStateVariables.CurrentMode.COMBAT)
                        {
                            // Scale down for camera movement
                            lookDirection = lookDirection.normalized * Mathf.Log(lookDirection.magnitude) * MOUSE_COMBAT_LOG_SCALE;
                        }

                        if (lookDirection.sqrMagnitude > 1f) lookDirection = lookDirection.normalized;
                    }
                    else
                    {
                        // Change axis for game's interpretation
                        lookDirection.x = -lookDirection.x;
                        lookDirection.z = lookDirection.y;
                        lookDirection.y = 0;
                    }

                    if (deadzone.HasValue)
                    {
                        BasePlayerMode.ClipToDeadZone(ref lookDirection, deadzone.Value);
                    }
                    else
                    {
                        BasePlayerMode.ClipToDeadZone(ref lookDirection);
                    }

                    if (!LOGDUI.Instance.MattePainting)
                    {
                        // Calculate y component so that the inverse rotated vector has a y component of zero
                        var cameraUp = PlayerStateVariables.m_cameraTransform.up;
                        if (cameraUp.y != 0)
                        {
                            lookDirection.y = -((lookDirection.x * cameraUp.x + lookDirection.z * cameraUp.z) / cameraUp.y);
                        }
                        else
                        {
                            lookDirection.y = 0;
                        }
                        rotationVector = PlayerStateVariables.m_cameraTransform.InverseTransformDirection(lookDirection.x, lookDirection.y, lookDirection.z);
                    }
                    else
                    {
                        rotationVector = lookDirection;
                    }
                }
                else
                {
                    rotationVector = Vector3.zero;
                }
            }
            PlayerStateVariables.m_RotationVector.Set(rotationVector.x, rotationVector.y, rotationVector.z);
            PlayerStateVariables.m_xRot = rotationVector.x;
            PlayerStateVariables.m_yRot = rotationVector.z;
        }

        public static void DoCommonKeyboardHandling(float? deadzone)
        {
            // Don't contribute for Synput
            if (GameManager.Instance.Player.CurrentState is CodexMode) return;

            var keyboardPos = GetKeyboardPos();
            if (deadzone.HasValue)
            {
                BasePlayerMode.ClipToDeadZone(ref keyboardPos, deadzone.Value);
            }
            else
            {
                BasePlayerMode.ClipToDeadZone(ref keyboardPos);
            }

            var worldVec = new Vector3(-keyboardPos.x, keyboardPos.y, -keyboardPos.z);
            var cameraUp = PlayerStateVariables.m_cameraTransform.up;
            // Calculate y component so that the inverse rotated vector has a y component of zero
            if (cameraUp.y != 0)
            {
                worldVec.y = -((worldVec.x * cameraUp.x + worldVec.z * cameraUp.z) / cameraUp.y);
            }
            var inputVec = PlayerStateVariables.m_cameraTransform.InverseTransformDirection(worldVec);
            PlayerStateVariables.m_MovementVector.Set(inputVec.x, inputVec.y, inputVec.z);
            PlayerStateVariables.m_xMov = inputVec.x;
            PlayerStateVariables.m_yMov = inputVec.z;
        }

        public static void PollSynput()
        {
            var player = PlayerStateVariables.CurrentPlayer;
            if (player != null)
            {
                SynputLeftStickClicked = player.GetButtonDown(ActionIds.ACTION_ID_CUSTOM_LEFT_STICK_CLICK);
                SynputRightStickClicked = player.GetButtonDown(ActionIds.ACTION_ID_CUSTOM_RIGHT_STICK_CLICK);
            }
            else
            {
                SynputLeftStickClicked = false;
                SynputRightStickClicked = false;
            }
        }

        public static void PollSynputBlank()
        {
            SynputLeftStickClicked = false;
            SynputRightStickClicked = false;
        }

        public static bool EvaluateSynput(StickAndClickComboData gesture)
        {
            if (gesture.EvaluateInput(PlayerStateVariables.m_leftStickComboButton, PlayerStateVariables.m_rightStickComboButton,
                PlayerStateVariables.m_MovementVector, PlayerStateVariables.m_RotationVector))
            {
                return true;
            }

            // Keyboard check
            var keyboardPos = GetKeyboardPos();
            BasePlayerMode.ClipToDeadZone(ref keyboardPos);
            return gesture.EvaluateInput(SynputLeftStickClicked, SynputRightStickClicked, keyboardPos, keyboardPos);
        }
    }
}
