using UnityEngine;

public class CandleInteract : Interactable
{
    [Header("CollectableInteraction Settings")]

    private Torchelight lightEffect;

    void Start() 
    {
        base.Start();
        lightEffect = GetComponent<Torchelight>();
    }

    public override void Interact()
    {
        base.Interact();
        lightEffect.IntensityLight = lightEffect.MaxLightIntensity;
    }
}
