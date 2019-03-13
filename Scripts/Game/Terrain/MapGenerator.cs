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
        public static int seed = 0;

        private void Start()
        {
            Chunk chunk = new GameObject("Chunk", typeof(Chunk)).GetComponent<Chunk>();
            chunk.Initialize(Vector2Int.zero);
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
