using UnityEngine;

namespace Assets.Scripts.UIFramework
{
    public interface IUIContainer
    {
        Transform UIContainer { get; }
        UIStack UIStack { get; }
    }
}