using HarmonyLib;
using System;
using UnityEngine;
using static OreStatus.OreStatus;

namespace OreStatus
{
    [HarmonyPatch(typeof(MineRock5))]
    internal class PatchMineRock5
    {
        [HarmonyPatch("RPC_Damage")]
        [HarmonyPostfix]
        internal static void RPC_Damage(ref MineRock5 __instance, long sender, HitData hit, int hitAreaIndex)
        {
            if (!OreStatusConfig.displayType.Value.Equals(DisplayType.Disabled))
            {
                var m_nview = AccessTools.Field(typeof(MineRock5), "m_nview").GetValue(__instance) as ZNetView;
                if (m_nview.IsValid())
                {
                    MineRock5.HitArea hitArea = (MineRock5.HitArea)AccessTools.Method(typeof(MineRock5), "GetHitArea").Invoke(__instance, new object[] { hitAreaIndex });
                    float remainingPercentage = hitArea.m_health / __instance.m_health * 100;
                    if (remainingPercentage > 0.0f)
                    {
                        Chat.instance.SetNpcText(hitArea.m_collider.gameObject, hitArea.m_bound.m_pos, 0, 5.0f, "", OreStatusUtils.GetPercentageString(remainingPercentage), false);
                    }
                }
            }
        }
    }
}
