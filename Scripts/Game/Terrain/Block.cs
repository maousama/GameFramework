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
        private BiomeType biomeType;

        public Color color;

        private Chunk chunk;
        private Vector2Int index;

        public void Initialize(Chunk chunk, Vector2Int index)
        {
            //Set gameObject
            this.chunk = chunk;
            this.index = index;
            transform.parent = chunk.transform;
            transform.localPosition = new Vector3(index.x + (0.5f - Chunk.halfSideLength), 0, index.y + (0.5f - Chunk.halfSideLength));

            MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

            meshRenderer.materials = new Material[] { AssetsAgent.GetAsset<Material>("BlockMaterial") };
            meshFilter.mesh = new Mesh();
            meshFilter.mesh.name = "Block Mesh";

            //Set traingles and vertices
            int floorx = Mathf.FloorToInt(transform.position.x);
            int ceilx = Mathf.CeilToInt(transform.position.x);
            int floorz = Mathf.FloorToInt(transform.position.z);
            int ceilz = Mathf.CeilToInt(transform.position.z);
            Vector3[] vertices = new Vector3[4];
            vertices[0] = new Vector3(-0.5f, chunk.GetHeight(new Vector2Int(floorx, floorz)), -0.5f);
            vertices[1] = new Vector3(-0.5f, chunk.GetHeight(new Vector2Int(floorx, ceilz)), 0.5f);
            vertices[2] = new Vector3(0.5f, chunk.GetHeight(new Vector2Int(ceilx, ceilz)), 0.5f);
            vertices[3] = new Vector3(0.5f, chunk.GetHeight(new Vector2Int(ceilx, floorz)), -0.5f);
            int[] traingles = new int[6] { 0, 1, 2, 2, 3, 0 };
            meshFilter.mesh.vertices = vertices;
            meshFilter.mesh.triangles = traingles;

            //Set height
            float allHeight = 0;
            for (int i = 0; i < vertices.Length; i++) allHeight += vertices[i].y;
            height = allHeight * 0.25f;

            //Set temperature and precipitation,range 0 -> 100
            baseTemperature = PerlinNoise.SuperimposedOctave(MapGenerator.seed - 1, transform.position.x * 0.03f, transform.position.z * 0.03f) * 37.5f + 50f;
            precipitationPercentage = PerlinNoise.SuperimposedOctave(MapGenerator.seed - 2, transform.position.x * 0.03f, transform.position.z * 0.03f) * 37.5f + 50f;

            temperature = baseTemperature - height * 0.1f;
            precipitation = temperature * 0.01f * precipitationPercentage;

            biomeType = Biome.BiomeSelector(temperature, precipitationPercentage);

            color = Biome.GetColor(biomeType);
            Color[] colors = new Color[4] { color, color, color, color };
            meshFilter.mesh.colors = colors;

            meshFilter.mesh.RecalculateNormals();
        }



    }
}
