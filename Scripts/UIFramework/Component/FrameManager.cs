using UnityEngine;
using AssetsManager;

namespace UIFramework
{
    /// <summary>
    /// 一个单例物体负责管理Frame供外部使用同时作为Frame的根节点使用
    /// </summary>
    public class FrameManager : MonoSingleton<FrameManager>, IFrameNode
    {
        private FrameStack frameStack = new FrameStack();

        public Transform Transform { get { return transform; } }

        public FrameStack FrameStack { get { return frameStack; } }


        public void OpenFrame(string frameName, IFrameNode parentNode)
        {
            GameObject instance = AssetsAgent.GetGameObject(frameName, parentNode.Transform);
            Frame frame = instance.GetComponent<Frame>();
            parentNode.FrameStack.Push(frame);


        }
        public void CloseFrame(Frame frame)
        {

        }
        public void PopFrame(Frame frame)
        {

        }
    }
}
