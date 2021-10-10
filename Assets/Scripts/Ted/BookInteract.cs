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


    private openNotebookAnimation diaryControl;
    AnalyticsResult ar;

    private void Start()
    {
        base.Start();
        myCollider = closeBookCollider;
        diaryControl = GetComponent<openNotebookAnimation>();
    }

    public override void Interact()
    {
        base.Interact();
        AnalyticsEvent.LevelStart("diary_lock");
        // when start to interact, set diarycontrol timer to 0
        diaryControl.secondsElapsed = 0;
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
        //finish diarylock
        base.FinishInteracting();
        // add custom params in analytical events: seconds played
        Dictionary<string, object> customParams = new Dictionary<string, object>();
        customParams.Add("seconds_played", diaryControl.secondsElapsed);
        
        if (solvedPreLock == false)
        {
            InspectionSystem.Instance.TurnOff();
            ColorLock.SetActive(false);
            solvedPreLock = true;
            AnalyticsEvent.LevelComplete("diary_lock", customParams);
            ar = AnalyticsEvent.LevelComplete("diary_lock");
            Debug.Log("LCResult = " + ar.ToString() + diaryControl.secondsElapsed.ToString());
            diaryControl.OpenBook();
            myCollider = openBookCollider;
        }
    }

    public override void QuitInteracting()
    {
        //not finish diary lock, press space to quit
        base.QuitInteracting();
        Dictionary<string, object> customParams = new Dictionary<string, object>();
        customParams.Add("seconds_played", diaryControl.secondsElapsed);
        if (solvedPreLock == false)
        {
            InspectionSystem.Instance.TurnOff();
            ColorLock.SetActive(false);
            AnalyticsEvent.LevelQuit("diary_lock", customParams);
            ar = AnalyticsEvent.LevelQuit("diary_lock");
            Debug.Log("LQResult = " + ar.ToString() + diaryControl.secondsElapsed.ToString());
        }
    }
}
