using System;
using UnityEngine;
namespace Assets.Scripts.Game.Terrain
{
    public class PerlinNoise
    {
        private static int[][] unitGradients = new int[][]
        {
        new int[]{ 1,2},
        new int[]{ 1,-2},
        new int[]{ -1,2},
        new int[]{ -1,-2},
        new int[]{ 2,1},
        new int[]{ 2,-1},
        new int[]{ -2,1},
        new int[]{ -2,-1},
        };

        private static float Lerp(float a, float b, float t)
        {
            t = t * t * t * (6 * t * t - 15 * t + 10);
            return a + (b - a) * t;
        }

        private static float DotGridGrandient(int ix, int iy, float x, float y, int seed)
        {
            float dx = x - ix;
            float dy = y - iy;

            int[] gradient = GetGradient(ix, iy, seed);
            //Debug.Log(dx + "|" + dy + "{}{}" + (dx * gradient[0] + dy * gradient[1]));
            return dx * gradient[0] + dy * gradient[1];
        }

        private static int[] GetGradient(int ix, int iy, int seed)
        {
            int randomNum = seed * ix * iy - ix * 7 - iy * ix - seed;
            int index = randomNum % 8;

            index = index < 0 ? index + 8 : index;
            return unitGradients[index];
        }

        private static float Perlin(float x, float y, int seed)
        {
            // Determine grid cell coordinates
            int x0 = Mathf.FloorToInt(x);
            int x1 = x0 + 1;
            int y0 = Mathf.FloorToInt(y);
            int y1 = y0 + 1;
            // Determine interpolation weights
            // Could also use higher order polynomial/s-curve here
            float sx = x - x0;
            float sy = y - y0;
            // Interpolate between grid point gradients
            float n0, n1, ix0, ix1, value;
            n0 = DotGridGrandient(x0, y0, x, y, seed);
            n1 = DotGridGrandient(x1, y0, x, y, seed);
            ix0 = Lerp(n0, n1, sx);
            n0 = DotGridGrandient(x0, y1, x, y, seed);
            n1 = DotGridGrandient(x1, y1, x, y, seed);
            ix1 = Lerp(n0, n1, sx);
            value = Lerp(ix0, ix1, sy);

            return value;
        }

        public static float SuperimposedOctave(int seed, float x, float y, int superposition = 1)
        {
            float result = 0;
            superposition = superposition < 0 ? 0 : superposition;
            for (int i = 0; i < superposition; i++)
            {
                float nIthPower = Mathf.Pow(2, i);
                result += Perlin(nIthPower * x, nIthPower * y, seed) * Mathf.Pow(0.5f, i);
            }
            return result;
        }


    }
}