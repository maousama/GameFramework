﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Game.Terrain.Blocks;

namespace Assets.Scripts.Game.Terrain.Biomes
{
    internal class GrassLand : Biome
    {
        internal static readonly string BiomeName = "GrassLand";
        internal override string Name { get { return BiomeName; } }

        internal override string GetBlockByStratum(int stratum)
        {
            switch (stratum)
            {
                case 0: return Stone.blockName;
                case 1: return Dirt.blockName;
                default: return Air.blockName;
            }
        }
    }
}
