using UnityEngine;

public class Main : MonoBehaviour
{
    [Header("Room")]
    [SerializeField] private Vector3 _roomSizeInMeters;
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private float _wallSize;
    [Header("Anchor System")]
    [SerializeField] private float _anchorRangeInMeters;
    [SerializeField] private GameObject _anchorPrefab;

    void Start()
    {
        var anchorTarget = Camera.main.GetComponent<IAnchorTargetable>();
        IRoom room = new Room(anchorTarget.GetCurrentPosition(), _roomSizeInMeters, _wallPrefab, _wallSize);
        room.BuildRoom();
        var anchorSystem = new AnchorSystem();
        anchorSystem.Init(_anchorPrefab, anchorTarget, room, _anchorRangeInMeters);        
    }
}
