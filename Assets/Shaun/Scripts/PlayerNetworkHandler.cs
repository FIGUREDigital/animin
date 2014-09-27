using UnityEngine;
using System.Collections;

public class PlayerNetworkHandler : Photon.MonoBehaviour {

	private 	CharacterControllerScript 		__playerController;
	private 	AnimationControllerScript 		__animationController;

	private 	Vector3 				__characterPosition;
	private		Quaternion 				__characterRotation;



	// CORE
	// ---------------------------------------------------------------------------------------------------------------------------------------------------

	public void Start () 
	{
		__playerController = GetComponent<CharacterControllerScript>();
		__animationController = GetComponent<AnimationControllerScript>();
		UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnAniminEnd(this.gameObject);
	}

	public void Update () 
    {
		//if (GameController.instance.gameType == GameType.NETWORK) {
			if (PhotonNetwork.connectionStateDetailed == PeerState.Joined) {
				//if (!photonView.isMine) {
				//if (Application.loadedLevelName == "shaun") {
					if (!__playerController.local) 
                    {
						__playerController.UpdatePositionRemotely(__characterPosition);
						__playerController.UpdateRotationRemotely(__characterRotation);
					}
				//}
			}
		//}
	}


	// PUBLIC
	// ---------------------------------------------------------------------------------------------------------------------------------------------------

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
		//Debug.Log("OnPhotonSerializeView\t:\t" + stream.isWriting);

		if (stream.isWriting) {
			// OUR PLAYER, SEND OTHERS OUR DATA
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);

			//if (Application.loadedLevelName == "shaun") {
			if (__animationController) 
				stream.SendNext((int)__animationController.GetAnimationEnum());
			//}
		} else {
			// OTHER (NETWORK) PLAYER, RECEIVE DATA
			__characterPosition = (Vector3)stream.ReceiveNext();
			__characterRotation = (Quaternion)stream.ReceiveNext();
			//if (Application.loadedLevelName == "shaun") {
			if (__animationController)
				__animationController.SetAnimationFromEnum((Animations)stream.ReceiveNext());
			//}
		}
	}


	// PRIVATE
	// ---------------------------------------------------------------------------------------------------------------------------------------------------
}
