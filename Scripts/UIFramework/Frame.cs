using System;
using UnityEngine;

namespace Assets.Scripts.UIFramework
{
    [RequireComponent(typeof(Canvas))]
    public sealed class Frame : MonoBehaviour, IFrameNode
    {
        internal static Camera uiCamera;

        public Action OnSetToTop;
        public Action OnRemoveFromTop;

        internal IFrameNode parentNode;
        [SerializeField]
        private FrameStack framestack;
        [HideInInspector]
        private Canvas canvas;
        private Transform frameContainer;

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
                return frameContainer;
            }
        }

        public bool IsFrameStackExist() { return framestack != null; }

        /// <summary>
        /// 在Frame于栈中的索引改变时调用
        /// </summary>
        internal void SetSortingOrder(int newIndex)
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
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera) canvas.worldCamera = uiCamera;
        }
    }
}