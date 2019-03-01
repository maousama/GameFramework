using System;
using UnityEngine;

namespace UIFramework
{
    [RequireComponent(typeof(Canvas))]
    public sealed class Frame : MonoBehaviour, IFrameNode
    {
        internal static Camera uiCamera;

        internal IFrameNode parentNode;
        [SerializeField]
        private FrameStack framestack;
        [HideInInspector]
        private Canvas canvas;
        private Transform frameContainer;
        private Transform componentContainer;

        public Transform ComponentContainer
        {
            get
            {
                if (!componentContainer)
                {
                    componentContainer = transform.Find("ComponentContainer");
                    if (!componentContainer) Debug.LogError(name + " frame doesnt have component container but you try to get it !!");
                }
                return componentContainer;
            }
        }
        public FrameStack FrameStack
        {
            get
            {
                if (framestack == null) framestack = new FrameStack();
                return framestack;
            }
        }
        public Transform FrameContainer
        {
            get
            {
                if (!frameContainer) frameContainer = new GameObject("TrameContainer").transform;
                frameContainer.transform.SetAsLastSibling();
                return frameContainer;
            }
        }

        public bool IsFrameStackExist() { return framestack != null; }

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