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
        private Chunk chunk;

        private Vector2Int index;

        public void Initialize(Chunk chunk, Vector2Int index)
        {
            this.chunk = chunk;
            this.index = index;
            transform.parent = chunk.transform;
            transform.localPosition = new Vector3(index.x + (0.5f - Chunk.halfSideLength), 0, index.y + (0.5f - Chunk.halfSideLength));

            MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

            meshRenderer.materials = new Material[] { AssetsAgent.GetAsset<Material>("BlockMaterial") };
            meshFilter.mesh = new Mesh();
            meshFilter.mesh.name = "Block Mesh";


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


            meshFilter.mesh.RecalculateNormals();
        }

    }
}
