using UnityEngine;

namespace UIFramework
{
    public interface IFrameNode
    {
        Transform FrameContainer { get; }
        FrameStack FrameStack { get; }
        bool IsFrameStackExist();
    }
}
