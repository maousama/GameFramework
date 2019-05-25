using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.Terrain
{
    internal class RNG
    {
        private const int X_PRIME = 6547;
        private const int Y_PRIME = 24533;
        private const int Z_PRIME = 8411;

        internal static int Random2D(int x, int y, int seed)
        {
            int value = seed;
            value ^= X_PRIME * x;
            value ^= Y_PRIME * y;
            value = value * value * value * 7547;
            value = (value >> 11) ^ value;

            return value;
        }
        internal static int Random3D(int x, int y, int z, int seed)
        {
            int value = seed;
            value ^= X_PRIME * x;
            value ^= Y_PRIME * y;
            value ^= Z_PRIME * z;
            value = value * value * value * 7547;
            value = (value >> 11) ^ value;

            return value;
        }
    }
}
