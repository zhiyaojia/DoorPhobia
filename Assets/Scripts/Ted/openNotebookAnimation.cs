using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openNotebookAnimation : MonoBehaviour
{
    private Animation anim;

    private void Start() {
        anim = GetComponent<Animation>();
    }
    public void OpenBook()
    {
        StartCoroutine("openBook");
    }

    IEnumerator openBook()
    {   
        anim.Play();
        yield return null;
    }
}
