using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteract : Interactable
{
    //粘的LockInteract的，还没搞明白怎么用
    // public LockInteract padLock;
    private LockController myLockController;

    private void Start()
    {
        base.Start();
        myLockController = GetComponent<LockController>();
        // myLockController.enabled = false;
        // lockCamera.enabled = false;
    }

    public override void Interact()
    {
        // mainCamera.enabled = false;
        // lockCamera.enabled = true;
        myLockController.enabled = true;
        base.Interact();
        print("Interact with Lock");        
        
    }

    public override void StopInteracting()
    {
        base.StopInteracting();
        myLockController.enabled = false;
        // mainCamera.enabled = true;
        // lockCamera.enabled = false;
    }
}
