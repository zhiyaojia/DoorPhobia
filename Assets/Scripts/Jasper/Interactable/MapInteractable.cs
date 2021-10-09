using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInteractable : Interactable
{
    public Transform focusPointTransform;

    private MapControl mapControl;

    void Start()
    {
        base.Start();
        mapControl = GetComponentInChildren<MapControl>();
    }

    public override void Interact()
    {
        base.Interact();
        PlayerControl.Instance.FocusOnObject(focusPointTransform, true);
        mapControl.enabled = true;
    }

    public override void FinishInteracting()
    {
        base.FinishInteracting();
        PlayerControl.Instance.StopFocusOnObject();
        mapControl.enabled = false;
    }

    public override void QuitInteracting()
    {
        base.QuitInteracting();
        PlayerControl.Instance.StopFocusOnObject();
        mapControl.enabled = false;
    }
}