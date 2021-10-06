using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NunPaintingControl : MonoBehaviour
{
    private Animation anim;
    private Collider myCollider;

    void Start()
    {
        anim = GetComponent<Animation>();
        myCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            myCollider.enabled = false;
            anim.Play();
        }
    }
}
