using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public new Transform camera;
	public Transform anchor;

	public float yOffset = 1.0f;
	public float zOffset = 1.0f;

	public float pitch = 0.0f;
	public float yaw = 0.0f;

	public float maxPitch = 90.0f;
	public float minPitch = -90.0f;

	// Start is called before the first frame update
	void Start()
	{
		if (!camera) {
			camera = GetComponent<Camera>().transform;
			pitch = camera.eulerAngles.x;
			yaw = camera.eulerAngles.x;
		}
	}

	void OnEnable()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
	}

	// Update is called once per frame
	void Update()
	{
		float mx = Input.GetAxis("Mouse X");
		float my = Input.GetAxis("Mouse Y");

		pitch -= my;
		yaw   += mx;

		pitch = Mathf.Min(pitch, maxPitch);
		pitch = Mathf.Max(pitch, minPitch);

		Vector3 cam = camera.localEulerAngles;
		Vector3 anc = anchor.eulerAngles;

		cam.x = pitch;
		anc.y = yaw;

		anchor.eulerAngles = anc;
		camera.localEulerAngles = cam;
	}
}
