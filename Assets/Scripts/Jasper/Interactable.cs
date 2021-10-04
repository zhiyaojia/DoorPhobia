using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Tooltip("Check this box if you want this object to be directly interacted without any condition")]
    public bool noConditionNeed = false;

    [Tooltip("Max distance that player can interact with this object")]
    public float maxInteractableDistance = 3.5f;

    [Tooltip("Check this box if you want to change the view when interacting with this object")]
    public bool focusOnView = false;
    [ConditionalHide("focusOnView", true)]
    public Transform focusPointTransform;
    [ConditionalHide("Nothing but freeze player's movement", true)]
    public bool canRotateView = false;

    protected Collider myCollider;
    protected bool alreadyInteracted = false;
    protected bool alreadyHovered = false;
    protected bool meetInteractCondition = false;
    protected Transform playerCameraTransform;

    protected bool IsIntereacting = false;

    public virtual void Start()
    {
        myCollider = GetComponent<Collider>();
        if (myCollider == null)
        {
            Debug.LogError(transform.name + " needs a collider in order to use interactable");
        }

        if (noConditionNeed == true)
        {
            meetInteractCondition = true;
        }
    }

    public void Update()
    {
        UpdateCondition();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (focusOnView)
            {
                StopInteracting();
                alreadyInteracted = false;
            }
        }

        float distanceWithPlayer = Vector3.Distance(PlayerControl.Instance.transform.position, transform.position);
        if (distanceWithPlayer < maxInteractableDistance)
        {
            RaycastHit hit;
            if (myCollider.Raycast(PlayerControl.Instance.rayFromScreenCenter, out hit, maxInteractableDistance))
            {
                if (alreadyHovered == false)
                {
                    alreadyHovered = true;
                    if (meetInteractCondition)
                    {
                        PlayerControl.Instance.SetHandIcon(true);
                    }
                    else
                    {
                        PlayerControl.Instance.SetLockIcon(true);
                    }
                }
                if (Input.GetMouseButtonDown(0) && alreadyInteracted == false && meetInteractCondition)
                {
                    alreadyInteracted = true;
                    Interact();
                }
            }
            else
            {
                if (alreadyHovered == true)
                {
                    alreadyHovered = false;
                    alreadyInteracted = false;
                    PlayerControl.Instance.SetHandIcon(false);
                    PlayerControl.Instance.SetLockIcon(false);
                }
            }
        }
        else
        {
            if (alreadyHovered == true)
            {
                alreadyInteracted = false;
                alreadyHovered = false;
                PlayerControl.Instance.SetHandIcon(false);
                PlayerControl.Instance.SetLockIcon(false);
            }
        }
    }

    public void Unlock()
    {
        meetInteractCondition = true;
    }

    public virtual void UpdateCondition() { }

    public virtual void Interact()
    {
        if (focusOnView)
        {
            PlayerControl.Instance.FocusOnObject(focusPointTransform, canRotateView);
        }
        IsIntereacting = true;
    }

    public virtual void StopInteracting()
    {
        PlayerControl.Instance.StopFocusOnObject(canRotateView);
        IsIntereacting = false;
    }
}