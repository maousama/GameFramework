using System;
using UnityEngine;

namespace UIFramework
{
    [RequireComponent(typeof(Canvas))]
    internal sealed class Frame : MonoBehaviour
    {
        public static Camera uiCamera;

        [HideInInspector]
        public Canvas canvas;

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