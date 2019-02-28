using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIFramework
{
    [RequireComponent(typeof(Canvas))]
    public sealed class Frame : MonoBehaviour, IFrameNode
    {
        internal static Camera uiCamera;

        internal IFrameNode parentNode;
        private FrameStack framestack;
        [HideInInspector]
        private Canvas canvas;


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
        internal void OnIndexChange(int newIndex)
        {
            //根节点交换位置不能改变层级必须通过SortingOrder
            if (canvas.isRootCanvas)
            {
                canvas.sortingOrder = newIndex * 10;
            }
            //子节点默认根据SublingIndex决定其顺序,如果你想重写SortingOrder来决定顺序也可以.
            else
            {
                transform.SetSiblingIndex(newIndex);
            }
        }

        private void Awake()
        {
            canvas = GetComponent<Canvas>();

            canvas.worldCamera = uiCamera;
        }
        private void Start()
        {
            if (canvas.renderMode == RenderMode.ScreenSpaceOverlay) throw new Exception("Canvas rendermode error!");
        }
    }
}