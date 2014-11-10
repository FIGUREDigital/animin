﻿using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using Prime31;

public class FlurryLogger{
	
	bool m_Inited = false;
    private string m_SessionTime = "SessionTime";
    private string m_MainMenuStart = "MainMenuStart";
    private string m_TimeInMinigame = "TimeInMinigame";
	public void Init(){
		if (m_Inited) return;
		Debug.Log ("FLURRY LOGGER : [Init];");

#if UNITY_IOS
		FlurryAnalytics.startSession ("VWK79FC9CHBVXY4VXXY6");
		FlurryAnalytics.setSessionReportsOnCloseEnabled (true);
		FlurryAnalytics.setSessionReportsOnPauseEnabled (true);
#endif
		
		var dict = new Dictionary<string,string> ();
		dict.Add ("DeviceModel", SystemInfo.deviceModel);

#if UNITY_IOS
		FlurryAnalytics.logEventWithParameters ("DeviceStats", dict, false);
		FlurryAnalytics.logEvent ("SessionTime", true);
#endif
		
		m_Inited = true;
	}
	public void EndSession(){
		Debug.Log ("FLURRY LOGGER : [End Session];");
#if UNITY_IOS
		FlurryAnalytics.endTimedEvent ("SessionTime");
#endif
	}



	private bool TimingMainScreen;

	public void StartMainScreenTimer(){
		Debug.Log ("FLURRY LOGGER : [StartMainScreenTimer];");
#if UNITY_IOS
		FlurryAnalytics.logEvent ("MainMenuStart",true);
#endif
		TimingMainScreen = true;
	}
	public void EndMainScreenTimer(){
		Debug.Log ("FLURRY LOGGER : [EndMainScreenTimer];");
		if (!TimingMainScreen) return;
#if UNITY_IOS
		FlurryAnalytics.endTimedEvent ("MainMenuStart");
#endif
		TimingMainScreen = false;
	}



	public enum Minigame{Cuberunners, Gungame};
	private bool TimingMinigame;

	public void StartMinigame(Minigame game){
		
		Debug.Log ("FLURRY LOGGER : [StartMinigame];");

		Dictionary<string,string> dict = new Dictionary<string,string> ();
		dict.Add ("Minigame", game.ToString());
#if UNITY_IOS
		FlurryAnalytics.logEventWithParameters ("TimeInMinigame", dict, true);
#endif
		TimingMinigame = true;
	}
	public void EndMinigame(){
		if (!TimingMinigame) return;
		Debug.Log ("FLURRY LOGGER : [EndMinigame];");

		Dictionary<string,string> dict = new Dictionary<string,string> ();
		dict.Add ("DateTime", DateTime.Now.ToString ("dd:MM:yyyy hh:mm"));
#if UNITY_IOS
		FlurryAnalytics.logEventWithParameters ("TimeInMinigame", dict, true);
#endif

		TimingMinigame = false;
	}



	private bool TimingARCard;

	public void StartARCard(){
		Debug.Log ("FLURRY LOGGER : [StartARCard];");
#if UNITY_IOS
		FlurryAnalytics.logEvent ("ARCardTime",false);
#endif
		TimingARCard = true;
	}
	public void EndARCard(){
		Debug.Log ("FLURRY LOGGER : [EndARCard];");
		if (!TimingARCard) return;
#if UNITY_IOS
		FlurryAnalytics.endTimedEvent ("ARCardTime");
#endif
		TimingARCard = false;
	}

	public void CharacterPurchasedIAP()
	{
		for(int i = 1; i < ProfilesManagementScript.Singleton.CurrentProfile.Characters.Length; i++)
		{
			if(ProfilesManagementScript.Singleton.CurrentProfile.Characters[i].CreatedOn != null)
			{
				return;
			}

		}
		TimeSpan useLength = DateTime.Now.Subtract(ProfilesManagementScript.Singleton.CurrentProfile.Characters[0].CreatedOn);
		Debug.Log ("FLURRY LOGGER : [ConversionTime];");

		Dictionary<string,string> dict = new Dictionary<string,string> ();
		dict.Add ("TimeToBuy", useLength.ToString ());
		dict.Add ("ItemBought", UnlockCharacterManager.Instance.ID.ToString());
		dict.Add ("ItemBought", "In App Purchase");
#if UNITY_IOS
		FlurryAnalytics.logEventWithParameters ("ConversionTime", dict, false);
#endif
	}

	public void CharacterPurchasedWeb()
	{
		
		for(int i = 1; i < ProfilesManagementScript.Singleton.CurrentProfile.Characters.Length; i++)
		{
			if(ProfilesManagementScript.Singleton.CurrentProfile.Characters[i].CreatedOn != null)
			{
				return;
			}
			
		}
		TimeSpan useLength = DateTime.Now.Subtract(ProfilesManagementScript.Singleton.CurrentProfile.Characters[0].CreatedOn);
		Debug.Log ("FLURRY LOGGER : [ConversionTime];");
		
		Dictionary<string,string> dict = new Dictionary<string,string> ();
		dict.Add ("TimeToBuy", useLength.ToString ());
		dict.Add ("ItemBought", UnlockCharacterManager.Instance.ID.ToString());
		dict.Add ("ItemBought", "Webview");
#if UNITY_IOS
		FlurryAnalytics.logEventWithParameters ("ConversionTime", dict, false);
#endif
	}


	private static FlurryLogger m_Instance;
	public static FlurryLogger Instance{
		get{
			if (m_Instance == null){
				m_Instance = new FlurryLogger();
			}
			return m_Instance;
		}
	}
}