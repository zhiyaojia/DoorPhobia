using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerInteractable : Interactable
{ 
    private Animation lockerControl;
    private AudioSource audio;

    [Header("Locker Settings")]
    public PhoneControl phoneControl;
    public PhoneInteractable phoneInteractable;

    void Start()
    {
        base.Start();
        lockerControl = GetComponent<Animation>();
        audio = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        base.Interact();
        lockerControl.Play();
        audio.Play();
        StartCoroutine("PhoneRing");
    }

    IEnumerator PhoneRing()
    {
        yield return new WaitForSeconds(3);

        phoneInteractable.Unlock();
        phoneControl.PlayMusic();
    }
}
