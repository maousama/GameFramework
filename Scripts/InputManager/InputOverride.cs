using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputOverride : BaseInput
{
    protected override void Awake()
    {
        base.Awake();
        StandaloneInputModule standaloneInputModule = GetComponent<StandaloneInputModule>();
        if (standaloneInputModule) standaloneInputModule.inputOverride = this;
    }

    public override float GetAxisRaw(string axisName)
    {
        if (axisName == "Horizontal") { }
        else if (axisName == "Vertical") { }
        return 0f;
    }

    public override bool GetButtonDown(string buttonName)
    {
        if (buttonName == "Submit") { }
        else if (buttonName == "Cancel") { }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override string ToString()
    {
        return base.ToString();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    public override bool IsActive()
    {
        return base.IsActive();
    }

    protected override void OnValidate()
    {
        base.OnValidate();
    }

    protected override void Reset()
    {
        base.Reset();
    }

    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();
    }

    protected override void OnBeforeTransformParentChanged()
    {
        base.OnBeforeTransformParentChanged();
    }

    protected override void OnTransformParentChanged()
    {
        base.OnTransformParentChanged();
    }

    protected override void OnDidApplyAnimationProperties()
    {
        base.OnDidApplyAnimationProperties();
    }

    protected override void OnCanvasGroupChanged()
    {
        base.OnCanvasGroupChanged();
    }

    protected override void OnCanvasHierarchyChanged()
    {
        base.OnCanvasHierarchyChanged();
    }

    public override bool GetMouseButtonDown(int button)
    {
        return base.GetMouseButtonDown(button);
    }

    public override bool GetMouseButtonUp(int button)
    {
        return base.GetMouseButtonUp(button);
    }

    public override bool GetMouseButton(int button)
    {
        return base.GetMouseButton(button);
    }

    public override Touch GetTouch(int index)
    {
        return base.GetTouch(index);
    }
}
