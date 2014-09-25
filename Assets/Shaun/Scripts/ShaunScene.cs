using UnityEngine;
//using System.Collections;


public class ShaunScene : MonoBehaviour {

	public static 	ShaunScene			instance;					// SINGLETON, STORE REFERENCE

	private  		GameObject			__joystick;
	private  		GameObject			__joystickBase;
	private  		GameObject 			__playerModel;

	// CORE

	public void Awake() {
		instance = this;

		__joystick = GameObject.Find("Joystick");
		__joystickBase = GameObject.Find("Joystick Base");

		__joystick.SetActive(false);
		__joystickBase.SetActive(false);
	}

	public void OnEnable() {}

	public void Start () {}

	public void Update () {}

	public void OnGUI() {}


	// PUBLIC

	public void LoadModel() {
		Debug.Log ("LoadModel\t:\t"	+	GameController.instance.gameType);

		string prefabPath = "Prefabs/tbo_baby_multi";

		if (GameController.instance.gameType == GameType.SOLO) {
			GameObject prefab = (GameObject)Resources.Load(prefabPath);
			__playerModel = (GameObject)Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
		}  else {
			__playerModel = PhotonNetwork.Instantiate(prefabPath, Vector3.zero, Quaternion.identity, 0);
		}

		/*
		Transform playerTransform = GameObject.Find("Player").transform;
		Transform rotationTransform = playerTransform.Find("Rotation").transform;
		__playerModel.transform.parent = rotationTransform;
		*/
		
		__playerModel.GetComponent<PlayerController>().SetLocalPlayer(true);

		///

		__joystick.SetActive(true);
		__joystickBase.SetActive(true);
	}

	public void DestroyModel() {
		//Debug.Log ("DestroyModel\t:\t"	+	GameController.instance.gameType);

		Destroy(__playerModel);

		__joystick.SetActive(false);
		__joystickBase.SetActive(false);
	}


	// PRIVATE
}