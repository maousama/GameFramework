using UnityEngine;
using System.Collections;
using Assets.Scripts.UISystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class View : MonoBehaviour
{
    public static View focusView;
    public Selectable defaultSelected;
    public UI frame;

    private void OnFocusChange(bool isFocus) { if (isFocus) focusView = frame.transform.GetChild(0).GetComponent<View>(); }

    protected virtual void Awake()
    {
        //frame = transform.parent.GetComponent<Frame>();
        //frame.OnFocusChange += OnFocusChange;
    }

    protected virtual void OnDestroy()
    {
        //frame.OnFocusChange -= OnFocusChange;
    }
}
