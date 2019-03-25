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

        private void Awake()
        {
            SetBlockInfo();
        }

        private void Start()
        {
            DrawMesh();
        }


        private void SetHeight()
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

        private void SetNeightbours()
        {
            Vector3 position = transform.position;
            Vector2Int thisPos = new Vector2Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.z));
            Vector2Int southwestPos = new Vector2Int(thisPos.x - 1, thisPos.y - 1);
            Vector2Int southPos = new Vector2Int(thisPos.x, thisPos.y - 1);
            Vector2Int southeastPos = new Vector2Int(thisPos.x + 1, thisPos.y - 1);
            Vector2Int westPos = new Vector2Int(thisPos.x - 1, thisPos.y);
            Vector2Int eastPos = new Vector2Int(thisPos.x + 1, thisPos.y);
            Vector2Int northwestPos = new Vector2Int(thisPos.x - 1, thisPos.y + 1);
            Vector2Int northPos = new Vector2Int(thisPos.x, thisPos.y);
            Vector2Int northeastPos = new Vector2Int(thisPos.x + 1, thisPos.y + 1);

            int directionCount = 8;
            neighbours = new Dictionary<Direction, Block>(directionCount);
            neighbours.Add(Direction.Southwest, MapGenerator.blockMap[southwestPos]);

        }

        public void SetBlockInfo()
        {



            //Set temperature and precipitation,range 0 -> 100
            baseTemperature = PerlinNoise.SuperimposedOctave(MapGenerator.seed - 1, transform.position.x * 0.0003f, transform.position.z * 0.0003f) * 37.5f + 50f;
            precipitationPercentage = PerlinNoise.SuperimposedOctave(MapGenerator.seed - 2, transform.position.x * 0.0003f, transform.position.z * 0.0003f) * 37.5f + 50f;

            temperature = baseTemperature - height * 0.1f;
            precipitation = temperature * 0.01f * precipitationPercentage;

            biome = Biome.BiomeSelector(temperature, precipitationPercentage, height);
        }


        public void DrawMesh()
        {
            //Set color after SetBlockInfo


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
