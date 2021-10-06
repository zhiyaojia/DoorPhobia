using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectionSystem : MonoBehaviour
{
    public GameObject canvas;
    public GameObject camera;
    public GameObject volume;
    public GameObject light;

    public static InspectionSystem Instance { get; set; }
    private bool handIconActive = false;

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

    public void TurnOn()
    {
        PlayerControl.Instance.playerMovement.StopMove();
        Cursor.lockState = CursorLockMode.None;
        canvas.SetActive(true);
        camera.SetActive(true);
        volume.SetActive(true);
        light.SetActive(true);
        handIconActive = PlayerControl.Instance.IsHandIconActive();
        PlayerControl.Instance.SetHandIcon(false);
    }

    public void TurnOff()
    {
        PlayerControl.Instance.playerMovement.StartMove();
        Cursor.lockState = CursorLockMode.Locked;
        canvas.SetActive(false);
        camera.SetActive(false);
        volume.SetActive(false);
        light.SetActive(false);
        PlayerControl.Instance.SetHandIcon(handIconActive);
    }
}
