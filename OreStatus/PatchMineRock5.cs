using HarmonyLib;
using System;
using UnityEngine;
using static OreStatus.OreStatus;

namespace OreStatus
{
    [HarmonyPatch(typeof(MineRock5))]
    internal class PatchMineRock5
    {
        [HarmonyPatch("DamageArea")]
        [HarmonyPostfix]
        internal static void DamageArea_Post(ref MineRock5 __instance, int hitAreaIndex, HitData hit)
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
                        Chat.instance.SetNpcText(hitArea.m_collider.gameObject, hitArea.m_bound.m_pos, 0, 5.0f, "", GetPercentageString(remainingPercentage), false);
                    }
                }
            }
        }

        private static string GetPercentageString(float percentage)
        {
            switch (OreStatusConfig.displayType.Value)
            {
                case DisplayType.HealthBar:
                    float percentageBase2 = percentage / 10f;
                    string progress = "";
                    for (int i = 0; i < 10; i++)
                    {
                        if (i <= percentageBase2)
                        {
                            progress += "<color=#00CC00>█</color>";
                        }
                        else
                        {
                            progress += "<color=#CC0000>█</color>";
                        }
                    }
                    return progress;
                case DisplayType.Percentage:
                default:
                    return $"<color=orange>{Convert.ToInt32(percentage)}% left</color>";

            }
        }
    }
}
