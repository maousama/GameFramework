using System;
using UnityEngine;

namespace UIFramework
{
    [RequireComponent(typeof(Canvas))]
    public sealed class Frame : Component, IFrameNode
    {
        public static Camera uiCamera;

        [HideInInspector]
        public Canvas canvas;

        public Transform Transform
        {
            get
            {
                return transform;
            }
        }

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            if (canvas.renderMode == RenderMode.ScreenSpaceOverlay) throw new Exception("Canvas rendermode error!");
        }

        private void OnDestroy()
        {

        }

    }
}