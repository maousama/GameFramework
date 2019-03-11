using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.Terrain
{

    public class MapGenerator : MonoSingleton<MapGenerator>
    {

        private void CreateOneChunk()
        {
            
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
        public bool IsDone => throw new NotImplementedException();

        public float Progress => throw new NotImplementedException();

        public Action Completed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
