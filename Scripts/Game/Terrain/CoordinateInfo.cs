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
        internal Vector2Int position;
        internal float baseTemperature;
        internal float precipitation;
        internal string biomeName;

        private string[] blocks = Enumerable.Repeat(Air.blockName, 2 * Chunk.HalfHeight).ToArray();
        private int altitude = 0;

        /// <summary>
        /// 获得块名
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        internal string GetBlock(int y)
        {
            return blocks[y];
        }
        /// <summary>
        /// 设置块
        /// </summary>
        /// <param name="y"></param>
        /// <param name="block"></param>
        internal void SetBlock(int y, string block)
        {
            //如果相同则不做任何操作直接返回
            if (blocks[y] == block) return;
            //如果是顶端被删除时候更新海拔
            if (y == altitude && block == Air.blockName)
            {
                for (int i = altitude - 1; i > -1; i--)
                {
                    if (blocks[i] != Air.blockName)
                    {
                        altitude = i;
                        break;
                    }
                }
            }
            //更高的地方被设置了块且不为空气
            else if (y > altitude && block != Air.blockName)
            {
                altitude = y;
            }
            blocks[y] = block;
        }
        /// <summary>
        /// 获得具体块的温度
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        internal float GetTemperature(float height)
        {
            return baseTemperature + 0.5f - height * 0.01f;
        }
    }
}
