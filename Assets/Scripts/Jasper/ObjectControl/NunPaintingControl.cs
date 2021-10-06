using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NunPaintingControl : MonoBehaviour
{
    private AnimationControl anim;
    private Collider myCollider;

    void Start()
    {
        anim = GetComponent<AnimationControl>();
        myCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            myCollider.enabled = false;
            anim.PlayAnimation();
        }
    }
}
