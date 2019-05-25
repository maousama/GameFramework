using System;
using System.IO;
using UnityEngine;
namespace Assets.Scripts.Game.Terrain
{
    public class PerlinNoise
    {
        private static readonly Vector2Int[] GRAD_2D =
        {
            new Vector2Int(-1,-1), new Vector2Int( 1,-1), new Vector2Int(-1, 1), new Vector2Int( 1, 1),
            new Vector2Int( 0,-1), new Vector2Int(-1, 0), new Vector2Int( 0, 1), new Vector2Int( 1, 0),
        };

        private static readonly Vector3Int[] GRAD_3D =
        {
            new Vector3Int( 1, 1, 0), new Vector3Int(-1, 1, 0), new Vector3Int( 1,-1, 0), new Vector3Int(-1,-1, 0),
            new Vector3Int( 1, 0, 1), new Vector3Int(-1, 0, 1), new Vector3Int( 1, 0,-1), new Vector3Int(-1, 0,-1),
            new Vector3Int( 0, 1, 1), new Vector3Int( 0,-1, 1), new Vector3Int( 0, 1,-1), new Vector3Int( 0,-1,-1),
            new Vector3Int( 1, 1, 0), new Vector3Int( 0,-1, 1), new Vector3Int(-1, 1, 0), new Vector3Int( 0,-1,-1),
        };

        private const int X_PRIME = 1619;
        private const int Y_PRIME = 31337;
        private const int Z_PRIME = 6971;

        private static float Lerp(float a, float b, float t)
        {
            t = t * t * t * (6 * t * t - 15 * t + 10);
            return a + (b - a) * t;
        }


        private static Vector2Int GetGradient2D(int x, int y, int seed)
        {
            int hash = seed;
            hash ^= X_PRIME * x;
            hash ^= Y_PRIME * y;
            hash = hash * hash * hash * 46853;
            hash = (hash >> 13) ^ hash;

            int index = hash % 8;
            if (index < 0) index += 8;
            return GRAD_2D[index];
        }

        private static Vector3Int GetGradient3D(int x, int y, int z, int seed)
        {
            int hash = seed;
            hash ^= X_PRIME * x;
            hash ^= Y_PRIME * y;
            hash ^= Z_PRIME * z;
            hash = hash * hash * hash * 46853;
            hash = (hash >> 13) ^ hash;

            int index = hash % 16;
            if (index < 0) index += 16;
            return GRAD_3D[index];
        }

        private static float DotGridGradient2D(int ix, int iy, float x, float y, int seed)
        {
            float dx = x - ix;
            float dy = y - iy;

            Vector2Int gradient = GetGradient2D(ix, iy, seed);
            return dx * gradient.x + dy * gradient.y;
        }

        private static float DotGridGradient3D(int ix, int iy, int iz, float x, float y, float z, int seed)
        {
            float dx = x - ix;
            float dy = y - iy;
            float dz = z - iz;

            Vector3Int gradient = GetGradient3D(ix, iy, iz, seed);
            return dx * gradient.x + dy * gradient.y + dz * gradient.z;
        }

        public static float PerlinNoise2D(int seed, float x, float y)
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
            n0 = DotGridGradient2D(x0, y0, x, y, seed);
            n1 = DotGridGradient2D(x1, y0, x, y, seed);
            ix0 = Lerp(n0, n1, sx);
            n0 = DotGridGradient2D(x0, y1, x, y, seed);
            n1 = DotGridGradient2D(x1, y1, x, y, seed);
            ix1 = Lerp(n0, n1, sx);
            value = Lerp(ix0, ix1, sy);

            return value;
        }

        public static float PerlinNoise3D(int seed, float x, float y, float z)
        {
            // Determine grid cell coordinates
            int x0 = Mathf.FloorToInt(x);
            int x1 = x0 + 1;
            int y0 = Mathf.FloorToInt(y);
            int y1 = y0 + 1;
            int z0 = Mathf.FloorToInt(z);
            int z1 = z0 + 1;
            // Determine interpolation weights
            // Could also use higher order polynomial/s-curve here
            float sx = x - x0;
            float sy = y - y0;
            float sz = z - z0;
            // Interpolate between grid point gradients
            float xf00 = Lerp(DotGridGradient3D(x0, y0, z0, x, y, z, seed), DotGridGradient3D(x1, y0, z0, x, y, z, seed), sx);
            float xf10 = Lerp(DotGridGradient3D(x0, y1, z0, x, y, z, seed), DotGridGradient3D(x1, y1, z0, x, y, z, seed), sx);
            float xf01 = Lerp(DotGridGradient3D(x0, y0, z1, x, y, z, seed), DotGridGradient3D(x1, y0, z1, x, y, z, seed), sx);
            float xf11 = Lerp(DotGridGradient3D(x0, y1, z1, x, y, z, seed), DotGridGradient3D(x1, y1, z1, x, y, z, seed), sx);

            float yf0 = Lerp(xf00, xf10, sy);
            float yf1 = Lerp(xf01, xf11, sy);

            return Lerp(yf0, yf1, sz);
        }

        public static float SuperimposedOctave3D(int seed, float x, float y, float z, int superposition = 1)
        {
            float result = 0;
            superposition = superposition < 0 ? 0 : superposition;
            for (int i = 0; i < superposition; i++)
            {
                float nIthPower = Mathf.Pow(2, i);
                result += PerlinNoise3D(seed + i, nIthPower * x, nIthPower * y, nIthPower * z) * Mathf.Pow(0.5f, i);
            }
            return result;
        }


        public static float SuperimposedOctave2D(int seed, float x, float y, int superposition = 1)
        {
            float result = 0;
            superposition = superposition < 0 ? 0 : superposition;
            for (int i = 0; i < superposition; i++)
            {
                float nIthPower = Mathf.Pow(2, i);
                result += PerlinNoise2D(seed + i, nIthPower * x, nIthPower * y) * Mathf.Pow(0.5f, i);
            }
            return result;
        }

    }
}