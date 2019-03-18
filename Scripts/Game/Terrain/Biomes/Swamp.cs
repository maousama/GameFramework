using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Biomes
{
    /// <summary>
    /// 沼泽
    /// </summary>
    public class Swamp : Biome
    {
        private static Swamp instance = null;
        public static Swamp Instance
        {
            get
            {
                if (instance == null) instance = new Swamp();
                return instance;
            }
        }
        private Color color = new Color(0.4f, 0.2f, 0);
        public override Color Color { get { return color; } }

        public override void SetItem(Block block)
        {
        }
    }
}
