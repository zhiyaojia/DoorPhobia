using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Tooltip("Check this box if you want this object to be directly interacted without any condition")]
    public bool noConditionNeed = false;
    //[Tooltip("Uncheck this box if this object needs to be unlocked before real interaction")]
    protected bool solvedPreLock = false;

    [Tooltip("Max distance that player can interact with this object")]
    public float maxInteractableDistance = 3.5f;

    public bool needDialogue = false;
    [ConditionalHide("needDialogue", true)] [TextArea]
    public string message;
    [ConditionalHide("needDialogue", true)]
    public bool onlyShowWhenLocked = false;

    protected Collider myCollider;
    protected bool alreadyInteracted = false;
    protected bool alreadyHovered = false;
    protected bool meetInteractCondition = false;
    protected Transform playerCameraTransform;

    [HideInInspector]public bool canQuit = true;

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
        float distanceWithPlayer = Vector3.Distance(PlayerControl.Instance.transform.position, transform.position);
        if (distanceWithPlayer < maxInteractableDistance)
        {
            RaycastHit hit;
            if (myCollider.Raycast(PlayerControl.Instance.rayFromScreenCenter, out hit, maxInteractableDistance * 3.0f))
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
                if (Input.GetMouseButtonDown(0) && alreadyInteracted == false && InspectionSystem.Instance.light.activeInHierarchy == false)
                {
                    if (meetInteractCondition)
                    {
                        Interact();
                    }
                    if (needDialogue)
                    {
                        if ((onlyShowWhenLocked && meetInteractCondition == false) || onlyShowWhenLocked == false && solvedPreLock == false)
                        {
                            PlayerControl.Instance.ShowDialogue(message);
                        }
                    }
                }
            }
            else
            {
                if (alreadyHovered == true)
                {
                    alreadyHovered = false;
                    PlayerControl.Instance.SetHandIcon(false);
                    PlayerControl.Instance.SetLockIcon(false);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && alreadyInteracted == true && canQuit == true)
            {
                QuitInteracting();
            }
        }
        else
        {
            if (alreadyHovered == true)
            {
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

    public virtual void Interact()
    {
        alreadyInteracted = true;
    }

    public virtual void FinishInteracting() // This is called when finish puzzle
    {
        alreadyInteracted = false;
        alreadyHovered = false;
    }

    public virtual void QuitInteracting() // This is called when press space
    {
        alreadyInteracted = false;
        alreadyHovered = false;
    }

    public void DisablePlayerMovement()
    {
        PlayerControl.Instance.playerMovement.StopMove();
    }

    public void EnablePlayerMovement()
    {
        PlayerControl.Instance.playerMovement.StartMove();
    }
}