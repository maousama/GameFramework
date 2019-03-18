using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Biomes
{
    /// <summary>
    /// 沙漠
    /// </summary>
    public class Desert : Biome
    {
        private static Desert instance = null;
        public static Desert Instance
        {
            get
            {
                if (instance == null) instance = new Desert();
                return instance;
            }
        }
        private Color color = new Color(0.8f, 0.8f, 0.6f);

        public override Color Color { get { return color; } }

        public override void SetItem(Block block)
        {
            throw new NotImplementedException();
        }
    }
}
