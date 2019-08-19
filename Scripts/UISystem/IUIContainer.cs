using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UISystem
{
    public interface IUIContainer
    {
        Transform UIContainer { get; }
        IUIContainer Parent { get; }
    }
}