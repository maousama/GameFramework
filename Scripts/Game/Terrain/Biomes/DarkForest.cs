using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Biomes
{
    /// <summary>
    /// 黑暗森林
    /// </summary>
    public class DarkForest : Biome
    {
       private static DarkForest instance = null;
        public static DarkForest Instance
        {
            get
            {
                if (instance == null) instance = new DarkForest();
                return instance;
            }
        }
        private Color color = new Color(0.3f, 0.2f, 0.2f);

        public override Color Color { get { return color; } }

        public override void SetItem(Block block)
        {
            throw new NotImplementedException();
        }
    }
}
