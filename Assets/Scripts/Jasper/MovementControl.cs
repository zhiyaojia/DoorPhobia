using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
	public float MovementSpeed = 1;
	public float horizontalSpeed = 1f;
	public float verticalSpeed = 1f;

	private CharacterController characterController;

	private float xRotation = 0.0f;
	private float yRotation = 0.0f;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		characterController = GetComponent<CharacterController>();
		Vector3 rotation = transform.rotation.eulerAngles;
		xRotation = rotation.x;
		yRotation = rotation.y;
	}

	void Update()
	{
		float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
		float vertical = Input.GetAxis("Vertical") * MovementSpeed;
		Vector3 forward = transform.forward;
		forward.y = 0.0f;
		Vector3 right = transform.right;
		right.y = 0.0f;
		characterController.Move((right * horizontal + forward * vertical) * Time.deltaTime);

		float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
		float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;

		yRotation += mouseX;
		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90, 90);
		transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
	}
}