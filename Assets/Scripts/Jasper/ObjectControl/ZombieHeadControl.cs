using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHeadControl : MonoBehaviour
{
    [Header("Neck")]
    public Transform neckTransform;
    public Vector3 neckStartRotation;
    public Vector3 neckTargetRotation;
    private Quaternion neckStartQuat;
    private Quaternion neckTargetQuat;

    [Header("Head")]
    public Transform headTransform;
    public Vector3 headStartRotation;
    public Vector3 headTargetRotation;
    private Quaternion headStartQuat;
    private Quaternion headTargetQuat;

    [Header("Timer Settings")]
    public float waitTime;
    public float animationTime;

    [Header("Hole Interact")]
    public HoleInteract holeInteract;

    void Start()
    {
        neckStartQuat = Quaternion.Euler(neckStartRotation);
        neckTargetQuat = Quaternion.Euler(neckTargetRotation);
        headStartQuat = Quaternion.Euler(headStartRotation);
        headTargetQuat = Quaternion.Euler(headTargetRotation);
    }

    public void TurnHead()
    {
        StartCoroutine(TurnHeadCouroutine());
    }

    IEnumerator TurnHeadCouroutine()
    {
        holeInteract.canQuit = false;

        yield return new WaitForSeconds(waitTime);

        holeInteract.canQuit = true;
        if (animationTime <= 0.0f)
        {
            neckTransform.localRotation = neckTargetQuat;
            headTransform.localRotation = headTargetQuat;
        }
        else
        {
            float timer = 0.0f;
            while (timer < animationTime)
            {
                neckTransform.localRotation = Quaternion.Slerp(neckStartQuat, neckTargetQuat, timer / animationTime);
                headTransform.localRotation = Quaternion.Slerp(headStartQuat, headTargetQuat, timer / animationTime);
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}
