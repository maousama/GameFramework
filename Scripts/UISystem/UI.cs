using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UISystem
{
    [RequireComponent(typeof(Canvas))]
    public sealed class UI : MonoBehaviour, IUIContainer
    {
        /// <summary>
        /// 用于存放子UI的容器
        /// 需要拖动来初始化
        /// 使用Container存放子UI能使编辑器中的UI结构一目了然
        /// 由于一个界面的子界面层级一定处于组织其元素(Button,Panel等)的层级的上方所以通过组织
        /// </summary>
        [SerializeField]
        private Transform uiContainer;
        /// <summary>
        /// 用于存放UI界面内部的元素的容器
        /// 需要拖动来初始化
        /// 与UIs结合确定元素与子界面的层级关系
        /// </summary>
        [SerializeField]
        private Transform elements;
        /// <summary>
        /// Canvas与层级管理相关需要对预设提进行配置
        /// 暂时使用一个Canvas一个界面一个Canvas已经可以满足大部分需求
        /// </summary>
        [SerializeField]
        private Canvas canvas;

        /// <summary>
        /// UI的父容器
        /// </summary>
        private IUIContainer parent;

        public IUIContainer Parent { get { return parent; } internal set { parent = value; } }
        public Transform UIContainer { get { return uiContainer; } }
        public Canvas Canvas { get { return canvas; } }
    }
}