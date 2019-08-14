using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Blocks
{
    internal class Air : Block
    {
        internal static readonly string blockName = "Air";
        internal override string Name { get { return blockName; } }
        internal override BlockState State { get { return BlockState.None; } }

        internal override Color Color => throw new NotImplementedException();
        internal override Material Material => throw new NotImplementedException();
    }
}
