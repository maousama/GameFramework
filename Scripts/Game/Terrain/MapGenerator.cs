using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Game.Terrain
{

    public class MapGenerator : MonoSingleton<MapGenerator>
    {
        public static int seed = 5;

        internal static Dictionary<Vector2Int, float> heightMap = new Dictionary<Vector2Int, float>();
        internal static Dictionary<Vector2Int, Block> blockMap = new Dictionary<Vector2Int, Block>();
        internal static Dictionary<Vector2Int, Chunk> chunkMap = new Dictionary<Vector2Int, Chunk>();

        private void Start()
        {
            StartCoroutine(GenerateMap());
        }

        //chunk = 256*256*128 block
        //choose map size
        //create height-map, "Raising..."
        //create strata, "Soiling..."
        //carve out caves, "Carving..."
        //create ore veins
        //flood fill-water, "Watering..."
        //flood fill-lava, "Melting..."
        //create surface layer, "Growing..."
        //create plants, "Planting..."
        private IEnumerator GenerateMap()
        {
            //GenerateMapOperation generateMapOperation = new GenerateMapOperation();
            seed = UnityEngine.Random.Range(0,18596);
            Debug.Log(seed);
            for (int z = 0; z <= 0; z++)
            {
                for (int x = 0; x <= 0; x++)
                {
                    GameObject gameObject = new GameObject("Chunk");
                    gameObject.transform.position = new Vector3(x * Chunk.SideLength, 0, z * Chunk.SideLength);
                    Chunk chunk = gameObject.AddComponent<Chunk>();
                    chunkMap.Add(new Vector2Int(x, z), chunk);
                    chunk.SetHeightMap();
                    yield return null;
                    chunk.SetBlockMap();
                }
            }

            foreach (Chunk chunk in chunkMap.Values)
            {
                chunk.DrawBlocks();
                yield return null;
                chunk.CombineBatches();
            }
            yield return null;
        }
    }
}
