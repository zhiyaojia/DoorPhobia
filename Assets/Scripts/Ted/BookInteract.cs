using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class BookInteract : Interactable
{
    public GameObject ColorLock;
    public int InspectDiaryBookIndex;

    public Collider closeBookCollider;
    public Collider openBookCollider;
    private float secondsElapsed = 0;

    private openNotebookAnimation diaryControl;

    private void Start()
    {
        base.Start();
        myCollider = closeBookCollider;
        diaryControl = GetComponent<openNotebookAnimation>();
    }

    private void Update() {
        secondsElapsed += Time.deltaTime;
    }
    public override void Interact()
    {
        base.Interact();
        AnalyticsEvent.LevelStart("diary_lock");
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
        // add custom params in analytical events: seconds played
        Dictionary<string, object> customParams = new Dictionary<string, object>();
        customParams.Add("seconds_played", secondsElapsed);
        if (solvedPreLock == false)
        {
            InspectionSystem.Instance.TurnOff();
            ColorLock.SetActive(false);
            solvedPreLock = true;
            AnalyticsEvent.LevelComplete("diary_lock", customParams);
            diaryControl.OpenBook();
            myCollider = openBookCollider;
        }
        else
        {
            AnalyticsEvent.LevelQuit("diary_lock", customParams);
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
