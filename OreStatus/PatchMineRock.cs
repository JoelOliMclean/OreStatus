using HarmonyLib;
using UnityEngine;
using static OreStatus.OreStatus;

namespace OreStatus
{
    [HarmonyPatch(typeof(MineRock))]
    class PatchMineRock
    {
        [HarmonyPatch("RPC_Hit")]
        [HarmonyPostfix]
        internal static void RPC_Hit_Post(ref MineRock __instance, long sender, HitData hit, int hitAreaIndex)
        {
            if (!OreStatusConfig.displayType.Value.Equals(DisplayType.Disabled))
            {
                var m_nview = AccessTools.Field(typeof(MineRock5), "m_nview").GetValue(__instance) as ZNetView;
                if (m_nview.IsValid())
                {
                    Collider hitArea = (Collider)AccessTools.Method(typeof(MineRock), "GetHitArea").Invoke(__instance, new object[] { hitAreaIndex });
                    string name = "Health" + hitAreaIndex.ToString();
                    var remainingHealth = m_nview.GetZDO().GetFloat(name, __instance.m_health);
                    float remainingPercentage = remainingHealth / __instance.m_health * 100;
                    if (remainingPercentage > 0.0f)
                    {
                        Chat.instance.SetNpcText(hitArea.gameObject, Vector3.up, 0, 5.0f, "", OreStatusUtils.GetPercentageString(remainingPercentage), false);
                    }
                }
            }
        }
    }
}
