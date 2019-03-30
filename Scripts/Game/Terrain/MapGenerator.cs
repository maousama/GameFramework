using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
            seed = UnityEngine.Random.Range(1, 999);
            for (int z = 0; z < 3; z++)
            {
                for (int x = 0; x < 3; x++)
                {
                    GameObject chunk = new GameObject("Chunk");
                    chunk.transform.position = new Vector3(x * Chunk.SideLength, 0, z * Chunk.SideLength);
                    chunk.AddComponent<Chunk>();
                }
            }

            foreach (Chunk chunk in chunkMap.Values) chunk.DrawBlocks();
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

            yield return null;
        }
    }
}
