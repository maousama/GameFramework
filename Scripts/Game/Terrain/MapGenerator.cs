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
        public static int seed = 154;

        private void Start()
        {
            for (int z = 0; z < 3; z++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Chunk chunk = new GameObject("Chunk", typeof(Chunk)).GetComponent<Chunk>();
                    chunk.Initialize(new Vector2Int(x, z));
                }
            }
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
            GenerateMapOperation generateMapOperation = new GenerateMapOperation();

            yield return null;
        }
    }


    public class GenerateMapOperation : IOperation
    {
        public bool IsDone
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public float Progress
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Action Completed
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {

            }
        }
    }
}
