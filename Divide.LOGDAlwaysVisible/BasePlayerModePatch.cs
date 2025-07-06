using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Divide.LOGDAlwaysVisible
{
    [HarmonyPatch(typeof(BasePlayerMode), "ProcessLOGDs")]
    internal static class BasePlayerModePatch
    {
        const float SCAN_INTERVAL = 5f;

        static float lastNodesScanTime;

        static void SetLOGDsVisible(int m_exploreFocusLayerMask, int m_combatFocusLayerMask)
        {
            // We run this on an interval because sometimes the nodes get unloaded, and they also get cleared if you
            // toggle supervisor mode.
            float currTime = Time.unscaledTime;
            if (lastNodesScanTime + SCAN_INTERVAL < currTime)
            {
                int layerMask = m_exploreFocusLayerMask | m_combatFocusLayerMask;
                foreach (Collider collider in UnityEngine.Object.FindObjectsOfType<Collider>())
                {
                    if (((1 << collider.gameObject.layer) & layerMask) != 0)
                    {
                        var parent = collider.transform.parent;
                        if (parent == null) continue;
                        LOGDNodeBehaviour node = parent.GetComponent<LOGDNodeBehaviour>();
                        if (node != null)
                        {
                            // Set not visible first because if the node gets destroyed, it doesn't get removed
                            // from the list of nodes visible (because we prevent the automatic removal), so when
                            // adding a new instance (e.g. after a chunk loads) the operation is ignored. Doesn't
                            // seem to have much performance impact.
                            node.SetNodeVisible(false);
                            node.SetNodeVisible(true);
                        }
                    }
                }
                lastNodesScanTime = currTime;
            }
        }

        static void Prefix(int ___m_exploreFocusLayerMask, int ___m_combatFocusLayerMask)
        {
            SetLOGDsVisible(___m_exploreFocusLayerMask, ___m_combatFocusLayerMask);
        }
    }
}
