using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    // 控制地球仪旋转
    // float RotateDegree;
    // float ERotateDegree;
    bool InteractWithGlobe = false;
    bool RotateHalfComplete = false;
    bool ManShockAudioPlayed = false;
    public AudioSource ManShockAudio;
    public GameObject eyeBallLeft;
    public GameObject eyeBallRight;
    private Animation anim;
    private Interactable bookInteract;

    private void Start()
    {
        anim = GetComponentInParent<Animation>();
        bookInteract = GetComponent<Interactable>();
    }
    public void rotateGlobe()
    {
        StartCoroutine("RotateH");
    }
    IEnumerator RotateH()
    {
        anim.Play();
        yield return null;
    }
    // IEnumerator RotateEyeBalls()
    // {
    //     // 旋转速度
    //     float RotateSpeed = 0.0005f;
    //     // 旋转180度
    //     while (ERotateDegree < 360) 
    //     {   
    //         eyeBallLeft.transform.Rotate(RotateSpeed,0,  0);
    //         eyeBallRight.transform.Rotate(RotateSpeed,0,  0);
    //         ERotateDegree += RotateSpeed;
    //         yield return null;
    //     }
    // }
}
