using ETS.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Divide.EnableCameraDebug
{
    [RequireComponent(typeof(PSCameraMover))]
    internal class CameraRetargeter : SingletonBehaviour<CameraRetargeter>
    {
        PSCameraMover cameraMover;

        protected override void Start()
        {
            base.Start();
            cameraMover = GetComponent<PSCameraMover>();
            if (cameraMover == null) cameraMover = gameObject.AddComponent<PSCameraMover>();
            cameraMover.m_controlOnStart = false;
            GameManager.Instance.OnCameraChanged = (Action<Camera>)Delegate.Combine(GameManager.Instance.OnCameraChanged, new Action<Camera>(OnCameraChanged));
            OnCameraChanged(PlayerStateVariables.m_activeCamera);
        }

        void OnCameraChanged(Camera camera)
        {
            if (LOGDUI.Instance.MattePainting)
            {
                cameraMover.m_moveObject = null;
                return;
            }

            if (camera.transform.parent != null)
            {
                var parent = camera.transform.parent;
                if (parent.GetComponent<Camera>() != null || parent.GetComponent<ThirdsCamera>() != null)
                {
                    cameraMover.m_moveObject = parent;
                    return;
                }
            }

            cameraMover.m_moveObject = camera.transform;
        }
    }
}
