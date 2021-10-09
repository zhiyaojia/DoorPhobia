using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableInteract : Interactable
{
    public int BagIndex;
    [Tooltip("Interactable object that will be unlocked after you interact with this object")]
    public Interactable TargetInteractObject;

    public override void Interact()
    {
        gameObject.SetActive(false);
        BagSystemControl.Instance.AddObject(BagIndex, needDialogue);
        PlayerControl.Instance.SetHandIcon(false);
        if (TargetInteractObject != null)
        {
            TargetInteractObject.Unlock();
        }
    }
}
