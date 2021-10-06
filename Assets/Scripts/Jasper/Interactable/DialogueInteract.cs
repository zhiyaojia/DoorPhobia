using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteract : Interactable
{
    [TextArea] public string message;

    public override void Interact()
    {
        base.Interact();
        PlayerControl.Instance.ShowDialogue(message);
    }
}
