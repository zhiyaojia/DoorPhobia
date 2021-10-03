using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeBoxController : MonoBehaviour
{
    public GameObject Door;
    public GameObject Handle;
    float DoorRotateDegree = 0;
    float HandleRotateDegree = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            Unlock();
        }
    }
    void Unlock()
    {
        StartCoroutine("TranslateAndRotateDoor");
    }
    IEnumerator TranslateAndRotateDoor()
    {
        while (HandleRotateDegree > -180)
        {
            Handle.transform.Translate(0, -0.01f, 0);
            HandleRotateDegree += -0.01f;
            yield return null;
        }

        while (DoorRotateDegree > -180)
        {
            Door.transform.Rotate(0, 0, -0.01f);
            DoorRotateDegree += -0.01f;
            yield return null;
        }
    }
}
