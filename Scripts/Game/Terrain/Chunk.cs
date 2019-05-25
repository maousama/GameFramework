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
        private Dictionary<string, BlocksRenderer> blockNameToBlocksRenderer = new Dictionary<string, BlocksRenderer>();

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
            //设置温度湿度生物群落信息
            SetTemperatureAndPrecipitationInfo();
            for (int z = -HalfLength; z < HalfLength; z++)
            {
                for (int x = -HalfLength; x < HalfLength; x++)
                {

                }
            }
        }
        /// <summary>
        /// 生成网格数据存储生成的温度湿度信息
        /// </summary>
        private void SetTemperatureAndPrecipitationInfo()
        {
            for (int z = -HalfLength; z < HalfLength; z++)
            {
                for (int x = -HalfLength; x < HalfLength; x++)
                {
                    Vector2Int vector2Int = new Vector2Int(position.x + x, position.z + z);
                    CoordinateInfo info = new CoordinateInfo();

                    info.precipitation = PerlinNoise.PerlinNoise2D(Map.Seed + 1, (vector2Int.x + 0.5f) * 0.003f, (vector2Int.y + 0.5f) * 0.003f);
                    info.baseTemperature = PerlinNoise.PerlinNoise2D(Map.Seed + 2, (vector2Int.x + 0.5f) * 0.003f, (vector2Int.y + 0.5f) * 0.003f);

                    info.biomeName = Biome.SelectBiome(info.baseTemperature, info.precipitation);

                    Map.Instance.coordinateInfoMap.Add(vector2Int, info);
                }
            }
        }



        /// <summary>
        /// 风蚀地形
        /// </summary>
        private void MakeErosion()
        {
            float v = 1 / HalfHeight;
            for (int y = 0; y < HalfHeight * 2; y++)
            {
                for (int z = -HalfLength; z < HalfLength; z++)
                {
                    for (int x = -HalfLength; x < HalfLength; x++)
                    {
                        Vector3 realPosition = position + new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
                        Vector2Int vector2Int = new Vector2Int(position.x + x, position.z + z);
                        //获取噪声值为-1至1;
                        float noiseValue = PerlinNoise.SuperimposedOctave3D(Map.Seed, realPosition.x * 0.017f, realPosition.y * 0.017f, realPosition.z * 0.017f);
                        //越高值越小
                        noiseValue = noiseValue - 2 + 2 * v * y;
                        if (noiseValue < 0) Map.Instance.coordinateInfoMap[vector2Int].blocks[y] = Air.blockName;
                    }
                }
            }
        }
        /// <summary>
        /// 生成底层:创建0.25halfHeight->1.5halfHeight;
        /// </summary>
        private void CreateStratums()
        {
            float stratumHeight = HalfHeight * 0.5f;
            for (int z = -HalfLength; z < HalfLength; z++)
            {
                for (int x = -HalfLength; x < HalfLength; x++)
                {
                    Vector2Int vector2Int = new Vector2Int(position.x + x, position.z + z);
                    float baseHeight = PerlinNoise.PerlinNoise2D(Map.Seed + 3, (vector2Int.x + 0.5f) * 0.003f, (vector2Int.y + 0.5f) * 0.003f);
                    baseHeight = (((baseHeight + 1) * 0.25f) + 0.5f) * stratumHeight;
                    CoordinateInfo coordinateInfo = Map.Instance.coordinateInfoMap[vector2Int];
                    coordinateInfo.blocks[0] = BaseStone.blockName;

                    int currentHeight = 0;
                    for (int y = 1; y < baseHeight; y++)
                    {
                        coordinateInfo.blocks[y] = BaseStone.blockName;
                        currentHeight = y + 1;
                    }

                    for (int stratum = 0; stratum < 2; stratum++)
                    {
                        float noise = PerlinNoise.PerlinNoise2D(Map.Seed + 4321 + stratum, (vector2Int.x + 0.5f) * 0.007f, (vector2Int.y + 0.5f) * 0.007f);
                        //0->0.5HalfHeight * 0.5层高
                        float h = (noise + 1) * 0.5f * stratumHeight;
                        float destHeight = currentHeight + h;
                        string curBlock = Biome.GetBiomeByName(coordinateInfo.biomeName).GetBlockByStratum(stratum);
                        for (int y = currentHeight; y < destHeight; y++)
                        {
                            coordinateInfo.blocks[y] = curBlock;
                            currentHeight = y;
                        }
                    }

                }
            }
        }

        /// <summary>
        /// 创建关联的方块
        /// </summary>
        private void CreateBlocks()
        {
            for (int y = 0; y < HalfHeight * 2 - 1; y++)
            {
                for (int z = -HalfLength - 1; z < HalfLength; z++)
                {
                    for (int x = -HalfLength - 1; x < HalfLength; x++)
                    {
                        CreateBlock(x, y, z);
                    }
                }
            }
        }
        /// <summary>
        /// 创建Block由CreateBlocks调用
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="batches"></param>
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
                    blocks[i] = Map.Instance.coordinateInfoMap[coordinateInfoKeys[i]].blocks[y];
                    blocks[i + 4] = Map.Instance.coordinateInfoMap[coordinateInfoKeys[i]].blocks[y + 1];
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
            //if (stateHash.Count == 1 && typeNameToIndexes.Keys.Count < 3 && (stateHash.Contains(BlockState.Solid) || stateHash.Contains(BlockState.None))) return;

            //绘制时候使用typeNameToIndexes对所有种类的方块进行绘制
            foreach (string blockName in typeNameToIndexes.Keys)
            {
                if (blockName == Air.blockName) continue;
                else if (Block.GetInstance(blockName).State == BlockState.Liquid && !stateHash.Contains(BlockState.None)) continue;

                if (!blockNameToBlocksRenderer.ContainsKey(blockName))
                {
                    GameObject blocksRendererGameObject = new GameObject(blockName);
                    blocksRendererGameObject.transform.SetParent(transform);
                    blocksRendererGameObject.transform.position = transform.position;

                    BlocksRenderer blocksRenderer = blocksRendererGameObject.AddComponent<BlocksRenderer>();

                    blockNameToBlocksRenderer.Add(blockName, blocksRenderer);
                }

                bool[] scalars = new bool[8];
                List<int> indexes = typeNameToIndexes[blockName];
                for (int i = 0; i < indexes.Count; i++) scalars[indexes[i]] = true;
                Vector3[] normals;
                Vector3[] vertices;
                int[] triangles;

                MarchingCubes.SetAllMeshInfo(scalars, out vertices, out triangles, out normals);

                GameObject blockGameObject = new GameObject(vector3Int + blockName);
                blockGameObject.transform.SetParent(blockNameToBlocksRenderer[blockName].transform);

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
            foreach (BlocksRenderer renderer in blockNameToBlocksRenderer.Values)
            {
                renderer.Draw();
            }
        }
    }
}

