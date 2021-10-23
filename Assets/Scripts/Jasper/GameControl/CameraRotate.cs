using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float horizontalSpeed = 1f;
    public float verticalSpeed = 1f;
    public float moveDeltaLimit = 20.0f;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    void Start()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        xRotation = rotation.x;
        yRotation = rotation.y;
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        //float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
        //float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;
        float mouseX = Mathf.Clamp(Input.GetAxis("Mouse X") * horizontalSpeed, -moveDeltaLimit, moveDeltaLimit);
        float mouseY = Mathf.Clamp(Input.GetAxis("Mouse Y") * verticalSpeed, -moveDeltaLimit, moveDeltaLimit);

        yRotation += mouseX;
        xRotation -= mouseY;
        //yRotation += Mathf.Clamp(mouseX, -moveDeltaLimit, moveDeltaLimit);
        //xRotation -= Mathf.Clamp(mouseY, -moveDeltaLimit, moveDeltaLimit);
        xRotation = Mathf.Clamp(xRotation, -80, 70);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0.0f);
    }
}
