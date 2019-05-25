using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Terrain.Blocks
{
    [RequireComponent(typeof(MeshFilter))]
    internal abstract class Block : MonoBehaviour
    {
        /// <summary>
        /// 使用反射初始化nameToType字典
        /// </summary>
        static Block()
        {
            var types = from type in Assembly.GetExecutingAssembly().GetTypes()
                        where type.IsClass && type.IsSubclassOf(typeof(Block)) && !type.IsAbstract
                        select type;
            types.ToList().ForEach((type) =>
            {
                FieldInfo blockNameFieldInfo = type.GetField("blockName", BindingFlags.NonPublic | BindingFlags.Static);
                string name = blockNameFieldInfo.GetValue(null).ToString();
                nameToType.Add(name, type);
            });
        }

        private static Dictionary<string, Type> nameToType = new Dictionary<string, Type>();
        private static Dictionary<string, Block> nameToInstance = null;
        private static GameObject instanceGameObject;

        internal static Type GetType(string name) { return nameToType[name]; }
        internal static Block GetInstance(string name)
        {
            if (nameToInstance == null)
            {
                if (!instanceGameObject)
                {
                    instanceGameObject = new GameObject("BlockInstancesRoot");
                    DontDestroyOnLoad(instanceGameObject);
                }
                nameToInstance = new Dictionary<string, Block>();
                foreach (string key in nameToType.Keys)
                {
                    Block instance = (Block)instanceGameObject.AddComponent(nameToType[key]);
                    nameToInstance.Add(key, instance);
                }
            }
            return nameToInstance[name];
        }

        /// <summary>
        /// 类名,重写时候使用子类类名会在初始化时反射一次自动生成查找表
        /// </summary>
        internal abstract string Name { get; }
        internal abstract BlockState State { get; }
        internal abstract Color Color { get; }
        internal abstract Material Material { get; }

        /// <summary>
        /// 父节点
        /// </summary>
        internal Chunk chunk;
        internal Vector3Int position;
        internal MeshFilter meshFilter;

        internal void Init(Chunk chunk, Vector3Int position, Vector3[] vertices, Vector3[] normals, int[] triangles)
        {
            meshFilter = GetComponent<MeshFilter>();

            this.chunk = chunk;
            this.position = position;
            transform.position = position;

            meshFilter.sharedMesh = new Mesh();
            meshFilter.sharedMesh.name = "Block Mesh";
            meshFilter.sharedMesh.vertices = vertices;
            meshFilter.sharedMesh.normals = normals;
            meshFilter.sharedMesh.triangles = triangles;
            Color[] colors = new Color[triangles.Length];
            for (int i = 0; i < colors.Length; i++) colors[i] = Color;
            meshFilter.sharedMesh.colors = colors;
        }
    }

    /// <summary>
    /// Block状态区分液体固体与晶体
    /// </summary>
    internal enum BlockState
    {
        None,
        Solid,
        Liquid,
        Crystal,
    }
}
