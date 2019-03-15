using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain
{
    public class Chunk : MonoBehaviour
    {
        public static int halfSideLength = 32;
        public static int heightMagnification = 10;
        public static int SideLength { get { return halfSideLength * 2; } }

        public float min, max;

        private Vector2Int index;

        public void Initialize(Vector2Int index)
        {
            this.index = index;
            transform.position = new Vector3(index.x * SideLength, 0, index.y * SideLength);

            CreateHeightMap();

            Block[,] blocks = new Block[SideLength, SideLength];
            for (int z = 0; z < SideLength; z++)
            {
                for (int x = 0; x < SideLength; x++)
                {
                    Block block = new GameObject("Block", typeof(Block)).GetComponent<Block>();
                    block.Initialize(this, new Vector2Int(x, z));
                    blocks[x, z] = block;
                }
            }

        }


        /// <summary>
        /// Temporary real position to height
        /// </summary>
        private Dictionary<Vector2Int, float> heightMap;
        private void CreateHeightMap()
        {
            heightMap = new Dictionary<Vector2Int, float>((SideLength + 1) * (SideLength + 1));
            int ix = (int)transform.position.x;
            int iz = (int)transform.position.z;
            for (int x = ix - halfSideLength; x <= ix + halfSideLength; x++)
            {
                for (int z = iz - halfSideLength; z <= iz + halfSideLength; z++)
                {
                    Vector2Int key = new Vector2Int(x, z);
                    float height = PerlinNoise.SuperimposedOctave(0.003f * key.x, 0.003f * key.y, 6) * 20;
                    heightMap.Add(key, height);
                }
            }
        }
        public float GetHeight(Vector2Int vector2Int)
        {
            return heightMap[vector2Int];
        }
    }
}
