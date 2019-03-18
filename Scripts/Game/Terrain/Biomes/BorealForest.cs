using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Biomes
{
    /// <summary>
    /// 针叶林
    /// </summary>
    public class BorealForest : Biome
    {
        private static BorealForest instance = null;
        public static BorealForest Instance
        {
            get
            {
                if (instance == null) instance = new BorealForest();
                return instance;
            }
        }
        private Color color = new Color(0.4f, 0.25f, 0.25f);
        public override Color Color { get { return color; } }

        public override void SetItem(Block block)
        {
            throw new NotImplementedException();
        }
    }
}
