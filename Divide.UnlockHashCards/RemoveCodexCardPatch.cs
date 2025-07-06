using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Divide.UnlockHashCards
{
    [HarmonyPatch(typeof(PlayerCodex), nameof(PlayerCodex.RemoveCodexCard))]
    internal static class RemoveCodexCardPatch
    {
        static bool Prefix(PlayerCodex __instance, ref bool __result, CodexCard card)
        {
            // Block hash cards from being removed
            if (card == __instance.Hashes.HashInitialConnectionCard || card == __instance.Hashes.HashUnlockerCard)
            {
                __result = true;
                return false;
            }

            return true;
        }
    }
}
