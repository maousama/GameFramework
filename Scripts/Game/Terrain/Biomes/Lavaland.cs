using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Biomes
{
    /// <summary>
    /// 熔岩大陆
    /// </summary>
    public class Lavaland : Biome
    {
        private static Lavaland instance = null;
        public static Lavaland Instance
        {
            get
            {
                if (instance == null) instance = new Lavaland();
                return instance;
            }
        }
        private Color color = new Color(0.4f, 0, 0);
        public override Color Color { get { return color; } }

        public override void SetItem(Block block)
        {
            throw new NotImplementedException();
        }
    }
}
