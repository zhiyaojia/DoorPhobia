using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteract : Interactable
{
    public override void Interact()
    {
        base.Interact();
        PlayerControl.Instance.ShowDialogue(message);
    }
}
