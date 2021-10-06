using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    private new Animation animation;
    private Interactable doorInteractable;
    private bool isOpen = false;
    private bool isRight = false;

    void Start()
    {
        animation = GetComponent<Animation>();
        doorInteractable = GetComponentInChildren<Interactable>();
    }

    public void PlayerAnimation()
    {
        Vector3 fromPlayer = (PlayerControl.Instance.transform.position - transform.position).normalized;
        float dot = Vector3.Dot(fromPlayer, transform.forward);

        if (isOpen == false)
        {
            if (dot >= 0)// player is at right side of the door, so should use left operation
            {
                animation.Play("LeftOpen");
                isRight = false;
            }
            else
            {
                animation.Play("RightOpen");
                isRight = true;
            }
        }
        else
        {
            if (isRight)
            {
                animation.Play("RightClose");
            }
            else
            {
                animation.Play("LeftClose");
            }
        }
        isOpen = !isOpen;
    }

    public void StopAnimation()
    {
        doorInteractable.StopInteracting();
    }
}
