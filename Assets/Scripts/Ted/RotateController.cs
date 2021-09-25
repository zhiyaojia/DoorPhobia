using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    float RotateDegree;
    bool InteractWithGlobe = false;
    bool RotateHalfComplete = false;
    bool ManShockAudioPlayed = false;
    public AudioSource ManShockAudio;

    // Update is called once per frame
    void Update()
    {   
        // Vector3 axis = new Vector3(0, 1, 0);
        // transform.Rotate(axis, 180);
        // transform.Rotate(0, 3.14f, 0);
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            // RotateHalf(0.1f);
            // while (RotateDegree < 180) 
            // {
            //     transform.Rotate(0, 0.1f, 0);
            //     RotateDegree += 0.1f;
            // }
            InteractWithGlobe = true;
        }
        if (InteractWithGlobe == true)
        {
            StartCoroutine("RotateH");
        }
        
    }
    IEnumerator RotateH()
    {   
        float RotateSpeed = 0.001f;
        while (RotateDegree < 180) 
        {   
            transform.Rotate(0, RotateSpeed, 0);
            RotateDegree += RotateSpeed;
            yield return null;
        }
        if (!ManShockAudioPlayed)
        {
            ManShockAudio.Play();
            ManShockAudioPlayed = true;
        }
        
    }
}
