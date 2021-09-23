using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    float RotateDegree;
    bool RotateHalfComplete = false;

    // Update is called once per frame
    void Update()
    {   
        // Vector3 axis = new Vector3(0, 1, 0);
        // transform.Rotate(axis, 180);
        // transform.Rotate(0, 3.14f, 0);
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            // RotateHalf(0.1f);
            while (RotateDegree < 180) 
            {
                transform.Rotate(0, 0.1f, 0);
                RotateDegree += 0.1f;
            }
        }
        
    }
    void RotateHalf(float RotateSpeed)
    {
        if (RotateDegree < 180) 
        {
            transform.Rotate(0, RotateSpeed, 0);
            RotateDegree += RotateSpeed;
        }
        else
        {
            RotateHalfComplete = true;
        }
    }
}
