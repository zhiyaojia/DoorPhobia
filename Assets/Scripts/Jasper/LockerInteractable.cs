using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerInteractable : Interactable
{ 
    private AnimationControl lockerControl;

    [Header("Locker Settings")]
    public PhoneControl phoneControl;
    public PhoneInteractable phoneInteractable;

    void Start()
    {
        base.Start();
        lockerControl = GetComponent<AnimationControl>();
    }

    public override void Interact()
    {
        base.Interact();
        lockerControl.PlayAnimation();
        StartCoroutine("PhoneRing");
    }

    IEnumerator PhoneRing()
    {
        yield return new WaitForSeconds(3);

        phoneInteractable.Unlock();
        phoneControl.PlayMusic();
    }
}
