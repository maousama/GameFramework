using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIFramework
{
    [RequireComponent(typeof(Canvas))]
    public sealed class Frame : MonoBehaviour, IFrameNode
    {
        internal static Camera uiCamera;

        internal FrameStack framestack;
        [HideInInspector]
        internal Canvas canvas;

        private IFrameNode parent;

        public FrameStack FrameStack
        {
            get
            {
                if (framestack == null) framestack = new FrameStack();
                return framestack;
            }
        }
        public Transform Transform { get { return transform; } }

        /// <summary>
        /// 在Frame于栈中的索引改变时调用
        /// </summary>
        internal void OnIndexInStackChange()
        {
            canvas.
        }

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            if (canvas.renderMode == RenderMode.ScreenSpaceOverlay) throw new Exception("Canvas rendermode error!");
            canvas.worldCamera = uiCamera;
        }
        private void OnDestroy()
        {
            parent.FrameStack.Remove(this);
        }

    }
}