using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Biomes
{
    /// <summary>
    /// 雨林
    /// </summary>
    public class RainForest : Biome
    {
        private static RainForest instance = null;
        public static RainForest Instance
        {
            get
            {
                if (instance == null) instance = new RainForest();
                return instance;
            }
        }
        private Color color = new Color(0.4f, 0.3f, 0.2f);
        public override Color Color { get { return color; } }

        public override void SetItem(Block block)
        {
            throw new NotImplementedException();
        }
    }
}
