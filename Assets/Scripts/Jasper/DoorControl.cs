using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    private new Animation animation;
    private bool isOpen = false;
    private bool isRight = false;

    void Start()
    {
        animation = GetComponent<Animation>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerAnimation();
        }
    }

    public void PlayerAnimation()
    {
        Vector3 fromPlayer = (PlayerControl.Instance.transform.position - transform.position).normalized;
        float dot = Vector3.Dot(fromPlayer, transform.forward);

        if (isOpen == false)
        {
            if (dot >= 0)// player is at right side of the door, so should use left operation
            {
                print("left open");
                animation.Play("leftOpen");
                isRight = false;
            }
            else
            {
                print("right open");
                animation.Play("rightOpen");
                isRight = true;
            }
        }
        else
        {
            if (isRight)
            {
                print("right close");
                animation.Play("rightClose");
            }
            else
            {

                print("left close");
                animation.Play("leftClose");
            }
        }
        isOpen = !isOpen;
    }
}
