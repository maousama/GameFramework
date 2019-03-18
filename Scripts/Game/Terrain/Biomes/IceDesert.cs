using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Biomes
{
    /// <summary>
    /// 冰沙漠
    /// </summary>
    public class IceDesert : Biome
    {
        private static IceDesert instance = null;
        public static IceDesert Instance
        {
            get
            {
                if (instance == null) instance = new IceDesert();
                return instance;
            }
        }
        private Color color = new Color(0.7f, 1, 1);
        public override Color Color { get { return color; } }

        public override void SetItem(Block block)
        {
            throw new System.NotImplementedException();
        }
    }
}
