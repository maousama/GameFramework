using Assets.Scripts.Game.Terrain.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain
{
    internal class Map : MonoSingleton<Map>
    {
        internal static int Seed = 1;
        internal static readonly int SeaLevel = -5;

        /// <summary>
        /// 网格信息Chunk产生时添加信息,Chunk被删除时候删除信息
        /// </summary>
        internal Dictionary<Vector2Int, CoordinateInfo> coordinateInfoMap = new Dictionary<Vector2Int, CoordinateInfo>();
        private Dictionary<Vector3Int, List<Block>> blockMap = new Dictionary<Vector3Int, List<Block>>();
        private Dictionary<Vector2Int, Chunk> chunkMap = new Dictionary<Vector2Int, Chunk>();


        internal bool ContainBlocks(Vector3Int position)
        {
            return blockMap.ContainsKey(position);
        }
        internal void AddBlock(Vector3Int position, Block block)
        {
            if (!blockMap.ContainsKey(position)) blockMap[position] = new List<Block>(4);
            blockMap[position].Add(block);
        }
        internal void RemoveBlocks(Vector3Int position)
        {
            if (blockMap.ContainsKey(position)) blockMap.Remove(position);
        }
        internal bool ContainChunk(Vector2Int index)
        {
            return chunkMap.ContainsKey(index);
        }

        protected override void Init()
        {
            Seed = UnityEngine.Random.Range(0, 9999);
            base.Init();
            for (int x = -3; x <= 3; x++)
            {
                for (int z = -3; z <= 3; z++)
                {
                    CreateChunk(new Vector2Int(x, z));
                }
            }

        }

        private void CreateChunk(Vector2Int index)
        {
            Chunk chunk = new GameObject("Chunk" + index).AddComponent<Chunk>();
            chunk.position = new Vector3Int(index.x * 2 * Chunk.HalfLength, 0, index.y * 2 * Chunk.HalfLength);
            chunk.index = index;
            chunk.Init();
            chunkMap.Add(index, chunk);
        }
    }
}
