using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInteractable : Interactable
{
    [ConditionalHide("focusOnView", true)]
    public Transform focusPointTransform;
    [ConditionalHide("Nothing but freeze player's movement", true)]
    public bool canRotateView = false;

    private MapControl mapControl;

    void Start()
    {
        base.Start();
        mapControl = GetComponentInChildren<MapControl>();
    }

    public override void Interact()
    {
        base.Interact();
        PlayerControl.Instance.FocusOnObject(focusPointTransform, canRotateView);
        mapControl.enabled = true;
    }

    public override void StopInteracting()
    {
        base.StopInteracting();
        PlayerControl.Instance.StopFocusOnObject(canRotateView);
        mapControl.enabled = false;
    }
}