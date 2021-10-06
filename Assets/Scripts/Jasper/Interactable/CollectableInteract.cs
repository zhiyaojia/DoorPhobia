using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableInteract : Interactable
{
    public int BagIndex;

    public override void Interact()
    {
        gameObject.SetActive(false);
        BagSystemControl.Instance.AddObject(BagIndex, needDialogue);
        PlayerControl.Instance.SetHandIcon(false);
    }
}
