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
        private Vector2Int position;

        public void Initialize(Chunk chunk, Vector2Int index)
        {
            this.chunk = chunk;
            this.index = index;
            transform.parent = chunk.transform;
            transform.localPosition = new Vector3(index.x + (0.5f - Chunk.sideLength * 0.5f), 0, index.y + (0.5f - Chunk.sideLength * 0.5f));

        }
    }
}
