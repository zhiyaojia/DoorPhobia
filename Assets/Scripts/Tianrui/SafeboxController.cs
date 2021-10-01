using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeboxController : MonoBehaviour
{
    public GameObject Door;
    public GameObject Handle;
    float HandleRotateDegree = 0;
    float DoorRotateDegree = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Unlock();
        }
    }
    void Unlock()
    {
        Debug.Log("unlock successfully");
        StartCoroutine("TranslateAndRotateDoor");
    }
    IEnumerator TranslateAndRotateDoor()
    {
        float RotateSpeed = -0.01f;
        while (HandleRotateDegree > -180)
        {
            Handle.transform.Rotate(0, RotateSpeed, 0);
            HandleRotateDegree += RotateSpeed;
            yield return null;
        }
        while (DoorRotateDegree > -180)
        {
            Door.transform.Rotate(0, 0, RotateSpeed);
            DoorRotateDegree += RotateSpeed;
            yield return null;
        }
    }
}
