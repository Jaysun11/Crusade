using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public MatchSettings matchSettings;


	void Awake() {

		if (instance != null) {
			Debug.LogError ("MORE THAN ONE GAME MANAGER IN SCENE");
		} else {
			instance = this;
		}
	}

	#region Player Tracking
	private const string PLAYER_PREFIX = "Player ";

	private static Dictionary<string, Player> players = new Dictionary<string, Player>();

	public static void RegisterPlayer(string netID, Player player) {

		string playerID = PLAYER_PREFIX + netID;
		players.Add (playerID, player);
		player.transform.name = playerID;
	}

	public static void DeRegisterPlayer(string playerId) {

		players.Remove (playerId);
	}

	public static Player GetPlayer (string playerID) {
		return players [playerID];
	}

//	void OnGUI() {
//		GUILayout.BeginArea (new Rect (200, 200, 200, 500));
//		GUILayout.BeginVertical ();
//
//		foreach (string playerString in players.Keys) {
//			GUILayout.Label (playerString + "   -   " + players [playerString].transform.name);
//		}
//
//		GUILayout.EndVertical ();
//		GUILayout.EndArea();
//	}
	#endregion
}
