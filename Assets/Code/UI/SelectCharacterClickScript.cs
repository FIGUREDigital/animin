using UnityEngine;
using System.Collections;

public class SelectCharacterClickScript : MonoBehaviour 
{
	public AniminId Animin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick()
	{
		PersistentData.Singleton.SetDefault();
		PersistentData.Singleton.PlayerAniminId = Animin;
		PersistentData.Singleton.AniminEvolutionId = AniminEvolutionStageId.Baby;
		PersistentData.Singleton.Save();

		Application.LoadLevel(@"VuforiaTest");
	}
}
