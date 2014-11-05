using UnityEngine;
using System.Collections;

public class LoginUser : MonoBehaviour 
{
	void OnClick()
	{
        ProfilesManagementScript.Singleton.CurrentProfile = ProfilesManagementScript.Singleton.ListOfPlayerProfiles[0];
        AchievementManager.Instance.PopulateAchievements(false);
        UnlockCharacterManager.Instance.CheckInitialCharacterUnlock();

        ProfilesManagementScript.Singleton.SelectProfile.SetActive(false);
		ProfilesManagementScript.Singleton.AniminsScreen.SetActive(true);
	}
}
