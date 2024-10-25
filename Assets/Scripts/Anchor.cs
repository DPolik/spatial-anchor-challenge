using UnityEngine;

public class Anchor
{
    private IAnchorView _view;
    private AnchorModel _model;

    public Anchor(GameObject prefab, Vector3 anchorWorldPosition)
    {
        _view = GameObject.Instantiate(prefab).GetComponent<IAnchorView>();
        _view.Init(anchorWorldPosition);
        _model = new AnchorModel
        {
            WorldPosition = anchorWorldPosition
        };
    }

    internal Vector3 GetPosition()
    {
        return _model.WorldPosition;
    }

    internal void AnchorTarget(IAnchorTargetable target)
    {
        _model.AnchoredTarget = target;
        _model.IsNearest = true;
        _view.SetHighlight(true);
    }

    internal void DetachTarget()
    {
        _model.AnchoredTarget = null;
        _model.IsNearest = false;
        _view.SetHighlight(false);
        _view.HideLabel();
    }

    internal void SetLayer(int layer)
    {
        _view.SetObjectLayer(layer);
    }

    internal void SetFocus(bool focused)
    {
        _model.IsFocused = focused;
        if(focused)
        {
            _view.UpdateLabel(Vector3.Distance(_model.WorldPosition, _model.AnchoredTarget.GetCurrentPosition()).ToString());
            _view.UpdateLabelLookDirection(_model.AnchoredTarget.GetCurrentPosition());
        }
        else
        {
            _view.HideLabel();
        }
    }
}
