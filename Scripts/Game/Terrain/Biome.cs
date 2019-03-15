using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.Terrain
{
    public class Biome
    {

        public BiomeType BiomeSelector(float temperature, float precipitationPercentage)
        {
            if (temperature < 25)
            {
                if (precipitationPercentage < 25) return BiomeType.ColdDesert;
                else if (precipitationPercentage < 70) return BiomeType.Tundra;
                else return BiomeType.IceDesert;
            }
            else if (temperature < 45)
            {
                if (precipitationPercentage < 10) return BiomeType.ColdDesert;
                else if (precipitationPercentage < 20) return BiomeType.TemperateGrassland;
                else if (precipitationPercentage < 50) return BiomeType.Shrubland;
                else return BiomeType.BorealForest;
            }
            else if (temperature < 75)
            {
                if (precipitationPercentage < 10) return BiomeType.TemperateDesert;
                else if (precipitationPercentage < 25) return BiomeType.Shrubland;
                else if (precipitationPercentage < 40) return BiomeType.TemperateGrassland;
                else if (precipitationPercentage < 55) return BiomeType.Woodland;
                else if (precipitationPercentage < 80) return BiomeType.TemperateSeasonalForest;
                else return BiomeType.TemperateRainforest;
            }
            else
            {
                if (precipitationPercentage < 30) return BiomeType.SubtropicalDesert;
                else if (precipitationPercentage < 70) return BiomeType.TropicalSeasonalForest;
                else return BiomeType.TropicalRainforest;
            }
        }
    }
    public enum BiomeType
    {
        /// <summary>
        /// 冰沙漠
        /// </summary>
        IceDesert,
        /// <summary>
        /// 寒冷沙漠
        /// </summary>
        ColdDesert,
        /// <summary>
        /// 苔原
        /// </summary>
        Tundra,
        /// <summary>
        /// 季节性沙漠
        /// </summary>
        TemperateDesert,
        /// <summary>
        /// 温带草原
        /// </summary>
        TemperateGrassland,
        /// <summary>
        /// 亚热带沙漠
        /// </summary>
        SubtropicalDesert,
        /// <summary>
        /// 针叶林
        /// </summary>
        BorealForest,
        /// <summary>
        /// 灌木
        /// </summary>
        Shrubland,
        /// <summary>
        /// 林地
        /// </summary>
        Woodland,
        /// <summary>
        /// 温带季节性森林
        /// </summary>
        TemperateSeasonalForest,
        /// <summary>
        /// 温带雨林
        /// </summary>
        TemperateRainforest,
        /// <summary>
        /// 热带雨林
        /// </summary>
        TropicalRainforest,
        /// <summary>
        /// 热带季节性森林
        /// </summary>
        TropicalSeasonalForest,

    }
}
