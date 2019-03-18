using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Biomes
{
    /// <summary>
    /// 灌木
    /// </summary>
    public class Shrubland : Biome
    {
        private static Shrubland instance = null;
        public static Shrubland Instance
        {
            get
            {
                if (instance == null) instance = new Shrubland();
                return instance;
            }
        }
        private Color color = new Color(0.7f, 0.5f, 0.3f);
        public override Color Color { get { return color; } }

        public override void SetItem(Block block)
        {
            throw new NotImplementedException();
        }
    }
}
