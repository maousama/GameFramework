using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIFramework
{
    internal class FrameStack : IEnumerable, ICollection
    {
        private List<Frame> list = new List<Frame>();
        private Dictionary<Frame, int> frameToIndex = new Dictionary<Frame, int>();
        /// <summary>
        /// 窗体数量
        /// </summary>
        public int Count
        {
            get
            {
                return list.Count;
            }
        }
        /// <summary>
        /// 出栈
        /// </summary>
        /// <returns></returns>
        public Frame Pop()
        {
            if (list.Count == 0) return null;
            Frame frame = list.LastOrDefault();
            frameToIndex.Remove(list[Count - 1]);
            list.RemoveAt(Count - 1);
            return frame;
        }
        /// <summary>
        /// 查看栈顶
        /// </summary>
        /// <returns></returns>
        public Frame Peek()
        {
            return list.LastOrDefault();
        }
        /// <summary>
        /// 入栈
        /// </summary>
        /// <param name="frame"></param>
        public void Push(Frame frame)
        {
            frameToIndex.Add(frame, Count);
            list.Add(frame);
        }
        /// <summary>
        /// 包含
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public bool Contains(Frame frame)
        {
            return frameToIndex.ContainsKey(frame);
        }
        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            frameToIndex.Clear();
            list.Clear();
        }
        /// <summary>
        /// 将Index指定框架到栈顶
        /// </summary>
        /// <param name="index"></param>
        public void JumpToTop(int index)
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
        /// <summary>
        /// 将Frame指定框架弹到栈顶
        /// </summary>
        /// <param name="frame"></param>
        public void JumpToTop(Frame frame)
        {
            int index;
            if(frameToIndex.TryGetValue(frame,out index))
            {
                JumpToTop(index);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            Frame[] frameArray = (Frame[])array;
            list.CopyTo(frameArray);
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }
        public object SyncRoot
        {
            get
            {
                return null;

            }
        }

        private bool IsInRange(int index)
        {
            return !(index <= 0 || index >= list.Count);
        }

        private int GetIndex(Frame frame)
        {
            return frameToIndex[frame];
        }

        
    }
}
