using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathRoomClosetInteract : Interactable
{
    [Header("BathRoom Closet Interact Settings")]
    public GameObject Lock;
    public GameObject Sword;

    private AudioSource audioSource;
    private Animation myAnimation;

    void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        myAnimation = GetComponent<Animation>();
    }

    public override void Interact()
    {
        base.Interact();
        InspectionSystem.Instance.TurnOn();
        Lock.SetActive(true);
    }

    public override void FinishInteracting()
    {
        base.FinishInteracting();
        InspectionSystem.Instance.TurnOff();
        Lock.SetActive(false);
        audioSource.Play();
        myAnimation.Play();
        Sword.SetActive(true);
        enabled = false;
    }

    public override void QuitInteracting()
    {
        base.QuitInteracting();
        InspectionSystem.Instance.TurnOff();
        Lock.SetActive(false);
    }
}
