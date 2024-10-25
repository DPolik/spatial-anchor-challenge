using UnityEngine;

public interface IRoom
{
    public void BuildRoom();
    public bool IsPositionOutOfBounds(Vector3 position);
}
