using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class MapInteractable : Interactable
{
    public Transform focusPointTransform;

    private MapControl mapControl;
    AnalyticsResult ar;
    private float startTime = 0;
    private float solveTime;

    void Start()
    {
        base.Start();
        mapControl = GetComponentInChildren<MapControl>();
    }

    public override void Interact()
    {
        base.Interact();
        if (startTime == 0) 
        {
            startTime = mapControl.secondsElapsed;
        } 
        Debug.Log("MapStartTime = " + startTime.ToString());
        PlayerControl.Instance.FocusOnObject(focusPointTransform, true);
        mapControl.enabled = true;
    }

    public override void FinishInteracting()
    {
        base.FinishInteracting();
        // report custom event
        solveTime = mapControl.secondsElapsed - startTime;
        ReportSolve3LMap(solveTime);
        // debug
        ar = Analytics.CustomEvent("solve_3L_Map");
        Debug.Log("solve_3L_Map_Result = " + ar.ToString() + "Use_time = " + solveTime.ToString());
        PlayerControl.Instance.StopFocusOnObject();
        mapControl.enabled = false;
    }

    public override void QuitInteracting()
    {
        base.QuitInteracting();
        PlayerControl.Instance.StopFocusOnObject();
        mapControl.enabled = false;
    }
    public void ReportSolve3LMap(float sTime){
        // custom event, report the time used to solve the lock
        AnalyticsEvent.Custom("solve_3L_Map", new Dictionary<string, object>
        {
            { "solve_time", sTime }
        });
    }
}