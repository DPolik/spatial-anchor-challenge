using UnityEngine;

public class Room : IRoom
{
    private Vector3 _center;
    private Vector3 _sizeInMeters;
    private GameObject _wallPrefab;
    private float _wallSize;

    public Room(Vector3 center, Vector3 sizeInMeters, GameObject wallPrefab, float wallSize)
    {
        _center = center;
        _sizeInMeters = sizeInMeters;
        _wallPrefab = wallPrefab;
        _wallSize = wallSize;
    }

    public void BuildRoom()
    {
        var wall = GameObject.Instantiate(_wallPrefab);
        wall.transform.localScale = new Vector3(_sizeInMeters.z / _wallSize, 1, _sizeInMeters.y / _wallSize);
        wall.transform.position = _center + Vector3.right * (_sizeInMeters.x / 2f);
        wall.transform.LookAt(_center);
        wall.transform.RotateAround(wall.transform.position, wall.transform.right, 90);

        wall = GameObject.Instantiate(_wallPrefab);
        wall.transform.localScale = new Vector3(_sizeInMeters.z / _wallSize, 1, _sizeInMeters.y / _wallSize);
        wall.transform.position = _center + Vector3.left * (_sizeInMeters.x / 2f);
        wall.transform.LookAt(_center);
        wall.transform.RotateAround(wall.transform.position, wall.transform.right, 90);

        wall = GameObject.Instantiate(_wallPrefab);
        wall.transform.localScale = new Vector3(_sizeInMeters.x / _wallSize, 1, _sizeInMeters.z / _wallSize);
        wall.transform.position = _center + Vector3.up * (_sizeInMeters.y / 2f);
        wall.transform.LookAt(_center);
        wall.transform.RotateAround(wall.transform.position, wall.transform.right, 90);

        wall = GameObject.Instantiate(_wallPrefab);
        wall.transform.localScale = new Vector3(_sizeInMeters.x / _wallSize, 1, _sizeInMeters.z / _wallSize);
        wall.transform.position = _center + Vector3.down * (_sizeInMeters.y / 2f);
        wall.transform.LookAt(_center);
        wall.transform.RotateAround(wall.transform.position, wall.transform.right, 90);

        wall = GameObject.Instantiate(_wallPrefab);
        wall.transform.localScale = new Vector3(_sizeInMeters.x / _wallSize, 1, _sizeInMeters.y / _wallSize);
        wall.transform.position = _center + Vector3.forward * (_sizeInMeters.z / 2f);
        wall.transform.LookAt(_center);
        wall.transform.RotateAround(wall.transform.position, wall.transform.right, 90);

        wall = GameObject.Instantiate(_wallPrefab);
        wall.transform.localScale = new Vector3(_sizeInMeters.x / _wallSize, 1, _sizeInMeters.y / _wallSize);
        wall.transform.position = _center + Vector3.back * (_sizeInMeters.z / 2f);
        wall.transform.LookAt(_center);
        wall.transform.RotateAround(wall.transform.position, wall.transform.right, 90);
    }

    public bool IsPositionOutOfBounds(Vector3 position)
    {
        var sizeVector = new Vector3(_sizeInMeters.x, _sizeInMeters.y, _sizeInMeters.z);
        var minBounds = _center - sizeVector/2f;
        var maxBounds = _center + sizeVector/2f;
        return position.x < minBounds.x || position.x > maxBounds.x ||
               position.y < minBounds.y || position.y > maxBounds.y ||
               position.z < minBounds.z || position.z > maxBounds.z;
    }
}
