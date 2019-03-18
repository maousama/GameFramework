using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Biomes
{
    /// <summary>
    /// 寒冷沙漠
    /// </summary>
    class ColdDesert : Biome
    {
        private static ColdDesert instance = null;
        public static ColdDesert Instance
        {
            get
            {
                if (instance == null) instance = new ColdDesert();
                return instance;
            }
        }
        public override Color Color { get { return Color.white; } }

        public override void SetItem(Block block)
        {
            throw new NotImplementedException();
        }
    }
}
