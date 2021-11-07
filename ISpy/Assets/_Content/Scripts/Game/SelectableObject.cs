using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public ObjectData selectableObjectData;
    private Outline outline;
    private void InitOutline()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineColor = selectableObjectData.outlineColor;
        outline.OutlineWidth = 5.0f;
    }
    public void ToggleOutline(bool isEnabled)
    {
        if (outline != null)
            outline.enabled = isEnabled;
        else
        {
            InitOutline();
            outline.enabled = isEnabled;
        }
    }
}
