using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class LockedDoorIntearctable : Interactable
{
    public GameObject DoorLock;

    private DoorControl doorControl;
    private float startTime = 0;
    private float solveTime;

    AnalyticsResult ar;

    private void Start()
    {
        base.Start();
        doorControl = GetComponentInParent<DoorControl>();
    }

    public override void Interact()
    {
        base.Interact();
        //send analytic event
        AnalyticsEvent.LevelStart("3L_Letter_lock");
        ar = AnalyticsEvent.LevelStart("3L_Letter_lock");
        // set start time as the time when player interact with item thefirst time 
        if (startTime == 0) 
        {
            startTime = doorControl.secondsElapsed;
        } 
        Debug.Log("LetCStart = " + ar.ToString() + startTime.ToString());

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
        // add custom params in analytical events: seconds played
        solveTime = doorControl.secondsElapsed - startTime;
        Dictionary<string, object> customParams = new Dictionary<string, object>();
        customParams.Add("seconds_played", solveTime.ToString());

        if (solvedPreLock == false)
        {
            InspectionSystem.Instance.TurnOff();            
            DoorLock.SetActive(false);
            solvedPreLock = true;
            // report event
            AnalyticsEvent.LevelComplete("3L_Letter_lock", customParams);
            ar = AnalyticsEvent.LevelComplete("3L_Letter_lock");
            Debug.Log("LetCFinish = " + ar.ToString() + doorControl.secondsElapsed.ToString() + "SolveTime=" + solveTime.ToString());
            // report custom event
            ReportSolve3LLetLock(solveTime);
        }
    }

    public override void QuitInteracting()
    {
        base.QuitInteracting();
        // add custom params
        Dictionary<string, object> customParams = new Dictionary<string, object>();
        customParams.Add("seconds_played", doorControl.secondsElapsed);
        if (solvedPreLock == false)
        {
            InspectionSystem.Instance.TurnOff();
            DoorLock.SetActive(false);
            AnalyticsEvent.LevelQuit("3L_Letter_lock", customParams);
            ar = AnalyticsEvent.LevelQuit("3L_Letter_lock");
            Debug.Log("LetQResult = " + ar.ToString() + doorControl.secondsElapsed.ToString());
        }
    }

    public void ReportSolve3LLetLock(float sTime){
        // custom event, report the time used to solve the lock
        AnalyticsEvent.Custom("3L_Letter_lock", new Dictionary<string, object>
        {
            { "time_elapsed", sTime }
        });
    }
}
