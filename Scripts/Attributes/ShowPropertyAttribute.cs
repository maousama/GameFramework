using UnityEngine;

public class ShowPropertyAttribute : PropertyAttribute
{
    public string PropertyName { get; private set; }
    public float? MinValue { get; private set; }
    public float? MaxValue { get; private set; }

    public ShowPropertyAttribute(string propertyName = null)
    {
        this.PropertyName = propertyName;
    }
    public ShowPropertyAttribute(float min, float max, string propertyName = null) : this(propertyName)
    {
        MinValue = min;
        MaxValue = max;
    }

}
