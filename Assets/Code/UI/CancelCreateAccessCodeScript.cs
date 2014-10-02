using UnityEngine;
using System.Collections;

public class CancelCreateAccessCodeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick()
	{
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
		ProfilesManagementScript.Singleton.CreateAccessCodeScreen.SetActive(false);
	}
}
