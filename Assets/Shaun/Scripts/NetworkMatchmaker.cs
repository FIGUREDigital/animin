using UnityEngine;


public class NetworkMatchmaker : MonoBehaviour {

	private 	bool					__doTraces;



	// CORE
	// ---------------------------------------------------------------------------------------------------------------------------------------------------

	public void Start() {
		//Debug.Log("NetworkMatchmaker Start");

		//PhotonNetwork.logLevel = PhotonLogLevel.Full;

		__doTraces = true;
		//__doTraces = false;
	}

	//public void Update () {}

	
	// PUBLIC (EVENTS)
	// ---------------------------------------------------------------------------------------------------------------------------------------------------

	public void OnConnectedToMaster() {
		if (__doTraces) Debug.Log("OnConnectedToMaster" + "\n");
	}

	public void OnConnectedToPhoton() {
		if (__doTraces) Debug.Log("OnConnectedToPhoton" + "\n");
	}

	public void OnConnectionFail() {
		if (__doTraces) Debug.Log("OnConnectionFail" + "\n");
	}

	public void OnCreatedRoom() 
	{
		if (__doTraces) Debug.Log("OnCreatedRoom" + "\n");
	}

	public void OnCustomAuthenticationFailed() 
	{
		if (__doTraces) Debug.Log("OnCustomAuthenticationFailed" + "\n");
	}

	public void OnDisconnectedFromPhoton() 
	{
		if (__doTraces) Debug.Log("OnDisconnectedFromPhoton" + "\n");
	}

	public void OnFailedToConnectToPhoton() 
	{
		if (__doTraces) Debug.Log("OnFailedToConnectToPhoton" + "\n");
	}

	public void OnJoinedLobby() {
		if (__doTraces) Debug.Log("OnJoinedLobby\t:\t" + GameController.instance.multiplayerType + "\n");

		if (GameController.instance.multiplayerType == MultiplayerType.FRIEND_START) {
			if (__doTraces) Debug.Log ("GameController.instance.username\t:\t"	+	GameController.instance.username + "\n");
			PhotonNetwork.CreateRoom(PlayerProfileData.ActiveProfile.ProfileName);
		} else if (GameController.instance.multiplayerType == MultiplayerType.FRIEND_JOIN) {
			Debug.Log ("GameController.instance.friendUsername\t:\t"+	GameController.instance.friendUsername);
			PhotonNetwork.JoinRoom(GameController.instance.friendUsername);
		} else if (GameController.instance.multiplayerType == MultiplayerType.RANDOM) {
			PhotonNetwork.JoinRandomRoom();
		}
	}

	public void OnJoinedRoom()
	{
		if (__doTraces) Debug.Log("OnJoinedRoom" + "\n");

		if (__doTraces) Debug.Log("PhotonNetwork.playerList.Length\t:\t"	+	PhotonNetwork.playerList.Length + "\n");

		GameController.instance.StartGame();
	}
	
	public void OnLeftLobby() {
		if (__doTraces) Debug.Log("OnLeftLobby" + "\n");
	}

	public void OnLeftRoom() {
		if (__doTraces) Debug.Log("OnLeftRoom" + "\n");
	}

	public void OnMasterClientSwitched() {
		if (__doTraces) Debug.Log("OnMasterClientSwitched" + "\n");
	}

	public void OnPhotonCreateRoomFailed() {
		if (__doTraces) Debug.Log("OnPhotonCreateRoomFailed" + "\n");
	}

	public void OnPhotonCustomRoomPropertiesChanged() {
		if (__doTraces) Debug.Log("OnPhotonCustomRoomPropertiesChanged" + "\n");
	}

	public void OnPhotonInstantiate() {
		if (__doTraces) Debug.Log("OnPhotonInstantiate" + "\n");
	}

	public void OnPhotonJoinRoomFailed() 
	{
		if (__doTraces) Debug.Log("OnPhotonJoinRoomFailed" + "\n");

		if(GameController.instance.multiplayerType == MultiplayerType.FRIEND_JOIN)
		{
			UIGlobalVariablesScript.Singleton.EnterFriendsNameChat.GetComponent<EnterFriendsNameSubmitScript>().ReportingLabel.text = "Failed to find friend";
			UIGlobalVariablesScript.Singleton.EnterFriendsNameChat.SetActive(true);
		}


		//
	}

	public void OnPhotonMaxCccuReached() {
		if (__doTraces) Debug.Log("OnPhotonMaxCccuReached" + "\n");
	}

	// NEW PLAYER JOINED THE ROOM!
	public void OnPhotonPlayerConnected(PhotonPlayer player) {
		if (__doTraces) Debug.Log("OnPhotonPlayerConnected\t:\t" + player.ID + "\n");

		// isMasterClient is the equivalent of the host, the player with the lowest ID in the room - all clients can check this!
		if (__doTraces) Debug.Log("PhotonNetwork.isMasterClient\t:\t"	+	PhotonNetwork.isMasterClient + "\n");
	}

	// PLAYER LEFT THE ROOM!
	void OnPhotonPlayerDisconnected(PhotonPlayer player) {
		if (__doTraces) Debug.Log("OnPhotonPlayerDisconnected\t:\t" + player.ID + "\n");

		// isMasterClient is the equivalent of the host, the player with the lowest ID in the room - all clients can check this!
		if (__doTraces) Debug.Log("PhotonNetwork.player.ID\t:\t"	+	PhotonNetwork.player.ID + "\n");
	}

	public void OnPhotonPlayerPropertiesChanged() {
		if (__doTraces) Debug.Log("OnPhotonPlayerPropertiesChanged" + "\n");
	}

	public void OnPhotonRandomJoinFailed() {
		if (__doTraces) Debug.Log("OnPhotonRandomJoinFailed" + "\n");

		// ROOM DOESN'T EXIST SO CREATE ONE!
		PhotonNetwork.CreateRoom(null);
	}

	public void OnPhotonSerializeView() {
		if (__doTraces) Debug.Log("OnPhotonSerializeView" + "\n");
	}

	public void OnReceivedRoomListUpdate() {
		if (__doTraces) Debug.Log("OnReceivedRoomListUpdate" + "\n");
	}

	public void OnUpdatedFriendList() {
		if (__doTraces) Debug.Log("OnUpdatedFriendList" + "\n");
	}

	public void OnWebRpcResponse() {
		if (__doTraces) Debug.Log("OnWebRpcResponse" + "\n");
	}

	
	// PRIVATE
	// ---------------------------------------------------------------------------------------------------------------------------------------------------
}