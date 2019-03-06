using UnityEngine;

namespace Assets.Scripts.UIFramework
{
    /// <summary>
    /// 一个单例物体负责管理Frame供外部使用同时作为Frame的根节点使用
    /// </summary>
    public class Manager : MonoSingleton<Manager>, IFrameNode
    {
        [SerializeField]
        private Frame currentFocusFrame;

        [SerializeField]
        private FrameStack frameStack = new FrameStack();

        public Transform FrameContainer { get { return transform; } }
        public FrameStack FrameStack { get { return frameStack; } }

        public Frame CurrentFocusFrame
        {
            get
            {
                return currentFocusFrame;
            }
            private set
            {
                if (currentFocusFrame != null) currentFocusFrame.OnFocusChange?.Invoke(false);
                currentFocusFrame = value;
                if (currentFocusFrame != null) currentFocusFrame.OnFocusChange?.Invoke(true);
            }
        }

        public void OpenFrame(string frameName, IFrameNode parentNode)
        {
            GameObject instance = AssetsAgent.GetGameObject(frameName, parentNode.FrameContainer);
            Frame frame = instance.GetComponent<Frame>();
            parentNode.FrameStack.Push(frame);
            frame.parentNode = parentNode;

            CurrentFocusFrame = frame;
        }
        public void OpenFrame(string frameName)
        {
            OpenFrame(frameName, this);
        }
        public void CloseFrame(Frame frame)
        {
            if (frame.HasFrameStack())
            {
                Frame[] array = frame.FrameStack.ToArray();
                frame.FrameStack.Clear();
                for (int i = 0; i < array.Length; i++) CloseFrame(array[i]);
            }
            FrameStack stack = frame.parentNode.FrameStack;
            bool topChange = (frame == stack.Peek());
            stack.Remove(frame);
            if (topChange) CurrentFocusFrame = FindFocusFrame(stack);
            AssetsAgent.DestroyGameObject(frame.gameObject);
        }
        public void CloseFrame(IFrameNode parentNode, int index)
        {
            if (parentNode != null) CloseFrame(parentNode.FrameStack.GetFrame(index));
        }
        public void SetFrameToTop(Frame frame)
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
            gameObject.hideFlags = HideFlags.NotEditable;
            InitFrameUICamera();
            DontDestroyOnLoad(gameObject);
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

            Frame.uiCamera = uiCamera;
        }
        private Frame FindFocusFrame(FrameStack stack)
        {
            Frame stackTop = stack.Peek();
            if (stackTop)
            {
                if (stackTop.HasFrameStack() && stackTop.FrameStack.Count > 0) return FindFocusFrame(stackTop.FrameStack);
                else return stackTop;
            }
            return null;
        }

    }
}
