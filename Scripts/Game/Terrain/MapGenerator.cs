﻿using System;
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

            //if (false)
            //{
            //    float min = 0;
            //    float max = 0;
            //    for (int i = 0; i < 1000; i++)
            //    {
            //        for (int j = 0; j < 1000; j++)
            //        {
            //            float value = PerlinNoise.SuperimposedOctave(seed2, i * 0.003f, j * 0.003f);
            //            min = value < min ? value : min;
            //            max = value > max ? value : max;

            //        }
            //    }
            //    Debug.Log(min + " | | " + max);
            //}

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
