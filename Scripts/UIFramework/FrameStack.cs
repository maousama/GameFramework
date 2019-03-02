using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UIFramework
{
    [Serializable]
    public class FrameStack
    {
        [SerializeField]
        private List<Frame> list = new List<Frame>();
        private Dictionary<Frame, int> frameToIndex = new Dictionary<Frame, int>();

        private int Count { get { return list.Count; } }
        private int lastIndex { get { return Count - 1; } }

        internal Frame Pop()
        {
            if (list.Count == 0) return null;
            Frame frame = list[lastIndex];
            frameToIndex.Remove(list[lastIndex]);
            list.RemoveAt(lastIndex);
            return frame;
        }
        internal Frame Peek()
        {
            return list[lastIndex];
        }
        internal void Push(Frame frame)
        {
            frameToIndex.Add(frame, Count);
            list.Add(frame);
            frame.OnIndexChange(Count);
        }
        internal bool Contains(Frame frame)
        {
            return frameToIndex.ContainsKey(frame);
        }
        internal void Clear()
        {
            frameToIndex.Clear();
            list.Clear();
        }
        internal void JumpToTop(int index)
        {
            if (!IsInRange(index)) return;
            Frame temp = list[index];
            RemoveAt(index);
            Push(temp);
        }
        internal void JumpToTop(Frame frame)
        {
            int index;
            if (frameToIndex.TryGetValue(frame, out index))
            {
                JumpToTop(index);
            }
        }
        internal void RemoveAt(int index)
        {
            if (!IsInRange(index)) return;
            frameToIndex.Remove(list[index]);
            list.RemoveAt(index);
            for (int i = index; i < Count; i++)
            {
                frameToIndex[list[i]] = i;
                list[i].OnIndexChange(i);
            }
        }
        internal void Remove(Frame frame)
        {
            int index;
            if (frameToIndex.TryGetValue(frame, out index))
            {
                RemoveAt(index);
            }
        }
        internal Frame GetFrame(int index)
        {
            if (IsInRange(index)) return list[index];
            else return null;
        }
        internal Frame[] ToArray()
        {
            return list.ToArray();
        }
        private bool IsInRange(int index) { return !(index <= 0 || index >= list.Count); }
        private int GetIndex(Frame frame) { return frameToIndex[frame]; }
    }
}
