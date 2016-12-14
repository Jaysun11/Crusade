using UnityEngine;
using System.Collections;


[RequireComponent (typeof (Rigidbody))]
public class PlayerMotor : MonoBehaviour {

	private Vector3 velocity = Vector3.zero;

	private Vector3 rotation = Vector3.zero;


	private float jumpForce = 0f;

	private float camRotationX = 0f;
	private float currentCameraRotationX = 0f;

	[SerializeField]
	private float cameraRotationLimit = 85f;

	[SerializeField]
	private Camera cam;

	private Rigidbody rigid;

	void Start() {

		rigid = GetComponent<Rigidbody> ();

	}

	public void FixedUpdate() {
		//Process physics and perform movement
		PerformMovement ();
		PerformRotation ();

	}

	public void Move(Vector3 velocityFromController) {
		//Takes the movement vector from the controller
		velocity = velocityFromController;
	}
		
	public void ApplyJump(float jumpForce) {
		//Takes the movement vector from the controller
		this.jumpForce = jumpForce;
	}

	public void PerformMovement() {

		if (velocity != Vector3.zero) {
			rigid.MovePosition (rigid.position + velocity * Time.fixedDeltaTime);
		}

		if (this.jumpForce != 0f) {
			rigid.AddForce (new Vector3 (0, 7, 0), ForceMode.Impulse);
		}
	}

	public void Rotate(Vector3 rotationFromController) {
		//Takes the movement vector from the controller
		rotation = rotationFromController;
	}

	public void RotateCamera(float rotationFromCam) {
		//Takes the cam vector from the controller
		camRotationX = rotationFromCam;
	}


	public void PerformRotation() {

		rigid.MoveRotation (rigid.rotation * Quaternion.Euler (rotation));

		if (cam != null) {
			currentCameraRotationX -= camRotationX;
			currentCameraRotationX = Mathf.Clamp (currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
			cam.transform.localEulerAngles = new Vector3 (currentCameraRotationX, 0f, 0f);
		}

	}



}
