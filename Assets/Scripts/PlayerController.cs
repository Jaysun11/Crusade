using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float speed = 5f;
	[SerializeField]
	private float lookSensitivity = 3f;


	[SerializeField]
	private LayerMask environmentMask;

	private bool jumping = false;
	private float lastJump = 0;

	// Component caching
	private PlayerMotor motor;



	void Start ()
	{
		motor = GetComponent<PlayerMotor>();
		jumping = false;

	}

	void Update ()
	{
		if (PauseMenu.IsOn)
		{
			if (Cursor.lockState != CursorLockMode.None)
				Cursor.lockState = CursorLockMode.None;

			motor.Move(Vector3.zero);
			motor.Rotate(Vector3.zero);
			motor.RotateCamera(0f);

			return;
		}

		if (Cursor.lockState != CursorLockMode.Locked)
		{
			Cursor.lockState = CursorLockMode.Locked;
		}



		//Calculate movement velocity as a 3D vector
		float _xMov = Input.GetAxis("Horizontal");
		float _zMov = Input.GetAxis("Vertical");

		Vector3 _movHorizontal = transform.right * _xMov;
		Vector3 _movVertical = transform.forward * _zMov;

		// Final movement vector
		Vector3 _velocity = (_movHorizontal + _movVertical) * speed;

		//Apply movement
		motor.Move(_velocity);

		//Calculate rotation as a 3D vector (turning around)
		float _yRot = Input.GetAxisRaw("Mouse X");

		Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

		//Apply rotation
		motor.Rotate(_rotation);

		//Calculate camera rotation as a 3D vector (turning around)
		float _xRot = Input.GetAxisRaw("Mouse Y");

		float _cameraRotationX = _xRot * lookSensitivity;

		//Apply camera rotation
		motor.RotateCamera(_cameraRotationX);

		checkJump (lastJump);

		// Calculate the jumpForce based on player input
		Vector3 jumpForce = Vector3.zero;
		if (Input.GetButton ("Jump") && !jumping) {
			jumpForce = Vector3.up * 1000;
			jumping = true;
			lastJump = Time.time;
		}

		motor.ApplyJump (jumpForce);

	}

	void checkJump(float lastJump) {
		if (Time.time - lastJump >= 1.0f) {
			jumping = false;
		}

	}

}
