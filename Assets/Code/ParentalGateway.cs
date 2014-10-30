﻿using UnityEngine;
using System.Collections;

public class ParentalGateway : MonoBehaviour 
{
	[SerializeField]
	private GatewayButton correctButton;
	private GameObject prevScreen;
	private GameObject nextScreen;

	public void Open(GameObject prev, GameObject next)
	{
		prevScreen = prev;
		nextScreen = next;
		prevScreen.SetActive(false);
		this.gameObject.SetActive(true);
	}

	public void Pass()
	{
		Destroy(this.gameObject, 0);
//		nextScreen.SetActive(true);
		UIGlobalVariablesScript.Singleton.LaunchWebview ();
	}

	public void Fail()
	{
		Destroy(this.gameObject, 0);
		prevScreen.SetActive(true);
	}
}
