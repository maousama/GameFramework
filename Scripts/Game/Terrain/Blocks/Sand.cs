using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Blocks
{
    internal class Sand : Block
    {
        internal static readonly string blockName = "Sand";
        internal static readonly Color color = new Color(0.85f, 0.75f, 0.43f, 1);

        internal override string Name { get { return blockName; } }
        internal override BlockState State { get { return BlockState.Solid; } }

        internal override Color Color { get { return Color.yellow; } }
        internal override Material Material { get { return AssetsManager.AssetsAgent.GetAsset<Material>("Solid"); } }
    }
}
