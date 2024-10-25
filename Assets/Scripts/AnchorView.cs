using TMPro;
using UnityEngine;

public class AnchorView : MonoBehaviour, IAnchorView
{
    [SerializeField] private MeshRenderer objMesh;
    [SerializeField] private Color highlightColor;
    [SerializeField] private Color defaultColor;
    [SerializeField] private TMP_Text label;

    public void Init(Vector3 worldPosition)
    {
        objMesh.material.color = defaultColor;
        label.gameObject.SetActive(false);
        transform.position = worldPosition;
    }

    public void SetHighlight(bool highlight)
    {
        objMesh.material.color = highlight ? highlightColor : defaultColor;
    }

    public void UpdateLabel(string labelString)
    {
        label.text = labelString;
        if(!label.gameObject.activeInHierarchy)
        {
            label.gameObject.SetActive(true);
        }
    }

    public void UpdateLabelLookDirection(Vector3 target)
    {
        label.transform.LookAt(target);
    }

    public void HideLabel()
    {
        label.gameObject.SetActive(false);
    }

    public void SetObjectLayer(int layer)
    {
        objMesh.gameObject.layer = layer;
    }
}

