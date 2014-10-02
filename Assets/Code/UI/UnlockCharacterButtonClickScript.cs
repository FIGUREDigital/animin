using UnityEngine;
using System.Collections;

public class UnlockCharacterButtonClickScript : MonoBehaviour 
{
	public AniminId Id;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick()
	{
		ProfilesManagementScript.Singleton.AniminToUnlockId = Id;

		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(false);
		ProfilesManagementScript.Singleton.CreateAccessCodeScreen.SetActive(true);
	}
}
