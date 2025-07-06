using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Divide.ARGlobalLighting
{
    public class Mod : MelonMod
    {
        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName.StartsWith("Ep") && !sceneName.Contains("_ROO"))
            {
                var lightGo = new GameObject("AR_Light");
                var light = lightGo.AddComponent<Light>();
                light.type = LightType.Directional;
                light.color = new Color(0.4926471f, 0.4926471f, 0.4926471f);
                light.cullingMask = unchecked((int)0xfffffdfe);
                light.intensity = 0.35f;
            }
        }
    }
}
