using HarmonyLib;
using UnityEngine;
using static OreStatus.OreStatus;

namespace OreStatus
{
    [HarmonyPatch(typeof(Destructible))]
    class PatchDestructible
    {
        [HarmonyPatch("RPC_Damage")]
        [HarmonyPostfix]
        internal static void RPC_Damage_Post(ref Destructible __instance, long sender, HitData hit)
        {
            if (__instance.gameObject.name.Contains("MineRock_Tin") && !OreStatusConfig.displayType.Value.Equals(DisplayType.Disabled))
            {
                var m_nview = AccessTools.Field(typeof(Destructible), "m_nview").GetValue(__instance) as ZNetView;
                if (m_nview.IsValid())
                {
                    float remainingHealth = m_nview.GetZDO().GetFloat("health", __instance.m_health);
                    float remainingPercentage = remainingHealth / __instance.m_health * 100;
                    if (remainingPercentage > 0.0f)
                    {
                        Chat.instance.SetNpcText(__instance.gameObject, Vector3.up, 0, 5.0f, "", OreStatusUtils.GetPercentageString(remainingPercentage), false);
                    }
                }
            }
        }
    }
}
