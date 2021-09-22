using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoInteract : Interactable
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
}
