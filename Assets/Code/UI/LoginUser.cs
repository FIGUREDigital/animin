using UnityEngine;
using System.Collections;

public class LoginUser : MonoBehaviour 
{
	void OnClick()
	{
        ProfilesManagementScript.Singleton.CurrentProfile = ProfilesManagementScript.Singleton.ListOfPlayerProfiles[0];
        PlayerProfileData.ActiveProfile = ProfilesManagementScript.Singleton.CurrentProfile;
        UnlockCharacterManager.Instance.CheckInitialCharacterUnlock();

        ProfilesManagementScript.Singleton.SelectProfile.SetActive(false);
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
	}
}
