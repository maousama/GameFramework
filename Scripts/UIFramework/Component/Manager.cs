using UnityEngine;

namespace UIFramework
{
    /// <summary>
    /// 一个单例物体负责管理Frame供外部使用同时作为Frame的根节点使用
    /// </summary>
    public class Manager : MonoSingleton<Manager>, IFrameNode
    {
        [SerializeField]
        private FrameStack frameStack = new FrameStack();

        public Transform FrameContainer { get { return transform; } }
        public FrameStack FrameStack { get { return frameStack; } }

        public void OpenFrame(string frameName, IFrameNode parentNode)
        {
            GameObject instance = AssetsAgent.GetGameObject(frameName, parentNode.FrameContainer);
            Frame frame = instance.GetComponent<Frame>();
            parentNode.FrameStack.Push(frame);
            frame.parentNode = parentNode;
        }
        public void OpenFrame(string frameName)
        {
            OpenFrame(frameName, this);
        }

        public void CloseFrame(Frame frame)
        {
            if (frame.IsFrameStackExist())
            {
                Frame[] array = frame.FrameStack.ToArray();
                frame.FrameStack.Clear();
                for (int i = 0; i < array.Length; i++) CloseFrame(array[i]);
            }
            AssetsAgent.DestroyGameObject(frame.gameObject);
        }
        public void CloseFrame(IFrameNode parentNode, int index)
        {
            if (parentNode != null) CloseFrame(parentNode.FrameStack.GetFrame(index));
        }

        public void PopFrame(Frame frame)
        {
            frame.parentNode.FrameStack.JumpToTop(frame);
        }
        public void PopFrame(IFrameNode frameNode, int index)
        {
            frameNode.FrameStack.JumpToTop(index);
        }

        public bool IsFrameStackExist() { return frameStack != null; }

        protected override void Init()
        {
            base.Init();
            gameObject.hideFlags = HideFlags.NotEditable;
            InitFrameUICamera();
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


    }
}
