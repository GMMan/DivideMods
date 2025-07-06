using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Divide.UnlockHashCards
{
    [HarmonyPatch(typeof(GameManager), "ClearLoadScreen")]
    internal static class HashUnlockerPatch
    {
        static void Postfix()
        {
            var codex = GameManager.Instance.PlayerCodex;
            if (codex != null)
            {
                if (!codex.HasCodexCard(codex.Hashes.HashInitialConnectionCard))
                    codex.AddCodexCard(codex.Hashes.HashInitialConnectionCard);

                if (!codex.HasCodexCard(codex.Hashes.HashUnlockerCard))
                    codex.AddCodexCard(codex.Hashes.HashUnlockerCard);

                var vpointsCard = Resources.Load<CodexCard>("data/codexcards/XCRD_CitizenID_Software_VPoints");
                if (!codex.HasCodexCard(vpointsCard))
                    codex.AddCodexCard(vpointsCard);

                var supervisorCard = Resources.Load<CodexCard>("data/codexcards/XCRD_CitizenID_Software_SupervisorMode");
                if (!codex.HasCodexCard(supervisorCard))
                    codex.AddCodexCard(supervisorCard);

                var riotCard = Resources.Load<CodexCard>("data/codexcards/XCRD_CitizenID_Software_RiotMode");
                if (!codex.HasCodexCard(riotCard))
                    codex.AddCodexCard(riotCard);
            }
        }
    }
}
