#define DEBUG 
#undef DEBUG// comment this if using debug mode
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;


public class PlayerControl : MonoBehaviour
{
    [Header("Cameras")]
    public GameObject focusCamera;
    public GameObject playerCamera;

    [Header("Interactable Icon")]
    public GameObject CrossHair;
    public GameObject HandIcon;
    public GameObject LockIcon;

    [HideInInspector] public Ray rayFromScreenCenter;
    [HideInInspector] public MovementControl playerMovement;

    private PlayerUIControl UIControl;

    public static PlayerControl Instance { get; set; }

    [Header("Analytics")]
    public float secondsElapsed = 0;
    AnalyticsResult ar;
    public int checkBagTimes = 0;
    public int solvePuzzles = 0;
    public int interactTimes = 0;
    Dictionary<string, object> customParams;
    public Dictionary<string, object> eachRoomStayTime;
    public Dictionary<string, object> eachRoomEnterTime;
    public string currentRoom;
    public float startTime;
    int clickTimes = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerMovement = GetComponent<MovementControl>();
        rayFromScreenCenter = new Ray(Vector3.zero, Vector3.up);
        UIControl = GetComponent<PlayerUIControl>();
        // initialize custom event params and current room        
        currentRoom = "BoyLivingRoom";
        customParams = new Dictionary<string, object>();
        customParams.Add("user_id", AnalyticsSessionInfo.userId);

        eachRoomStayTime = new Dictionary<string, object>();
        eachRoomStayTime.Add("BoyLivingRoom", secondsElapsed);
        eachRoomStayTime.Add("Corridor", secondsElapsed);
        eachRoomStayTime.Add("SecretRoom", secondsElapsed);
        eachRoomStayTime.Add("StudyRoom", secondsElapsed);
        eachRoomStayTime.Add("BathRoom", secondsElapsed);
        
