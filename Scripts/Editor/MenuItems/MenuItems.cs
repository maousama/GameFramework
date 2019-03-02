using Assets.Scripts.UIFramework;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuItems : MonoBehaviour
{
    [MenuItem("GameObject/UI/Frame", false, 1)]
    public static void CreateCustomGameObject(MenuCommand menuCommand)
    {
        GameObject go = new GameObject("Frame");
        Canvas canvas = go.AddComponent<Canvas>();
        CanvasScaler canvasScaler = go.AddComponent<CanvasScaler>();
        GraphicRaycaster graphicRaycaster = go.AddComponent<GraphicRaycaster>();
        go.AddComponent<Frame>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        GameObject componentContainer = new GameObject("ComponentContainer", typeof(RectTransform));
        componentContainer.transform.SetParent(go.transform);
        RectTransform containerRectTr = componentContainer.GetComponent<RectTransform>();
        
        containerRectTr.anchorMin = Vector2.zero;
        containerRectTr.anchorMax = new Vector2(1, 1);
        containerRectTr.offsetMin = Vector2.zero;
        containerRectTr.offsetMax = Vector2.zero;

        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
}
