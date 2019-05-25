using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Blocks
{
    internal class Dirt : Block
    {
        internal static readonly string blockName = "Dirt";
        internal static readonly Color color = new Color(0.5f, 0.35f, 0.2f, 1);

        internal override string Name { get { return blockName; } }
        internal override BlockState State { get { return BlockState.Solid; } }

        internal override Color Color { get { return color; } }
        internal override Material Material { get { return AssetsManager.AssetsAgent.GetAsset<Material>("Solid"); } }
    }
}