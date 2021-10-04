using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockInteract : Interactable
{
    
    private LockController myLockController;
    public Camera mainCamera;
    public Camera renderCamera;

    private void Start()
    {
        base.Start();
        myLockController = GetComponent<LockController>();
        myLockController.enabled = false;
        renderCamera.enabled = false;
    }

    public override void Interact()
    {
        mainCamera.enabled = false;
        renderCamera.enabled = true;
        myLockController.enabled = true;
        base.Interact();
        print("Interact with Lock");        
        
    }

    public override void StopInteracting()
    {
        base.StopInteracting();
        myLockController.enabled = false;
        mainCamera.enabled = true;
        renderCamera.enabled = false;
    }
}
