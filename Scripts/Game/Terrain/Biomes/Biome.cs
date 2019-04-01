using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Biomes
{
    public abstract class Biome
    {
        /// <summary>
        /// Set item on the block
        /// </summary>
        public abstract void SetItem(Block block);
        public abstract Color Color { get; }

        public static Biome BiomeSelector(float temperature, float precipitationPercentage, float height)
        {
            if (temperature < 10)
            {
                if (precipitationPercentage < 50) return ColdDesert.Instance;
                else return IceDesert.Instance;
            }
            else if (temperature < 30)
            {
                if (precipitationPercentage < 10) return ColdDesert.Instance;
                else if (precipitationPercentage < 70) return Tundra.Instance;
                else return BorealForest.Instance;
            }
            else if (temperature < 50)
            {
                if (precipitationPercentage < 30) return Shrubland.Instance;
                else if (precipitationPercentage < 60) return Grassland.Instance;
                else return BorealForest.Instance;
            }
            else if (temperature < 70)
            {
                if (precipitationPercentage < 10) return Shrubland.Instance;
                if (precipitationPercentage < 30) return Grassland.Instance;
                else
                {
                    if (height < 0) return Swamp.Instance;
                    else return Woodland.Instance;
                }

            }
            else if (temperature < 90)
            {
                if (precipitationPercentage < 30) return Desert.Instance;
                else if (precipitationPercentage < 60) return DarkForest.Instance;
                else return RainForest.Instance;
            }
            else return Lavaland.Instance;
        }

    }
}
