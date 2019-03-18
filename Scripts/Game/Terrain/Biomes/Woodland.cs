using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Biomes
{
    /// <summary>
    /// 林地
    /// </summary>
    public class Woodland : Biome
    {
        private static Woodland instance = null;
        public static Woodland Instance
        {
            get
            {
                if (instance == null) instance = new Woodland();
                return instance;
            }
        }
        private Color color = new Color(0.6f, 0.5f, 0.3f);
        public override Color Color { get { return color; } }

        public override void SetItem(Block block)
        {
        }
    }
}
