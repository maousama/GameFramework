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
        public static int heightMagnification = 20;
        public static int SideLength { get { return halfSideLength * 2; } }


        private List<Vector2Int> blockMapKeys;
        private List<Vector2Int> heightMapkeys;

        private void Awake()
        {
            //Create height map that its blocks need
            CreateHeightMap();
            //Create block map
            CreateBlockMap();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < blockMapKeys.Count; i++) MapGenerator.blockMap.Remove(blockMapKeys[i]);
            for (int i = 0; i < heightMapkeys.Count; i++) MapGenerator.heightMap.Remove(heightMapkeys[i]);
        }

        private void CreateBlockMap()
        {
            int blockCount = SideLength * SideLength;
            blockMapKeys = new List<Vector2Int>();
            List<GameObject> gameObjects = new List<GameObject>(blockCount);

            Vector3 chunkPosition = transform.position;
            for (int z = 0; z < SideLength; z++)
            {
                for (int x = 0; x < SideLength; x++)
                {
                    GameObject blockGameObject = new GameObject("Block");
                    blockGameObject.transform.parent = transform;
                    blockGameObject.transform.localPosition = new Vector3(x + 0.5f - halfSideLength, 0, z + 0.5f - halfSideLength);
                    Block block = blockGameObject.AddComponent<Block>();
                    Vector3 blockPosition = block.transform.position;
                    Vector2Int key = new Vector2Int(Mathf.FloorToInt(blockPosition.x), Mathf.FloorToInt(blockPosition.z));
                    blockMapKeys.Add(key);
                    MapGenerator.blockMap.Add(key, block);
                    gameObjects.Add(block.gameObject);
                }
            }
            StaticBatchingUtility.Combine(gameObjects.ToArray(), gameObject);
        }

        private void CreateHeightMap()
        {
            int ix = (int)transform.position.x;
            int iz = (int)transform.position.z;
            heightMapkeys = new List<Vector2Int>();
            for (int x = ix - halfSideLength; x <= ix + halfSideLength; x++)
            {
                for (int z = iz - halfSideLength; z <= iz + halfSideLength; z++)
                {
                    Vector2Int key = new Vector2Int(x, z);
                    heightMapkeys.Add(key);
                    float height = PerlinNoise.SuperimposedOctave(MapGenerator.seed, 0.003f * key.x, 0.003f * key.y, 6) * heightMagnification;
                    if (!MapGenerator.heightMap.ContainsKey(key)) MapGenerator.heightMap.Add(key, height);
                }
            }
        }


    }
}
