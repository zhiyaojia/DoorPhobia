using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteract : Interactable
{
    public GameObject ColorLock;
    public int InspectDiaryBookIndex;

    public Collider closeBookCollider;
    public Collider openBookCollider;

    private openNotebookAnimation diaryControl;
    private bool solveLock = false;

    private void Start()
    {
        base.Start();
        myCollider = closeBookCollider;
        diaryControl = GetComponent<openNotebookAnimation>();
    }

    public override void Interact()
    {
        base.Interact();
        if (solveLock == false)
        {
            InspectionSystem.Instance.TurnOn();
            ColorLock.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
            BagSystemControl.Instance.AddObject(InspectDiaryBookIndex);
            PlayerControl.Instance.SetHandIcon(false);
        }
    }

    public override void StopInteracting()
    {
        base.StopInteracting();
        if (solveLock == false)
        {
            InspectionSystem.Instance.TurnOff();
            ColorLock.SetActive(false);
            solveLock = true;
            diaryControl.OpenBook();
            myCollider = openBookCollider;
        }
    }
}
