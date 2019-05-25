using Assets.Scripts.Game.Terrain.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.Terrain.Biomes
{
    internal class IceLand : Biome
    {
        internal static readonly string BiomeName = "IceLand";
        internal override string Name { get { return BiomeName; } }

        internal override string GetBlockByStratum(int stratum)
        {
            switch (stratum)
            {
                case 0: return Snow.blockName;
                case 1: return Dirt.blockName;
                default: return Air.blockName;
            }
        }
    }
}
