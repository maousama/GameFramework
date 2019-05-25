using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Blocks
{
    internal class BaseStone : Block
    {
        internal static readonly string blockName = "BaseStone";
        internal override string Name { get { return blockName; } }
        internal override BlockState State { get { return BlockState.Solid; } }

        internal override Color Color { get { return Color.black; } }

        internal override Material Material { get { return AssetsManager.AssetsAgent.GetAsset<Material>("Solid"); } }
    }
}

