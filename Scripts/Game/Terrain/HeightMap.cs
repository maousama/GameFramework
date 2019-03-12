using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.Game.Terrain
{
    /// <summary>
    /// 坐标高度图(x,z)实际代表(x+0.5,z+0.5)处的高度
    /// </summary>
    public class HeightMap
    {
        private Dictionary<Vector2Int, int> coordinateHeightMap = new Dictionary<Vector2Int, int>();

        public int GetHeight(Vector2Int coordinate)
        {
            if (coordinateHeightMap.ContainsKey(coordinate)) return coordinateHeightMap[coordinate];
            else
            {
                
            }
        }
    }
}


