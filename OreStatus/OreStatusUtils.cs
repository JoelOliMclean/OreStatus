using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OreStatus.OreStatus;

namespace OreStatus
{
    internal class OreStatusUtils
    {   
        internal static string GetPercentageString(float percentage)
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
