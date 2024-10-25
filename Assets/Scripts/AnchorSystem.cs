using System.Collections.Generic;
using UnityEngine;

public class AnchorSystem
{
    private GameObject _anchorPrefab;
    private IAnchorTargetable _anchorTarget;
    private float _anchorReachInMeters;
    private int _nearestAnchorIndex = -1;
    private List<Anchor> _anchorList = new ();
    private string _nearestAnchorLayerName = "NearestAnchor";
    private IRoom _room;

    public void Init(GameObject anchorPrefab, IAnchorTargetable anchorTarget, IRoom room, float anchorReachInMeters)
    {
        _anchorPrefab = anchorPrefab;
        _anchorTarget = anchorTarget;
        _anchorReachInMeters = anchorReachInMeters;

        _anchorTarget.OnPositionChanged += CheckAnchorCreation;
        _anchorTarget.OnRotationChanged += CheckAnchorFocus;

        _room = room;
        CreateAnchor();
    }

    private void CheckAnchorFocus(Vector3 _)
    {
        // Fire a ray to check if we are focused on the nearest anchor
        var nearestIsFocused = Physics.Raycast(_anchorTarget.GetCurrentPosition(), _anchorTarget.GetCurrentLookVector(), _anchorReachInMeters, LayerMask.GetMask(_nearestAnchorLayerName));
        _anchorList[_nearestAnchorIndex].SetFocus(nearestIsFocused);
    }

    private void CheckAnchorCreation(Vector3 targetNewPosition)
    {
        if(_room.IsPositionOutOfBounds(targetNewPosition))
        {
            return;
        }

        // If no anchor was created, create one
        if(_nearestAnchorIndex < 0)
        {
            CreateAnchor();
            return;
        }

        CheckAnchorFocus(_anchorTarget.GetCurrentLookVector());

        // If we are within half the range, means the nearest isn't changing
        var distanceToNearest = Vector3.Distance(_anchorList[_nearestAnchorIndex].GetPosition(), targetNewPosition);
        if(distanceToNearest <= _anchorReachInMeters/2f)
        {
            return;
        }

        // Check if there is a nearer anchor
        int nearestAnchorIndex = FindNewNearestAnchor(targetNewPosition);
        if(nearestAnchorIndex != -1)
        {
            SetAnchorAsNearest(nearestAnchorIndex);
            return;
        }

        // Check if we should create a new anchor
        if (distanceToNearest > _anchorReachInMeters)
        {
            CreateAnchor();
        }
    }

    private void SetAnchorAsNearest(int anchorIndex)
    {
        Anchor nearest;
        if (_nearestAnchorIndex != -1)
        {
            nearest = _anchorList[_nearestAnchorIndex];
            nearest.DetachTarget();
            nearest.SetLayer(0);
        }
        
        _nearestAnchorIndex = anchorIndex;
        nearest = _anchorList[_nearestAnchorIndex];
        nearest.AnchorTarget(_anchorTarget);
        nearest.SetLayer(LayerMask.NameToLayer(_nearestAnchorLayerName));
        CheckAnchorFocus(_anchorTarget.GetCurrentLookVector());
    }

    private int FindNewNearestAnchor(Vector3 newPosition)
    {
        // Simple iteration of the anchor list, should not be impactful with real anchor values
        for(int i = 0; i < _anchorList.Count; i++)
        {
            if(i == _nearestAnchorIndex)
            {
                continue;
            }

            if(Vector3.Distance(_anchorList[i].GetPosition(), newPosition) <= _anchorReachInMeters/2f)
            {
                return i;
            }
        }

        return -1;
    }

    private void CreateAnchor()
    {
        var anchor = new Anchor(_anchorPrefab, _anchorTarget.GetCurrentPosition());
        _anchorList.Add(anchor);
        SetAnchorAsNearest(_anchorList.Count - 1);
    }
}
