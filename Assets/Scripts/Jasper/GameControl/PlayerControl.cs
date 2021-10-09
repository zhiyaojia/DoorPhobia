using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    void Update()
    {
        UpdateRay();
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
}