        int times = 0;
        eachRoomEnterTime = new Dictionary<string, object>();
        eachRoomEnterTime.Add("BoyLivingRoom", times);
        eachRoomEnterTime.Add("Corridor", times);
        eachRoomEnterTime.Add("SecretRoom", times);
        eachRoomEnterTime.Add("StudyRoom", times);
        eachRoomEnterTime.Add("BathRoom", times);
    }

    void Update()
    {
        secondsElapsed += Time.deltaTime;        
        UpdateRay();
        if (Input.GetMouseButtonDown(0)) 
        {
            clickTimes += 1;
            customParams.Add("click" + clickTimes.ToString(), secondsElapsed);
        }
        
        // If user press Esc the game is ended.
        if (Input.GetKey("escape"))
        {
            // report every click times
            ReportEachClickTime(customParams);
            #if DEBUG
                ar = Analytics.CustomEvent("each_click_time");
                Debug.Log("each_click_time = " + ar.ToString());
            #endif     
            // report check bag times
            ReportCheckBagTimes(checkBagTimes);
            #if DEBUG
                ar = Analytics.CustomEvent("check_bag_times");
                Debug.Log("check_bag_times = " + ar.ToString());
            #endif
            //report total solved puzzles
            ReportSolvePuzzles(solvePuzzles);
            #if DEBUG
                ar = Analytics.CustomEvent("solve_puzzle_num");
                Debug.Log("solve_puzzle_num = " + ar.ToString());
            #endif
            //report total interact times
            ReportInteractableTimes(interactTimes);
            #if DEBUG
                ar = Analytics.CustomEvent("interactable_times");
                Debug.Log("interactable_times = " + ar.ToString());
            #endif
            //report game time
            customParams = new Dictionary<string, object>(); 
            customParams.Add("user_id", AnalyticsSessionInfo.userId);
            customParams.Add("seconds_played", secondsElapsed);
            AnalyticsEvent.LevelQuit("Quit_Game", customParams); 
            #if DEBUG
                ar = AnalyticsEvent.LevelQuit("Quit_Game");
                Debug.Log("Quit_Result = " + ar.ToString() + "Quit_time = " + secondsElapsed);
            #endif  
            // report each room stay time and enter times
            ReportEachRoomStayTime(eachRoomStayTime);
            ReportEachRoomEnterTime(eachRoomEnterTime);
            #if DEBUG
                ar = Analytics.CustomEvent("each_room_stay_time");
                Debug.Log("each_room_stay_time = " + ar.ToString());
                ar = Analytics.CustomEvent("each_room_enter_time");
                Debug.Log("each_room_enter_time = " + ar.ToString());
            #endif
            // quit game
            Application.Quit();
        }
    }

    private void UpdateRay()
    {
        if (playerCamera.activeInHierarchy == true)
        {
            rayFromScreenCenter.origin = playerCamera.transform.position;
            rayFromScreenCenter.direction = playerCamera.transform.forward;
        }
        else
        {
            rayFromScreenCenter.origin = focusCamera.transform.position;
            rayFromScreenCenter.direction = focusCamera.transform.forward;
        }
    }

    public void SetHandIcon(bool active)
    {
        HandIcon.SetActive(active);
    }

    public bool IsHandIconActive()
    {
        return HandIcon.activeInHierarchy;
    }

    public void SetLockIcon(bool active)
    {
        LockIcon.SetActive(active);
    }

    public void SetCrossHair(bool active)
    {
        CrossHair.SetActive(active);
    }

    public void ShowDialogue(string mes)
    {
        UIControl.ShowDialogue(mes);
    }

    public void ShowBagInfo(string mes, bool afterDialogue)
    {
        UIControl.ShowBagInfo(mes, afterDialogue? UIControl.DialogueMessageLifeTime:0.0f);
    }

    // This function can make camera focus on the interacting object
    public void FocusOnObject(Transform focusTransform, bool canRotateView)
    {
        playerCamera.SetActive(false);
        playerMovement.enabled = false;

        focusCamera.SetActive(true);
        focusCamera.transform.position = focusTransform.position;
        focusCamera.transform.rotation = focusTransform.rotation;
        focusCamera.GetComponent<CameraRotate>().enabled = canRotateView;

        CrossHair.SetActive(canRotateView);
        HandIcon.SetActive(false);
    }

    public void StopFocusOnObject()
    {
        playerCamera.SetActive(true);
        playerMovement.enabled = true;
        focusCamera.SetActive(false);
        CrossHair.SetActive(true);
        HandIcon.SetActive(true);
    }

    // custom analytic events
    public void ReportCheckBagTimes(int checkTimes){
        // custom event, report the time used to solve the lock
        AnalyticsEvent.Custom("check_bag_times", new Dictionary<string, object>
        {
            { "check_times", checkTimes }
        });
    }

    public void ReportSolvePuzzles(int solveTimes){
        // custom event, report the time used to solve the lock
        AnalyticsEvent.Custom("solve_puzzle_num", new Dictionary<string, object>
        {
            { "solve_num", solveTimes }
        });
    }

    public void ReportInteractableTimes(int interactTimes){
        // custom event, report the time used to solve the lock
        AnalyticsEvent.Custom("interactable_times", new Dictionary<string, object>
        {
            { "interact_times", interactTimes }
        });
    }

    public void ReportEachRoomStayTime(Dictionary<string, object> dict){
        // custom event, report the time used to solve the lock
        AnalyticsEvent.Custom("each_room_stay_time", dict);
    }

    public void ReportEachRoomEnterTime(Dictionary<string, object> dict){
        // custom event, report the time used to solve the lock
        AnalyticsEvent.Custom("each_room_enter_time", dict);
    }

    public void ReportEachClickTime(Dictionary<string, object> dict){
        // custom event, report the time used to solve the lock
        AnalyticsEvent.Custom("each_click_time", dict);
    }
}