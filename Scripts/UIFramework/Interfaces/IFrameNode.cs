using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UIFramework
{
    public interface IFrameNode
    {
        Transform Transform { get; }
        FrameStack FrameStack { get; }
    }
}
