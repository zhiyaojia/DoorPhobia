using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveLetterInteractable : CollectableInteract
{
    [Header("LoveLetter Settings")]
    public PhoneControl phoneControl;

    public override void QuitInteracting()
    {
        base.QuitInteracting();
        phoneControl.PhoneRing();
    }
}
