using UnityEngine;

public class CandleInteract : Interactable
{
    [Header("CollectableInteraction Settings")]
    public GameObject torchEffect;

    void Start() 
    {
        base.Start();
    }

    public override void Interact()
    {
        base.Interact();
        torchEffect.SetActive(true);
    }
}
