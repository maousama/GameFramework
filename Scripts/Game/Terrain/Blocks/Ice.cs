using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Blocks
{
    internal class Ice : Block
    {
        internal static readonly string blockName = "Ice";
        internal static readonly Color color = new Color(0.2f, 0.6f, 1, 0.5f);

        internal override string Name { get { return blockName; } }
        internal override BlockState State { get { return BlockState.Crystal; } }

        internal override Color Color { get { return color; } }
        internal override Material Material { get { return AssetsManager.AssetsAgent.GetAsset<Material>("Crystal"); } }
    }

}
