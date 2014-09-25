using UnityEngine;
using System.Collections;

public class SelectCharacterClickScript : MonoBehaviour 
{
	public AniminId Animin;
	private bool BeginLoadLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	
	}

	void OnClick()
	{
		//PersistentData.Singleton.SetDefault();
		//PersistentData.Singleton.PlayerAniminId = Animin;
		//PersistentData.Singleton.AniminEvolutionId = AniminEvolutionStageId.Baby;
		//PersistentData.Singleton.Save();

		PlayerProfileData.ActiveProfile.ActiveAnimin = Animin;
		PersistentData.Singleton = PlayerProfileData.ActiveProfile.Characters[(int)Animin];



		if(!ProfilesManagementScript.Singleton.BeginLoadLevel)	ProfilesManagementScript.Singleton.BeginLoadLevel = true;
		//AsyncOperation asyncOp = Application.LoadLevelAsync("VuforiaTest");
		//yield return asyncOp;
		//Debug.Log("Loading complete");


	}

}
