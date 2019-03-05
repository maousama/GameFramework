using System;
using UnityEngine;

namespace Assets.Scripts.UIFramework
{
    [RequireComponent(typeof(Canvas))]
    public sealed class Frame : MonoBehaviour, IFrameNode
    {
        internal static Camera uiCamera;

        public Action<bool> OnFocusChange;

        internal IFrameNode parentNode;

        private Transform frameContainer;
        [SerializeField]
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
        public Transform FrameContainer
        {
            get
            {
                if (!frameContainer) frameContainer = new GameObject("TrameContainer").transform;
                return frameContainer;
            }
        }

        public bool HasFrameStack() { return framestack != null; }

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

        private void OnDestroy()
        {
            OnFocusChange = null;
        }
    }
}