using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [Header("Interactable Setting")]
    public float maxInteractableDistance = 5.0f;
    public GameObject focusCamera;
    [Header("Interactable UI")]
    public GameObject CrossHair;
    public GameObject HandIcon;
    public GameObject LockIcon;
    [HideInInspector] public Camera playerCamera;
    [HideInInspector] public Ray rayFromScreenCenter;
    [HideInInspector] public MovementControl playerMovement;

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
    }

    void Update()
    {
        UpdateRay();
    }

    private void UpdateRay()
    {
        rayFromScreenCenter.origin = playerCamera.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, playerCamera.nearClipPlane));
        rayFromScreenCenter.direction = transform.forward;
    }

    public void SetHandIcon(bool active)
    {
        HandIcon.SetActive(active);
    }

    public void SetLockIcon(bool active)
    {
        LockIcon.SetActive(active);
    }

    // This function can make camera focus on the interacting object
    public void FocusOnObject(Transform focusTransform)
    {
        playerCamera.enabled = false;
        playerMovement.enabled = false;
        focusCamera.SetActive(true);
        focusCamera.transform.position = focusTransform.position;
        focusCamera.transform.rotation = focusTransform.rotation;

        SetHandIcon(false);
        CrossHair.SetActive(false);
    }

    public void StopFocusOnObject()
    {
        playerCamera.enabled = true;
        playerMovement.enabled = true;
        focusCamera.SetActive(false);
        CrossHair.SetActive(true);
        SetHandIcon(true);
    }
}
