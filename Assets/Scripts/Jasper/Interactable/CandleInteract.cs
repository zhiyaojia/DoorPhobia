using System.Collections;
using UnityEngine;

public class CandleInteract : Interactable
{
    [Header("CollectableInteraction Settings")]
    public GameObject candleEffect;
    private AudioSource audioSource;

    void Start() 
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        base.Interact();
        BagSystemControl.Instance.RemoveObject(3);
        FinishInteracting();
        StartCoroutine(LightUp());
    }

    IEnumerator LightUp()
    {
        audioSource.Play();
        yield return new WaitForSeconds(0.8f);
        candleEffect.SetActive(true);
    }
}
