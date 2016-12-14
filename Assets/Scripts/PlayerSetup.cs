using UnityEngine;
using UnityEngine.Networking;
[RequireComponent (typeof(Player))]
public class PlayerSetup : NetworkBehaviour {
	[SerializeField]
	Behaviour[] componentsToDisable;

	[SerializeField]
	string remoteLayerName = "remotePlayer";

	Camera sceneCamera;


	void Start() {

		if (!isLocalPlayer) {
			disableComponents ();
			assignRemoteLayer ();
		} else {
			sceneCamera = Camera.main;
			if (sceneCamera != null) {
				sceneCamera.gameObject.SetActive (false);
			}

		}

		GetComponent<Player> ().Setup ();
			
	}

	public override void OnStartClient() { 
		base.OnStartClient ();

		string netId = GetComponent<NetworkIdentity> ().netId.ToString();
		Player player = GetComponent<Player> ();

		GameManager.RegisterPlayer (netId, player);
	}

	void disableComponents() {

		for (int i = 0; i < componentsToDisable.Length; i++) {
			componentsToDisable [i].enabled = false;
		}

	}

	void assignRemoteLayer() {
		gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
	}

	void OnDisable() {

		if (sceneCamera != null) {
			sceneCamera.gameObject.SetActive (true);
		}

		GameManager.DeRegisterPlayer (transform.name);
	}

}
