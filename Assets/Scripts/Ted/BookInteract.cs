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

    private void Start()
    {
        base.Start();
        myCollider = closeBookCollider;
        diaryControl = GetComponent<openNotebookAnimation>();
    }

    public override void Interact()
    {
        base.Interact();
        if (solvedPreLock == false)
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

    public override void FinishInteracting()
    {
        base.FinishInteracting();
        if (solvedPreLock == false)
        {
            InspectionSystem.Instance.TurnOff();
            ColorLock.SetActive(false);
            solvedPreLock = true;
            diaryControl.OpenBook();
            myCollider = openBookCollider;
        }
    }

    public override void QuitInteracting()
    {
        base.QuitInteracting();
        if (solvedPreLock == false)
        {
            InspectionSystem.Instance.TurnOff();
            ColorLock.SetActive(false);
        }
    }
}
