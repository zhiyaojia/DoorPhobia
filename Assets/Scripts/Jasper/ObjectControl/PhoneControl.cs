using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneControl : MonoBehaviour
{
    public GameObject TwistedPainting;
    private AudioSource source;
    private PhoneInteractable phoneInteractable;

    void Start()
    {
        source = GetComponent<AudioSource>();
        phoneInteractable = GetComponent<PhoneInteractable>();
    }

    public void PlayMusic()
    {
        source.Play();
    }

    public void Interact()
    {
        TwistedPainting.SetActive(true);
        source.Stop();
    }
}
