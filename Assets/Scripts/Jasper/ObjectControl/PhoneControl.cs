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

    IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(3);
        source.Play();
    }

    public void PhoneRing()
    {
        StartCoroutine(PlayMusic());
    }

    public void Interact()
    {
        TwistedPainting.SetActive(true);
        source.Stop();
    }
}
