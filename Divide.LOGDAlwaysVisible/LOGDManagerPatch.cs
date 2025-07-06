using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Divide.LOGDAlwaysVisible
{
    [HarmonyPatch(typeof(LOGDManager), "Update")]
    internal static class LOGDManagerPatch
    {
        static bool Prefix()
        {
            return false;
        }
    }
}
