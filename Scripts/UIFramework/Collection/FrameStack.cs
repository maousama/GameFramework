using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIFramework
{
    public class FrameStack
    {
        private List<Frame> list = new List<Frame>();
        private Dictionary<Frame, int> frameToIndex = new Dictionary<Frame, int>();

        internal int Count { get { return list.Count; } }
        internal Frame Pop()
        {
            if (list.Count == 0) return null;
            Frame frame = list.LastOrDefault();
            frameToIndex.Remove(list[Count - 1]);
            list.RemoveAt(Count - 1);
            return frame;
        }
        internal Frame Peek()
        {
            return list.LastOrDefault();
        }
        internal void Push(Frame frame)
        {
            frameToIndex.Add(frame, Count);
            list.Add(frame);
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
            list.RemoveAt(index);
            for (int i = index; i < Count; i++)
            {
                frameToIndex[list[i]] -= 1;
            }
            list.Add(temp);
            frameToIndex[temp] = Count - 1;
        }
        internal void JumpToTop(Frame frame)
        {
            int index;
            if (frameToIndex.TryGetValue(frame, out index))
            {
                JumpToTop(index);
            }
        }
        internal void Remove(Frame frame)
        {
            int index = frameToIndex[frame];
            list.RemoveAt(index);
            frameToIndex.Remove(frame);
        }

        private bool IsInRange(int index) { return !(index <= 0 || index >= list.Count); }
        private int GetIndex(Frame frame) { return frameToIndex[frame]; }
    }
}
