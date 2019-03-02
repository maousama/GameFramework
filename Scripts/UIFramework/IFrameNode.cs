using UnityEngine;

namespace Assets.Scripts.UIFramework
{
    public interface IFrameNode
    {
        Transform FrameContainer { get; }
        FrameStack FrameStack { get; }
        bool IsFrameStackExist();
    }
}
