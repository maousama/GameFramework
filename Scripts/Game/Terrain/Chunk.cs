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
        public static int sideLength = 64;

        private Vector2Int index;

        public void Initialize(Vector2Int index)
        {
            this.index = index;
            transform.position = new Vector3(index.x * sideLength, 0, index.y * sideLength);

            Block[,] blocks = new Block[sideLength, sideLength];
            for (int z = 0; z < sideLength; z++)
            {
                for (int x = 0; x < sideLength; x++)
                {
                    Block block = new GameObject("Block", typeof(Block)).GetComponent<Block>();
                    block.Initialize(this, new Vector2Int(x, z));
                    blocks[x, z] = block;
                }
            }
        }

        public Dictionary<Vector2Int, int> heightMap = new Dictionary<Vector2Int, int>(sideLength++1));

    }
}
