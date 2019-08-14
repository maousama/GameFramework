using Assets.Scripts.Game.Terrain.Biomes;
using Assets.Scripts.Game.Terrain.Blocks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain
{
    internal class Chunk : MonoBehaviour
    {
        internal const int HalfHeight = 64;
        internal const int HalfLength = 8;

        internal Vector3Int position;
        internal Vector2Int index;
        private Dictionary<string, BlockRenderer> blockNameToBlockRenderer = new Dictionary<string, BlockRenderer>();

        /// <summary>
        /// 初始化
        /// </summary>
        internal void Init()
        {
            CreateCoordinateInfos();
            CreateBlocks();
            DrawBlocks();
        }
        /// <summary>
        /// 删除
        /// </summary>
        internal void Destroy()
        {
            DestroyLatticeInfos();
            DestroyBlocks();
            DestroyBlockRenderers();
        }

        /// <summary>
        /// 删除方块渲染器
        /// </summary>
        private void DestroyBlockRenderers()
        {
            foreach (var key in blockNameToBlockRenderer.Keys)
            {
                Destroy(blockNameToBlockRenderer[key]);
            }
            blockNameToBlockRenderer.Clear();
        }
        /// <summary>
        /// 删除关联的块
        /// </summary>
        private void DestroyBlocks()
        {
            for (int y = -HalfHeight; y < HalfHeight - 1; y++)
            {
                for (int z = -HalfLength - 1; z < HalfLength; z++)
                {
                    for (int x = -HalfLength - 1; x < HalfLength; x++)
                    {
                        Vector3Int vector3Int = position + new Vector3Int(x, y, z);
                        Map.Instance.RemoveBlocks(vector3Int);
                    }
                }
            }
        }
        /// <summary>
        /// 删除网格数据信息
        /// </summary>
        private void DestroyLatticeInfos()
        {

            for (int z = -HalfLength; z < HalfLength; z++)
            {
                for (int x = -HalfLength; x < HalfLength; x++)
                {
                    Vector2Int vector2Int = new Vector2Int(position.x + x, position.z + z);
                    //把数据存储到自定义格式文件中方便今后读取
                    //...(未实现)

                    //删除关联网格数据
                    Map.Instance.coordinateInfoMap.Remove(vector2Int);
                }
            }
        }

        /// <summary>
        /// 创建网格数据
        /// </summary>
        private void CreateCoordinateInfos()
        {
            for (int z = -HalfLength; z < HalfLength; z++)
            {
                for (int x = -HalfLength; x < HalfLength; x++)
                {
                    Vector2Int vector2Int = new Vector2Int(position.x + x, position.z + z);

                    CoordinateInfo info = new CoordinateInfo();
                    info.position = vector2Int;
                    info.precipitation = PerlinNoise.PerlinNoise2D(Map.Seed + 1, (vector2Int.x + 0.5f) * 0.003f, (vector2Int.y + 0.5f) * 0.003f);
                    info.baseTemperature = PerlinNoise.PerlinNoise2D(Map.Seed + 2, (vector2Int.x + 0.5f) * 0.003f, (vector2Int.y + 0.5f) * 0.003f);

                    info.biomeName = Biome.SelectBiome(info.baseTemperature, info.precipitation);
                    Biome currentBoime = Biome.GetBiomeByName(info.biomeName);

                    currentBoime.SetStratum(info);

                    Map.Instance.coordinateInfoMap.Add(vector2Int, info);
                }
            }
        }
        /// <summary>
        /// 创建方块
        /// </summary>
        private void CreateBlocks()
        {
            for (int z = -HalfLength - 1; z < HalfLength; z++)
            {
                for (int x = -HalfLength - 1; x < HalfLength; x++)
                {
                    for (int y = 0; y < HalfHeight * 2 - 1; y++)
                    {
                        CreateBlock(x, y, z);
                    }
                }
            }
        }
        private void CreateBlock(int x, int y, int z)
        {
            Vector3Int vector3Int = position + new Vector3Int(x, y, z);
            string[] blocks = new string[8];
            Vector2Int[] coordinateInfoKeys = new Vector2Int[4];

            coordinateInfoKeys[0] = new Vector2Int(vector3Int.x, vector3Int.z);
            coordinateInfoKeys[1] = new Vector2Int(vector3Int.x, vector3Int.z + 1);
            coordinateInfoKeys[2] = new Vector2Int(vector3Int.x + 1, vector3Int.z + 1);
            coordinateInfoKeys[3] = new Vector2Int(vector3Int.x + 1, vector3Int.z);

            //信息不完整时跳出本次
            for (int i = 0; i < coordinateInfoKeys.Length; i++)
            {
                if (!Map.Instance.coordinateInfoMap.ContainsKey(coordinateInfoKeys[i])) return;
                else
                {
                    blocks[i] = Map.Instance.coordinateInfoMap[coordinateInfoKeys[i]].GetBlock(y);
                    blocks[i + 4] = Map.Instance.coordinateInfoMap[coordinateInfoKeys[i]].GetBlock(y + 1);
                }
            }

            //如果都是同一种状态方块则不进行绘制
            Dictionary<string, List<int>> typeNameToIndexes = new Dictionary<string, List<int>>();
            HashSet<BlockState> stateHash = new HashSet<BlockState>();
            for (int i = 0; i < blocks.Length; i++)
            {
                if (typeNameToIndexes.ContainsKey(blocks[i]))
                {
                    typeNameToIndexes[blocks[i]].Add(i);
                }
                else
                {
                    typeNameToIndexes.Add(blocks[i], new List<int>());
                    typeNameToIndexes[blocks[i]].Add(i);
                }

                BlockState state = Block.GetInstance(blocks[i]).State;
                if (!stateHash.Contains(state)) stateHash.Add(state);
            }
            //当方块信息均为固体时候不进行绘制(普通固体不透明)
            if (stateHash.Count == 1 && stateHash.Contains(BlockState.Solid)) return;

            //绘制时候使用typeNameToIndexes对所有种类的方块进行绘制
            foreach (string blockName in typeNameToIndexes.Keys)
            {
                if (blockName == Air.blockName) continue;
                else if (Block.GetInstance(blockName).State == BlockState.Liquid && !stateHash.Contains(BlockState.None)) continue;

                if (!blockNameToBlockRenderer.ContainsKey(blockName))
                {
                    GameObject blockRendererGameObject = new GameObject(blockName);
                    blockRendererGameObject.transform.SetParent(transform);
                    blockRendererGameObject.transform.position = transform.position;

                    BlockRenderer blocksRenderer = blockRendererGameObject.AddComponent<BlockRenderer>();

                    blockNameToBlockRenderer.Add(blockName, blocksRenderer);
                }

                bool[] scalars = new bool[8];
                List<int> indexes = typeNameToIndexes[blockName];
                for (int i = 0; i < indexes.Count; i++) scalars[indexes[i]] = true;
                Vector3[] normals;
                Vector3[] vertices;
                int[] triangles;

                MarchingCubes.SetAllMeshInfo(scalars, out vertices, out triangles, out normals);

                GameObject blockGameObject = new GameObject(vector3Int + blockName);
                blockGameObject.transform.SetParent(blockNameToBlockRenderer[blockName].transform);

                Block block = (Block)blockGameObject.AddComponent(Block.GetType(blockName));
                block.Init(this, vector3Int, vertices, normals, triangles);

                Map.Instance.AddBlock(vector3Int, block);
            }
        }
        /// <summary>
        /// 绘制所有Block
        /// </summary>
        private void DrawBlocks()
        {
            foreach (BlockRenderer renderer in blockNameToBlockRenderer.Values)
            {
                renderer.Draw();
            }
        }
    }
}

