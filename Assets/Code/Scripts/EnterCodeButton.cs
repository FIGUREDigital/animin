﻿using UnityEngine;
using System.Collections;

public class EnterCodeButton : MonoBehaviour {

	void OnClick()
	{
		ProfilesManagementScript.Singleton.PurchaseChoiceScreen.SetActive(false);
		ProfilesManagementScript.Singleton.CreateUsernameScreen.SetActive(true);
	}
}