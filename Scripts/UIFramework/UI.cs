using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UIFramework
{
    public sealed class UI : MonoBehaviour, IUIContainer
    {
        private int instanceID;
        /// <summary>
        /// 子UI列表负责管理层级
        /// 使用Transform就可以进行管理但是无法保证Transform的子物体结构顺序不被污染所以使用List与Container同时进行维护
        /// </summary>
        private List<UI> children;
        /// <summary>
        /// 用于存放子UI的容器
        /// 使用Container存放子UI能使编辑器中的UI结构一目了然
        /// 由于一个界面的子界面层级一定处于组织其元素(Button,Panel等)的层级的上方所以通过组织
        /// </summary>
        private Transform uis;
        /// <summary>
        /// 用于存放UI界面内部的元素的容器
        /// 与UIs结合确定元素与子界面的层级关系
        /// </summary>
        [SerializeField]
        private Transform elements;
        /// <summary>
        /// 界面栈
        /// 每个用户界面层级在栈内结构变动时变动
        /// </summary>
        UIStack uiStack;

        public Transform UIContainer { get { return uis; } }
        public int InstanceID { get { return instanceID; } }
        public UIStack UIStack { get { return uiStack; } }

        private void Awake()
        {
            instanceID = GetInstanceID();
            if (!elements) Debug.LogError("varible elements is not setted");
        }
    }
}