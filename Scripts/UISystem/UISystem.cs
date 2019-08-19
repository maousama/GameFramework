using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UISystem
{
    /// <summary>
    /// UI系统
    /// 外部使用系统来对UI进行操作
    /// </summary>
    public class UISystem : MonoSingleton<UISystem>, IUIContainer
    {
        public Transform UIContainer { get { return transform; } }
        public IUIContainer Parent { get { return null; } }
        private Camera uiCamera;
        

        #region 需要功能: 打开,关闭,至顶部
        public UI Open(string uiName, IUIContainer container)
        {
            UI ui = AssetsAgent.GetGameObject(uiName, container.UIContainer).GetComponent<UI>();
            if (ui.Canvas.isRootCanvas) ui.Canvas.sortingOrder = ui.transform.GetSiblingIndex();
            ui.Parent = container;
            return ui;
        }
        public void Close(UI ui)
        {
            for (int i = ui.UIContainer.childCount - 1; i > -1; i--)
            {
                UI subUI = ui.UIContainer.GetChild(i).GetComponent<UI>();
                Close(subUI);
            }
            AssetsAgent.DestroyGameObject(ui.gameObject);
        }
        public void SetForegroundUI(UI ui)
        {
            if (IsRootContainer(ui)) return;
            SetForegroundUI((UI)ui.Parent);
            ui.transform.SetSiblingIndex(ui.Parent.UIContainer.childCount - 1);
            if (ui.Canvas.isRootCanvas) ui.Canvas.sortingOrder = ui.transform.GetSiblingIndex();
        }
        #endregion
        private bool IsRootContainer(UI ui) { return ui.Parent == null; }
    }
}