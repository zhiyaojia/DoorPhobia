using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneInteractable : Interactable
{
    private PhoneControl phoneControl;

    void Start()
    {
        base.Start();
        phoneControl = GetComponent<PhoneControl>();
    }

    public override void Interact()
    {
        base.Interact();
        phoneControl.Interact();
    }
}
