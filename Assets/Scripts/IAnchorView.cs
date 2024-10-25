using UnityEngine;

public interface IAnchorView 
{
    public void Init(Vector3 worldPosition);
    public void SetHighlight(bool highlight);
    public void UpdateLabel(string labelString);
    public void UpdateLabelLookDirection(Vector3 target);
    public void HideLabel();
    void SetObjectLayer(int layer);
}
