using UnityEngine;

namespace UIFramework
{
    public interface IFrameNode
    {
        Transform Transform { get; }
        FrameStack FrameStack { get; }
    }
}
