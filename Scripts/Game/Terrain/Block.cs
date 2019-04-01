using Assets.Scripts.Game.Terrain.Biomes;
using AssetsManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain
{

    public class Block : MonoBehaviour
    {
        /// <summary>
        /// 由地块xz坐标决定的基本温度
        /// </summary>
        private float baseTemperature;
        /// <summary>
        /// 降水量百分比
        /// </summary>
        private float precipitationPercentage;
        /// <summary>
        /// 高度:取四个点的平均值
        /// </summary>
        private float height;
        /// <summary>
        /// 温度:由海拔和基本温度决定
        /// </summary>
        private float temperature;
        /// <summary>
        /// 降水量:由百分比与最大降水量共同决定真实降水量
        /// </summary>
        private float precipitation;
        /// <summary>
        /// 生物群落类型
        /// </summary>
        private Biome biome;
        /// <summary>
        /// 邻居
        /// </summary>
        private Dictionary<Direction, Block> neighbours;

        private Vector3[] vertices;
        private int[] traingles;
        private Color[] colors;
        private bool canDraw = false;

        internal void SetHeight()
        {
            Vector3 position = transform.position;
            //Set traingles and vertices
            int floorx = Mathf.FloorToInt(position.x);
            int ceilx = Mathf.CeilToInt(position.x);
            int floorz = Mathf.FloorToInt(position.z);
            int ceilz = Mathf.CeilToInt(position.z);
            vertices = new Vector3[4];
            vertices[0] = new Vector3(-0.5f, MapGenerator.heightMap[new Vector2Int(floorx, floorz)], -0.5f);
            vertices[1] = new Vector3(-0.5f, MapGenerator.heightMap[new Vector2Int(floorx, ceilz)], 0.5f);
            vertices[2] = new Vector3(0.5f, MapGenerator.heightMap[new Vector2Int(ceilx, ceilz)], 0.5f);
            vertices[3] = new Vector3(0.5f, MapGenerator.heightMap[new Vector2Int(ceilx, floorz)], -0.5f);
            traingles = new int[6] { 0, 1, 2, 2, 3, 0 };

            //Set height
            float allHeight = 0;
            for (int i = 0; i < vertices.Length; i++) allHeight += vertices[i].y;
            height = allHeight * 0.25f;
        }

        internal void SetEnvironment()
        {
            //Set temperature and precipitation,range 0 -> 100
            baseTemperature = PerlinNoise.SuperimposedOctave(MapGenerator.seed - 1, transform.position.x * 0.003f, transform.position.z * 0.003f) * 37.5f + 50f;
            precipitationPercentage = PerlinNoise.SuperimposedOctave(MapGenerator.seed - 2, transform.position.x * 0.003f, transform.position.z * 0.003f) * 37.5f + 50f;

            temperature = baseTemperature - height * 0.1f;
            precipitation = temperature * 0.01f * precipitationPercentage;

            biome = Biome.BiomeSelector(temperature, precipitationPercentage, height);
        }

        internal void SetNeightbours()
        {
            Vector3 position = transform.position;
            Vector2Int thisPos = new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.z));
            Vector2Int southwestPos = new Vector2Int(thisPos.x - 1, thisPos.y - 1);
            Vector2Int southPos = new Vector2Int(thisPos.x, thisPos.y - 1);
            Vector2Int southeastPos = new Vector2Int(thisPos.x + 1, thisPos.y - 1);
            Vector2Int westPos = new Vector2Int(thisPos.x - 1, thisPos.y);
            Vector2Int eastPos = new Vector2Int(thisPos.x + 1, thisPos.y);
            Vector2Int northwestPos = new Vector2Int(thisPos.x - 1, thisPos.y + 1);
            Vector2Int northPos = new Vector2Int(thisPos.x, thisPos.y + 1);
            Vector2Int northeastPos = new Vector2Int(thisPos.x + 1, thisPos.y + 1);

            int directionCount = 8;
            neighbours = new Dictionary<Direction, Block>(directionCount);
            if (MapGenerator.blockMap.ContainsKey(southwestPos)) neighbours.Add(Direction.Southwest, MapGenerator.blockMap[southwestPos]);
            if (MapGenerator.blockMap.ContainsKey(southPos)) neighbours.Add(Direction.South, MapGenerator.blockMap[southPos]);
            if (MapGenerator.blockMap.ContainsKey(southeastPos)) neighbours.Add(Direction.Southeast, MapGenerator.blockMap[southeastPos]);
            if (MapGenerator.blockMap.ContainsKey(westPos)) neighbours.Add(Direction.West, MapGenerator.blockMap[westPos]);
            if (MapGenerator.blockMap.ContainsKey(eastPos)) neighbours.Add(Direction.East, MapGenerator.blockMap[eastPos]);
            if (MapGenerator.blockMap.ContainsKey(northwestPos)) neighbours.Add(Direction.Northwest, MapGenerator.blockMap[northwestPos]);
            if (MapGenerator.blockMap.ContainsKey(northPos)) neighbours.Add(Direction.North, MapGenerator.blockMap[northPos]);
            if (MapGenerator.blockMap.ContainsKey(northeastPos)) neighbours.Add(Direction.Northeast, MapGenerator.blockMap[northeastPos]);

            canDraw = neighbours.Count == 8;
            if (canDraw)
            {
                colors = new Color[4];
                colors[0] = (MapGenerator.blockMap[southwestPos].biome.Color + MapGenerator.blockMap[southPos].biome.Color + MapGenerator.blockMap[westPos].biome.Color + biome.Color) * 0.25f;
                colors[1] = (MapGenerator.blockMap[westPos].biome.Color + MapGenerator.blockMap[northwestPos].biome.Color + MapGenerator.blockMap[northPos].biome.Color + biome.Color) * 0.25f;
                colors[2] = (MapGenerator.blockMap[northPos].biome.Color + MapGenerator.blockMap[northeastPos].biome.Color + MapGenerator.blockMap[eastPos].biome.Color + biome.Color) * 0.25f;
                colors[3] = (MapGenerator.blockMap[eastPos].biome.Color + MapGenerator.blockMap[southPos].biome.Color + MapGenerator.blockMap[southeastPos].biome.Color + biome.Color) * 0.25f;
            }
        }

        internal void DrawMesh()
        {
            //Set color after SetBlockInfo
            if (canDraw)
            {
                MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
                MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

                meshRenderer.sharedMaterial = AssetsAgent.GetAsset<Material>("BlockMaterial");
                meshFilter.mesh = new Mesh();
                meshFilter.mesh.name = "Block Mesh";

                meshFilter.mesh.vertices = vertices;
                meshFilter.mesh.triangles = traingles;

                meshFilter.mesh.colors = colors;
                meshFilter.mesh.RecalculateNormals();
            }

        }
    }
}
