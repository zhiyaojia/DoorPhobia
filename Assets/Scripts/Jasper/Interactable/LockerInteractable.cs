using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerInteractable : Interactable
{ 
    private Animation lockerControl;
    private AudioSource audioSource;

    [Header("Locker Settings")]
    public GameObject loveLetter;

    void Start()
    {
        base.Start();
        lockerControl = GetComponent<Animation>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        base.Interact();
        lockerControl.Play();
        audioSource.Play();
        loveLetter.SetActive(true);
        enabled = false;
        FinishInteracting();
    }
}
