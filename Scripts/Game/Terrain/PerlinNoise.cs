﻿using UnityEngine;
namespace Assets.Scripts.Game.Terrain
{
    public class PerlinNoise
    {
        private int seed = 0;

        private int[][] unitGradients = new int[][]
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

        public int Seed { get => seed; set => seed = value; }

        private float Lerp(float a, float b, float t)
        {
            t = t * t * t * (6 * t * t - 15 * t + 10);
            return a + (b - a) * t;
        }

        private float DotGridGrandient(int ix, int iy, float x, float y)
        {
            float dx = x - ix;
            float dy = y - iy;

            int[] gradient = GetGradient(ix, iy);
            return dx * gradient[0] + dy * gradient[1];
        }

        private int[] GetGradient(int ix, int iy)
        {
            int randomNum = (Seed * iy + ix * (iy * ix * 60493 - Seed + 19990303) - (353 * Seed) + 1376312589 - 233 * iy) & 0x7fffffff;
            int index = randomNum % 8;

            index = index < 0 ? index + 8 : index;
            return unitGradients[index];
        }

        private float Perlin(float x, float y)
        {
            // Determine grid cell coordinates
            int x0 = (int)x;
            int x1 = x0 + 1;
            int y0 = (int)y;
            int y1 = y0 + 1;
            // Determine interpolation weights
            // Could also use higher order polynomial/s-curve here
            float sx = x - x0;
            float sy = y - y0;
            // Interpolate between grid point gradients
            float n0, n1, ix0, ix1, value;
            n0 = DotGridGrandient(x0, y0, x, y);
            n1 = DotGridGrandient(x1, y0, x, y);
            ix0 = Lerp(n0, n1, sx);
            n0 = DotGridGrandient(x0, y1, x, y);
            n1 = DotGridGrandient(x1, y1, x, y);
            ix1 = Lerp(n0, n1, sx);
            value = Lerp(ix0, ix1, sy);

            return value;
        }

        public float SuperimposedOctave(float x, float y, int superposition = 1)
        {
            float result = 0;
            superposition = superposition < 0 ? 0 : superposition;
            for (int i = 0; i < superposition; i++)
            {
                float nIthPower = Mathf.Pow(2, i);
                result += Perlin(nIthPower * x, nIthPower * y) * Mathf.Pow(0.5f, i);
            }
            return result;
        }
    }
}