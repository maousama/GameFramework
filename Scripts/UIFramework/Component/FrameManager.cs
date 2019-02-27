using UnityEngine;
using AssetsManager;

namespace UIFramework
{
    public class FrameManager : MonoSingleton<FrameManager>, IFrameNode
    {
        private FrameStack frameStack = new FrameStack();

        public Transform Transform
        {
            get { return transform; }
        }

        public void OpenFrame(string frameName, IFrameNode parentNode)
        {
            GameObject newFrameGameObject = AssetsAgent.GetAsset<GameObject>(frameName);
            
        }

        public void CloseFrame(Frame frame)
        {

        }

        public void PopFrame(Frame frame)
        {

        }
    }
}
