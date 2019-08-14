using Assets.Scripts.Game.Terrain.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Biomes
{
    internal class IceLand : Biome
    {
        internal static readonly string BiomeName = "IceLand";
        internal override string Name { get { return BiomeName; } }

        internal override void SetStratum(CoordinateInfo coordinateInfo)
        {
            float noiseValue2D = PerlinNoise.PerlinNoise2D(Map.Seed - 1, coordinateInfo.position.x * 0.03f, coordinateInfo.position.y * 0.03f);
            noiseValue2D = (noiseValue2D + 0.8f) * 0.5f;
            int coverCount = (int)(noiseValue2D * Chunk.HalfHeight * 0.5f);
            for (int y = 2 * Chunk.HalfHeight - 1; y > -1; y--)
            {
                float density = GetDensity(coordinateInfo, y);

                if (density > 0)
                {
                    if (coverCount > 0)
                    {
                        coordinateInfo.SetBlock(y, Snow.blockName);
                        coverCount--;
                    }
                    else
                    {
                        coordinateInfo.SetBlock(y, Stone.blockName);
                    }
                }
                else
                {
                    SetWater(coordinateInfo, y);
                }
            }
        }
    }
}