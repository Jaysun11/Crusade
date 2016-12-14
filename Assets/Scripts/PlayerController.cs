using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerMotor))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float speed = 5f;

	[SerializeField]
	private float lookSensitivity = 3f;

	[SerializeField]
	private float jumpForce = 2;

	private PlayerMotor motor;

	private bool canJump = false;

	void Start() {

		motor = GetComponent<PlayerMotor> ();

	}


	void Update() {
		//Where movement is handled

		float xMovement = Input.GetAxisRaw ("Horizontal");
		float zMovement = Input.GetAxisRaw ("Vertical"); ;

		Vector3 moveHorizontal = transform.right * xMovement;
		Vector3 moveVertical = transform.forward * zMovement;

		Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed; // SO we don't go diagonal faster

		motor.Move (velocity);

		//Calculate Rotation for turning around

		float yRotation = Input.GetAxisRaw ("Mouse X");

		Vector3 rotation = new Vector3 (0f, yRotation, 0f) * lookSensitivity;

		//apply rotation

		motor.Rotate (rotation);

		//Calculate Rotation for camera

		float xRotation = Input.GetAxisRaw ("Mouse Y");

		float camRotationX = xRotation * lookSensitivity;

		//apply rotation

		motor.RotateCamera (camRotationX);

		//Apply Jump Force
	

		float _jumpForce = 0;

		if (Input.GetButton("Jump") && canJump) {
			_jumpForce = this.jumpForce;
			canJump = false;
		} 

		motor.ApplyJump (_jumpForce);

	
	}


	void OnCollisionEnter(Collision other) {

		if (other.gameObject.CompareTag ("ground"))
		{
			canJump = true; // if the player is in the ground, the bool "canjump" become true
		}
		else
		{
			canJump = false; //if the player quit the ground, the bool become false
		}
			

	}

}
