using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInteract : Interactable
{
    public PianoInteractable piano;

    public override void Interact()
    {
        print("interact with cube");
        piano.Unlock();
    }
}
