using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeInteract : Interactable
{
    private RotateController rotateControl;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        rotateControl = GetComponentInChildren<RotateController>();
    }

    public override void Interact()
    {
        base.Interact();
        rotateControl.rotateGlobe();
    }
}
