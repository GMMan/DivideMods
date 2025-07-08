using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Divide.EnableCameraDebug
{
    public class Mod : MelonMod
    {
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            // Fetch instance so it gets instantiated
            GC.KeepAlive(CameraRetargeter.Instance);
        }
    }
}
