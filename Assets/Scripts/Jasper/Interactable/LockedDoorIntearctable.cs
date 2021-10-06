using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorIntearctable : Interactable
{
    public GameObject DoorLock;

    private DoorControl doorControl;
    private bool solveLock = false;

    private void Start()
    {
        base.Start();
        doorControl = GetComponentInParent<DoorControl>();
    }

    public override void Interact()
    {
        base.Interact();
        if (solveLock == false)
        {
            InspectionSystem.Instance.TurnOn();
            DoorLock.SetActive(true);
        }
        else
        {
            doorControl.PlayerAnimation();
        }
    }

    public override void StopInteracting()
    {
        base.StopInteracting();
        if (solveLock == false)
        {
            InspectionSystem.Instance.TurnOff();
            DoorLock.SetActive(false);
            solveLock = true;
        }
    }
}
