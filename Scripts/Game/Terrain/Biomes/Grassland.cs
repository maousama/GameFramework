using Assets.Scripts.Game.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Biomes
{
    /// <summary>
    /// 草原
    /// </summary>
    public class Grassland : Biome
    {
        private static Grassland instance = null;
        public static Grassland Instance
        {
            get
            {
                if (instance == null) instance = new Grassland();
                return instance;
            }
        }
        private Color color = new Color(0.5f, 0.5f, 0.25f);

        public override Color Color { get { return color; } }

        public override void SetItem(Block block)
        {
            GameObject grass = new GameObject("Grass", typeof(Grass));
            grass.transform.SetParent(block.transform);
            grass.transform.localPosition = Vector3.up * block.Height;
        }
    }
}
