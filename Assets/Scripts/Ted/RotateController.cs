using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class RotateController : MonoBehaviour
{
    // 控制地球仪旋转
    // float RotateDegree;
    // float ERotateDegree;
    bool InteractingWithGlobe = false;
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
        ReportRotateGlobe();
    }
    IEnumerator RotateH()
    {
        anim.Play();
        yield return null;
    }
    public void ReportRotateGlobe(){
    AnalyticsEvent.Custom("rotate_globe", new Dictionary<string, object>
    {
        { "time_elapsed", Time.timeSinceLevelLoad }
    });
}
}
