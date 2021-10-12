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
    private float startTime = 0;
    private float solveTime;

    private void Start()
    {
        base.Start();
        myCollider = closeBookCollider;
        diaryControl = GetComponent<openNotebookAnimation>();
    }

    public override void Interact()
    {
        base.Interact();
        //send analytic event
        // AnalyticsEvent.LevelStart("3L_diary_lock");
        // ar = AnalyticsEvent.LevelStart("3L_diary_lock");
        // set start time as the time when player interact with item thefirst time 
        if (startTime == 0) 
        {
            startTime = diaryControl.secondsElapsed;
        }   
        Debug.Log("LCStart = " + ar.ToString() + startTime.ToString());

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
        solveTime = diaryControl.secondsElapsed - startTime;
        // Dictionary<string, object> customParams = new Dictionary<string, object>();
        // customParams.Add("seconds_played", solveTime.ToString());
        
        if (solvedPreLock == false)
        {
            InspectionSystem.Instance.TurnOff();
            ColorLock.SetActive(false);
            solvedPreLock = true;
            // report event
            // AnalyticsEvent.LevelComplete("3L_diary_lock", customParams);
            // ar = AnalyticsEvent.LevelComplete("3L_diary_lock");
            // Debug.Log("LCFinish = " + ar.ToString() + diaryControl.secondsElapsed.ToString() + "SolveTime=" + solveTime.ToString());
            // report custom event
            ReportSolve3LDiaryLock(solveTime);
            ar = Analytics.CustomEvent("solve_diary_lock");
            Debug.Log("solve_3L_diarylock_Result = " + ar.ToString());

            diaryControl.OpenBook();
            myCollider = openBookCollider;
        }
    }

    public override void QuitInteracting()
    {
        //not finish diary lock, press space to quit
        base.QuitInteracting();
        // Dictionary<string, object> customParams = new Dictionary<string, object>();
        // customParams.Add("seconds_played", diaryControl.secondsElapsed);
        if (solvedPreLock == false)
        {
            InspectionSystem.Instance.TurnOff();
            ColorLock.SetActive(false);
            // AnalyticsEvent.LevelQuit("diary_lock", customParams);
            // ar = AnalyticsEvent.LevelQuit("diary_lock");
            // Debug.Log("LQResult = " + ar.ToString() + diaryControl.secondsElapsed.ToString());
        }
    }
    public void ReportSolve3LDiaryLock(float sTime){
        // custom event, report the time used to solve the lock
        AnalyticsEvent.Custom("solve_diary_lock", new Dictionary<string, object>
        {
            { "solve_time", sTime }
        });
    }
}
