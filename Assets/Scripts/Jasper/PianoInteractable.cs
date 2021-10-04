using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoInteractable : Interactable
{
    private PianoControl myPianoControl;

    private void Start()
    {
        base.Start();
        myPianoControl = GetComponent<PianoControl>();
    }

    public override void Interact()
    {
        base.Interact();
        print("Interact with Piano");
        myPianoControl.enabled = true;
    }

    public override void StopInteracting()
    {
        base.StopInteracting();
        myPianoControl.enabled = false;
    }
}