using Assets.Scripts.Game.Terrain.Blocks;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    internal class BlockRenderer : MonoBehaviour
    {
        internal MeshFilter meshFilter;
        internal MeshRenderer meshRenderer;
        internal MeshCollider meshCollider;

        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            meshRenderer = GetComponent<MeshRenderer>();
            meshCollider = GetComponent<MeshCollider>();
        }

        internal void Draw()
        {
            CombineInstance[] combine = new CombineInstance[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                MeshFilter mF = transform.GetChild(i).GetComponent<MeshFilter>();
                combine[i].mesh = mF.sharedMesh;
                combine[i].transform = mF.transform.localToWorldMatrix;
            }
            meshFilter.mesh = new Mesh();
            meshFilter.mesh.CombineMeshes(combine);

            meshRenderer.material = Block.GetInstance(name).Material;

            meshCollider.sharedMesh = meshFilter.mesh;
        }
    }
}