using Assets.Scripts.Game.Terrain.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain
{
    /// <summary>
    /// 大地信息
    /// </summary>
    internal class CoordinateInfo
    {
        internal Vector2Int coordinateValue;
        internal string[] blocks = Enumerable.Repeat(Air.blockName, 2 * Chunk.HalfHeight).ToArray();
        internal float baseTemperature;
        internal float precipitation;
        internal string biomeName;
        internal float GetTemperature(float height)
        {
            return baseTemperature + 0.5f - height * 0.01f;
        }
    }

}
