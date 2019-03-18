using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Item
{
    public class Grass : Item
    {
        private void Awake()
        {
            MeshFilter meshFilte = gameObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshFilte.mesh = new Mesh();
            meshFilte.mesh.name = "Grass";
            meshRenderer.sharedMaterial = AssetsManager.AssetsAgent.GetAsset<Material>("Grass");
        }
    }
}
