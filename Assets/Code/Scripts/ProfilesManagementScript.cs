using UnityEngine;
using System.Collections;

public class ProfilesManagementScript : MonoBehaviour 
{
	public static ProfilesManagementScript Singleton;
	public GameObject PrefabProfile;
	public GameObject ProfilesRoot;
	public GameObject ProfilesScreen;
	public GameObject AniminsScreen;
	public GameObject LoadingScreen;
	public bool BeginLoadLevel;

	public UILabel PiAge;
	public UILabel TBOAge;
	public UILabel KelsiAge;
	public UILabel MandiAge;

	void Awake()
	{
		Singleton = this;
	}

	// Use this for initialization
	void Start () 
	{
		PlayerProfileData newprof = CreateNewProfile("TEST");
		newprof.Save();

	 	PlayerProfileData[] profiles = PlayerProfileData.GetAllProfiles();
		if(profiles != null)
		{
			Debug.Log(profiles.Length.ToString());
			for(int i=0;i<profiles.Length;++i)
			{
				GameObject newProfile = (GameObject)Instantiate(PrefabProfile);
				newProfile.transform.parent = ProfilesRoot.transform;

				newProfile.transform.localScale = new Vector3(1,1,1);
				newProfile.transform.GetChild(3).GetComponent<UILabel>().text = profiles[i].ProfileName;
				newProfile.transform.localPosition = new Vector3(0, i * -300, 0);
				newProfile.transform.GetChild(1).GetComponent<SelectAniminToPlayClickScript>().ProfileRef = profiles[i];
			}
		}
		else
		{
			Debug.Log("No profiles found");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(BeginLoadLevel)
		{
			BeginLoadLevel = false;
			StartCoroutine(LoadLevel(@"VuforiaTest"));
			ProfilesManagementScript.Singleton.AniminsScreen.SetActive(false);
			ProfilesManagementScript.Singleton.LoadingScreen.SetActive(true);
		}
	}

	
	public IEnumerator LoadLevel(string name)
	{
		yield return new WaitForSeconds(0.1f);
		
		//nextLevel is one of the class fields
		AsyncOperation	nextLevel = Application.LoadLevelAsync(name);
		while (!nextLevel.isDone)
		{ 
			yield return new WaitForEndOfFrame(); 
		}       
	}
	
	public static PlayerProfileData CreateNewProfile(string name)
	{
		PlayerProfileData profile = new PlayerProfileData();
		profile.ProfileName = name;
		for(int i=0;i<profile.Characters.Length;++i)
		{
			profile.Characters[i] = new PersistentData();
			profile.Characters[i].SetDefault();
			profile.Characters[i].AniminEvolutionId = AniminEvolutionStageId.Baby;
			profile.Characters[i].PlayerAniminId = (AniminId)i;
		}

		return profile;
	}
}
