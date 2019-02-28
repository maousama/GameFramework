using UnityEngine;

namespace UIFramework
{
    /// <summary>
    /// 一个单例物体负责管理Frame供外部使用同时作为Frame的根节点使用
    /// </summary>
    public class Manager : MonoSingleton<Manager>, IFrameNode
    {
        private FrameStack frameStack = new FrameStack();

        public Transform Transform { get { return transform; } }
        public FrameStack FrameStack { get { return frameStack; } }

        public void OpenFrame(string frameName, IFrameNode parentNode)
        {
            GameObject instance = AssetsAgent.GetGameObject(frameName, parentNode.Transform);
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
            AssetsAgent.DestroyGameObject(frame.gameObject);
        }
        public void CloseFrame(IFrameNode parentNode, int index)
        {
            if (parentNode != null) AssetsAgent.DestroyGameObject(parentNode.FrameStack.GetFrame(index).gameObject);
        }

        public void PopFrame(Frame frame)
        {
            frame.parentNode.FrameStack.JumpToTop(frame);
        }
        public void PopFrame(IFrameNode frameNode, int index)
        {
            frameNode.FrameStack.JumpToTop(index);
        }

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
