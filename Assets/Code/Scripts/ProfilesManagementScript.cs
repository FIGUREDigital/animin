using UnityEngine;
using System.Collections;

public class ProfilesManagementScript : MonoBehaviour 
{
	public static ProfilesManagementScript Singleton;
	public static bool Initialized;
	public GameObject PrefabProfile;
	public GameObject ProfilesRoot;
	public GameObject OLD_ProfilesScreen;
	public GameObject AniminsScreen;
	public GameObject LoadingScreen;
	public GameObject CreateUsernameScreen;
	public GameObject PurchaseChoiceScreen;
	public GameObject CreateAccessCodeScreen;
	public GameObject LoadingSpinner;
	public GameObject ErrorBox;
	public GameObject CloseWebview;
	public GameObject SelectProfile;
	public GameObject NewUser;
	public GameObject LoginUser;
	public GameObject LoginCheckingDialogue;
	public GameObject NoSuchUserCodeDialogue;
	public GameObject AddressInput;
	public bool BeginLoadLevel;
	public GameObject[] AniminSprites;

	public UILabel PiAge;
	public UILabel TBOAge;
	public UILabel KelsiAge;
	public UILabel MandiAge;

	public AniminId AniminToUnlockId;

	void Awake()
	{
		Singleton = this;
		if(!Initialized)
		{
			Initialized = true;
			GameObject go = new GameObject();
			go.name = "ArCameraManager";
			go.AddComponent<ArCameraManager>();
		}
	}

	// Use this for initialization
	void Start ()
	{


		//PlayerProfileData.ActiveProfile = PlayerProfileData.GetDefaultProfile();
		//if(PlayerProfileData.ActiveProfile == null)
		//{
			PlayerProfileData.ActiveProfile = PlayerProfileData.CreateNewProfile("DefaultProfile");
		//}
		
		EvolutionManager.Instance.Deserialize();

		//ServerManager.Register("ServerProfile");
		//AppDataManager.SetUsername("ServerProfile");

		Debug.Log("-----Registered----");
		RefreshProfiles();

		//ServerManager.AddLeaderboardScore(15, 1);
		//ServerManager.GetLeaderboardScores(1);
	}

	public void NewUserProfileAdded()
	{
		ProfilesManagementScript.Singleton.NewUser.SetActive(false);
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);

	}

	public void CheckProfileLoginPasscode(string code)
	{
		StartCoroutine( Account.Instance.WWWCheckLoginCode( code ) );

	}

	public void SuccessfulLogin(bool successful, string code)
	{
		ProfilesManagementScript.Singleton.LoginCheckingDialogue.SetActive(false);

		if (successful) {
						ProfilesManagementScript.Singleton.LoginUser.SetActive (false);
						ProfilesManagementScript.Singleton.AniminsScreen.SetActive (true);
			Account.Instance.UniqueID = code;
				} 
		else 
		{
			ProfilesManagementScript.Singleton.NoSuchUserCodeDialogue.SetActive(true);	
		}

	}



	private void RefreshProfiles()
	{
		PlayerProfileData[] profiles = PlayerProfileData.GetAllProfiles();
		if(profiles != null)
		{
			Debug.Log(profiles.Length.ToString());
			for(int i=0;i<profiles.Length;++i)
			{
				GameObject newProfile = (GameObject)Instantiate(PrefabProfile);
				newProfile.transform.parent = ProfilesRoot.transform;
				
				newProfile.transform.localScale = new Vector3(1,1,1);
				newProfile.transform.GetChild(1).GetComponent<UILabel>().text = profiles[i].ProfileName;
				newProfile.transform.localPosition = new Vector3(i * 180 + 180, 0, 0);
				newProfile.GetComponent<SelectAniminToPlayClickScript>().ProfileRef = profiles[i];
			}
		}
		else
		{
			Debug.Log("No profiles found");
		}
	}

	public void OnAccessCodeResult(string resultId)
	{
		Debug.Log(resultId);
		if(resultId == "Card successfully activated")
		{
			UnlockCharacterManager.Instance.BuyCharacter(AniminToUnlockId, true);
		}
		else if(resultId == "Card number not valid")
		{
			LoadingSpinner.SetActive(false);
			AniminsScreen.SetActive(true);
		}
		else if(resultId == "Card number already in use")
		{
			LoadingSpinner.SetActive(false);
			AniminsScreen.SetActive(true);
		}
		else
		{
			Debug.Log("INVALID CODE RESPONSE");
			LoadingSpinner.SetActive(false);
			AniminsScreen.SetActive(true);
		}
	}

	public void OnAllowCreateProfile(string name)
	{
//		ProfilesManagementScript.Singleton.CreateUsernameScreen.SetActive(false);
//		ProfilesManagementScript.Singleton.ProfilesScreen.SetActive(true);
//
//		PlayerProfileData newprof = CreateNewProfile(name);
//		newprof.Save();
//		RefreshProfiles();


	}

	public void OnRejectedProfile()
	{
		Debug.Log("NO PROFILE FOR YOU");
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
	

}
