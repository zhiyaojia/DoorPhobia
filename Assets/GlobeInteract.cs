using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeInteract : Interactable
{
    private RotateController rotateControl;
    private Animation anim;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        anim = GetComponent<Animation>();
        rotateControl = GetComponentInChildren<RotateController>();
    }

    public override void Interact()
    {
        base.Interact();
        //rotateControl.rotateGlobe();
        anim.Play();
    }
}
