using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Panel : Selectable
{
    public View view;
    
    protected override void Awake()
    {
        base.Awake();
        transition = Transition.None;

        Navigation newNavigation = new Navigation();
        view = transform.GetComponentInParent<View>();
        newNavigation.mode = Navigation.Mode.Explicit;
        newNavigation.selectOnDown = newNavigation.selectOnLeft = newNavigation.selectOnRight = newNavigation.selectOnUp = view.defaultSelected;
        navigation = newNavigation;

        if (EventSystem.current != null) EventSystem.current.SetSelectedGameObject(gameObject);
    }
}
