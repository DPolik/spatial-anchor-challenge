using UnityEngine;

public struct AnchorModel
{
    public bool IsNearest { get => _isNearest; set => _isNearest = value; }
    public bool IsFocused { get => _isFocused; set => _isFocused = value; }
    public Vector3 WorldPosition { get => _worldPosition; set => _worldPosition = value; }
    public IAnchorTargetable AnchoredTarget { get => _anchoredTarget; set => _anchoredTarget = value; }

    private bool _isNearest;
    private bool _isFocused;
    private Vector3 _worldPosition;
    private IAnchorTargetable _anchoredTarget;
}
