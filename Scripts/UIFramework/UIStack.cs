using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UIFramework
{
    [Serializable]
    public class UIStack
    {
        [SerializeField]
        private List<UI> list = new List<UI>();
        /// <summary>
        /// 由UIManager使用此方法调用前物体已经成为了Container的子物体
        /// </summary>
        /// <param name="ui"></param>
        internal void Push(UI ui)
        {
            list.Add(ui);
            ui.transform.SetSiblingIndex(list.Count);
        }
        internal void RemoveAt(int index)
        {
            if (!IsInRange(index)) return;
            if (index == LastIndex) { Pop(); return; }
            frameToIndex.Remove(list[index]);
            list.RemoveAt(index);
            for (int i = index; i < Count; i++)
            {
                frameToIndex[list[i]] = i;
                list[i].SetSortingOrder(i);
            }
        }
        internal void Remove(Frame frame)
        {
            int index;
            if (frameToIndex.TryGetValue(frame, out index))
            {
                if (index == LastIndex) { Pop(); return; }
                RemoveAt(index);
            }
        }
        internal Frame Pop()
        {
            if (list.Count == 0) return null;
            Frame frame = list[LastIndex];
            frameToIndex.Remove(list[LastIndex]);
            list.RemoveAt(LastIndex);
            return frame;
        }
        internal Frame Peek()
        {
            if (LastIndex >= 0) return list[LastIndex];
            else return null;
        }
        internal void Push(Frame frame)
        {
            frameToIndex.Add(frame, Count);
            list.Add(frame);
            frame.SetSortingOrder(LastIndex);
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
        internal void Swap(int index1, int index2)
        {
            if (!IsInRange(index1) || !IsInRange(index2)) return;
            int temp = index1;
            frameToIndex[list[index1]] = index2;
            frameToIndex[list[index2]] = temp;
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
