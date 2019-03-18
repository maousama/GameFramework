using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Biomes
{
    /// <summary>
    /// 苔原
    /// </summary>
    public class Tundra : Biome
    {
        private static Tundra instance = null;
        public static Tundra Instance
        {
            get
            {
                if (instance == null) instance = new Tundra();
                return instance;
            }
        }
        private Color color = new Color(0.35f, 0.5f, 0.25f);
        public override Color Color { get { return color; } }

        public override void SetItem(Block block)
        {
            throw new NotImplementedException();
        }
    }
}
