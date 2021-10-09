using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorIntearctable : Interactable
{
    public GameObject DoorLock;

    private DoorControl doorControl;

    private void Start()
    {
        base.Start();
        doorControl = GetComponentInParent<DoorControl>();
    }

    public override void Interact()
    {
        base.Interact();
        if (solvedPreLock == false)
        {
            InspectionSystem.Instance.TurnOn();
            DoorLock.SetActive(true);
        }
        else
        {
            doorControl.PlayerAnimation();
        }
    }

    public override void FinishInteracting()
    {
        base.FinishInteracting();
        if (solvedPreLock == false)
        {
            InspectionSystem.Instance.TurnOff();
            DoorLock.SetActive(false);
            solvedPreLock = true;
        }
    }

    public override void QuitInteracting()
    {
        base.QuitInteracting();
        if (solvedPreLock == false)
        {
            InspectionSystem.Instance.TurnOff();
            DoorLock.SetActive(false);
        }
    }
}
