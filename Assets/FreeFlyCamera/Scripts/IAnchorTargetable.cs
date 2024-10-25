using System;
using UnityEngine;

public interface IAnchorTargetable
{
    public event Action<Vector3> OnPositionChanged;
    public event Action<Vector3> OnRotationChanged;
    public Vector3 GetCurrentPosition();
    public Vector3 GetCurrentLookVector();
}
