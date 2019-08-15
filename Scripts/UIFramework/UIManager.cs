using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UIFramework
{
    /// <summary>
    /// UI管理器
    /// UIFramework外观,对外提供操控UI的方法
    /// </summary>
    public class UIManager : MonoSingleton<UIManager>, IUIContainer
    {
        /// <summary>
        /// 子UI列表
        /// 管理根UI层级
        /// </summary>
        private List<UI> children = new List<UI>();
        /// <summary>
        /// 当前被选中UI
        /// </summary>
        private UI selectedUI;
        /// <summary>
        /// UIManager作为UI的根容器
        /// </summary>
        public Transform UIContainer { get { return transform; } }

        public List<UI> Children => throw new NotImplementedException();


        #region 需要功能: 打开,关闭,至顶部
        public UI Open(string uiName, IUIContainer container)
        {
            GameObject obj = AssetsAgent.GetGameObject(uiName, container.UIContainer);
            UI ui = obj.GetComponent<UI>();
            ui.UIStack.Push(ui);
            return ui;
        }

        public void Close(UI ui)
        {
            if (ui.UIContainer)
            {

            }
            if (ui.HasFrameStack())
            {
                UI[] array = ui.FrameStack.ToArray();
                ui.FrameStack.Clear();
                for (int i = 0; i < array.Length; i++) CloseFrame(array[i]);
            }
            FrameStack stack = ui.parentNode.FrameStack;
            bool topChange = (ui == stack.Peek());
            stack.Remove(ui);
            if (topChange) CurrentFocusFrame = FindFocusFrame(stack);
            AssetsAgent.DestroyGameObject(ui.gameObject);
        }
        public void CloseFrame(IFrameNode parentNode, int index)
        {
            if (parentNode != null) CloseFrame(parentNode.FrameStack.GetFrame(index));
        }
        public void SetFrameToTop(UI frame)
        {
            frame.parentNode.FrameStack.JumpToTop(frame);
            CurrentFocusFrame = FindFocusFrame(frame.parentNode.FrameStack);
        }
        public void SetFrameToTop(IFrameNode frameNode, int index)
        {
            frameNode.FrameStack.JumpToTop(index);
            CurrentFocusFrame = FindFocusFrame(frameNode.FrameStack);
        }

        public bool HasFrameStack() { return frameStack != null; }

        protected override void Init()
        {
            base.Init();
            InitFrameUICamera();
            InitEventSystem();
            DontDestroyOnLoad(gameObject);
        }

        private void InitEventSystem()
        {
            gameObject.AddComponent<EventSystem>();
            gameObject.AddComponent<StandaloneInputModule>();
        }

        private void InitFrameUICamera()
        {
            Camera uiCamera = gameObject.AddComponent<Camera>();
            uiCamera.clearFlags = CameraClearFlags.Depth;
            uiCamera.cullingMask = LayerMask.GetMask("UI");
            uiCamera.orthographic = true;
            uiCamera.orthographicSize = 5;
            uiCamera.nearClipPlane = -10;
            uiCamera.farClipPlane = 1000;
            uiCamera.depth = 100;

            UI.uiCamera = uiCamera;
        }
        private UI FindFocusFrame(FrameStack stack)
        {
            UI stackTop = stack.Peek();
            if (stackTop)
            {
                if (stackTop.HasFrameStack() && stackTop.FrameStack.Count > 0) return FindFocusFrame(stackTop.FrameStack);
                else return stackTop;
            }
            return null;
        }

    }
}
