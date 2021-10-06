using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoInteractable : Interactable
{
    public Transform focusPointTransform;
    private PianoControl myPianoControl;

    private void Start()
    {
        base.Start();
        myPianoControl = GetComponent<PianoControl>();
    }

    public override void Interact()
    {
        base.Interact();
        PlayerControl.Instance.FocusOnObject(focusPointTransform, false);
        myPianoControl.enabled = true;
    }

    public override void FinishInteracting()
    {
        base.FinishInteracting();
        PlayerControl.Instance.StopFocusOnObject();
        myPianoControl.enabled = false;
    }
}