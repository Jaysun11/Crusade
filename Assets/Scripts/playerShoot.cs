using UnityEngine;
using UnityEngine.Networking;

public class playerShoot : NetworkBehaviour {

	private const string PLAYER_TAG = "Player";

	public PlayerWeapon weapon;

	[SerializeField]
	private Camera cam;

	[SerializeField]
	private LayerMask mask;

	void Start() {
		if (cam == null) {
			Debug.LogError ("NO CAMERA REFERENCED FOR SHOOT");
			this.enabled = false;
		}
	}

	void Update() {
		
		if (Input.GetButtonDown ("Fire1")) {
			Shoot ();
		}
	}


	[Client]
	void Shoot() {
		RaycastHit hit;
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask) ){
			if (hit.collider.tag == PLAYER_TAG) {
				CmdPlayerHasBeenShot (hit.collider.name, weapon.damage);
			}
		}


	}

	[Command]
	void CmdPlayerHasBeenShot(string PlayerID, int damage) {
		Debug.Log (PlayerID + " Has been shot" + " by " + gameObject.name);

		Player player = GameManager.GetPlayer (PlayerID);
		player.RpcTakeDamage (damage);
	}

}
