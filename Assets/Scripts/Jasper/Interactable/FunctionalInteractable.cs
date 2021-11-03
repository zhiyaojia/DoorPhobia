using UnityEngine;
using UnityEngine.Events;

public class FunctionalInteractable : Interactable
{
    [Header("Function")]
    public UnityEvent action;

    void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        base.Interact();
        action.Invoke();
    }
}
