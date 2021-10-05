using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [Header("Interactable Setting")]
    public GameObject focusCamera;
    [Header("Interactable UI")]
    public GameObject CrossHair;
    public GameObject HandIcon;
    public GameObject LockIcon;
    public GameObject Dialogue;
    public Text DialogueText;
    [HideInInspector] public Camera playerCamera;
    [HideInInspector] public Ray rayFromScreenCenter;
    [HideInInspector] public MovementControl playerMovement;

    private float playerSpeed;
    private IEnumerator showDialogueCouroutine = null;

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
        playerCamera = GetComponentInChildren<Camera>();
        playerSpeed = playerMovement.MovementSpeed;
    }

    void Update()
    {
        UpdateRay();
    }

    private void UpdateRay()
    {
        rayFromScreenCenter.origin = playerCamera.transform.position;
        rayFromScreenCenter.direction = playerCamera.transform.forward;
        Debug.DrawRay(rayFromScreenCenter.origin, rayFromScreenCenter.direction * 10.0f, Color.red);
    }

    public void SetHandIcon(bool active)
    {
        HandIcon.SetActive(active);
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
        if (showDialogueCouroutine != null)
        {
            StopCoroutine(showDialogueCouroutine);
        }
        showDialogueCouroutine = TurnOnDialogue(mes);
        StartCoroutine(showDialogueCouroutine);
    }

    IEnumerator TurnOnDialogue(string mes)
    {
        DialogueText.text = mes;
        Dialogue.SetActive(true);

        yield return new WaitForSeconds(3);

        Dialogue.SetActive(false);
    }

    // This function can make camera focus on the interacting object
    public void FocusOnObject(Transform focusTransform, bool canRotateView)
    {
        if (canRotateView) // currently just use for map
        {
            playerMovement.MovementSpeed = 0;
        }
        else
        {
            playerCamera.enabled = false;
            focusCamera.SetActive(true);
            focusCamera.transform.position = focusTransform.position;
            focusCamera.transform.rotation = focusTransform.rotation;
            CrossHair.SetActive(false);
        }

        SetHandIcon(false);
    }

    public void StopFocusOnObject(bool canRotateView)
    {
        if (canRotateView)
        {
            playerMovement.MovementSpeed = playerSpeed;
        }
        else
        {
            playerCamera.enabled = true;
            playerMovement.enabled = true;
            focusCamera.SetActive(false);
            CrossHair.SetActive(true);
        }
        SetHandIcon(true);
    }
}