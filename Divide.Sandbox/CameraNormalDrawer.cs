using Divide.PCInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Divide.Sandbox
{
    [RequireComponent(typeof(LineRenderer))]
    internal class CameraNormalDrawer : MonoBehaviour
    {
        LineRenderer lineRenderer;

        void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            if (lineRenderer == null) lineRenderer = gameObject.AddComponent<LineRenderer>();

            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.SetColors(Color.white, Color.green);
            lineRenderer.SetWidth(0.1f, 0.1f);
            lineRenderer.enabled = false;
        }

        void Update()
        {
            var player = GameManager.Instance.Player;
            var cameraTransform = PlayerStateVariables.m_cameraTransform;
            if (player != null && cameraTransform != null)
            {
                var keyboardPos = InputLogic.GetKeyboardPos();
                var keyboardNormalized = keyboardPos.normalized;
                var fromPos = GameManager.Instance.Player.m_eyeLevelTransform.position;
                var toPos = GameManager.Instance.Player.m_eyeLevelTransform.position + new Vector3(-keyboardNormalized.x, keyboardNormalized.y, -keyboardNormalized.z) * keyboardPos.magnitude;

                lineRenderer.SetPositions(new[] { fromPos, toPos });
                lineRenderer.enabled = true;
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }
    }
}
