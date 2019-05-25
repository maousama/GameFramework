using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.Game.RNG
{
    public class RandomNumberGenerator
    {
        private const int X_PRIME = 1619;
        private const int Y_PRIME = 31337;
        private const int Z_PRIME = 6971;

        public static int GetRandomValue(int seed, int x, int y, int z)
        {
            int value = seed;
            value ^= X_PRIME * x;
            value ^= Y_PRIME * y;
            value ^= Z_PRIME * z;
            value = value * value * value * 76277;
            value = (value >> 11) ^ value;

            return value;
        }
    }
}