using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInteractable : Interactable
{
    private MapControl mapControl;

    void Start()
    {
        base.Start();
        mapControl = GetComponentInChildren<MapControl>();
    }

    public override void Interact()
    {
        base.Interact();
        mapControl.enabled = true;
    }

    public override void StopInteracting()
    {
        base.StopInteracting();
        mapControl.Stop();
    }
}
