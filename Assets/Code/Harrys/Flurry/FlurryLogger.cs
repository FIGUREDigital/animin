using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using Prime31;

public class FlurryLogger{
	
	bool m_Inited = false;
	public void Init(){
		if (m_Inited) return;
		Debug.Log ("FLURRY LOGGER : [Init];");
		FlurryBinding.startSession ("VWK79FC9CHBVXY4VXXY6");
		FlurryBinding.setSessionReportsOnCloseEnabled (true);
		FlurryBinding.setSessionReportsOnPauseEnabled (true);
		
		var dict = new Dictionary<string,string> ();
		dict.Add ("DeviceModel", SystemInfo.deviceModel);
		
		FlurryBinding.logEventWithParameters ("DeviceStats", dict, false);
		FlurryBinding.logEvent ("SessionTime", true);
		
		m_Inited = true;
	}
	public void EndSession(){
		Debug.Log ("FLURRY LOGGER : [End Session];");
		FlurryBinding.endTimedEvent ("SessionTime");
	}



	private bool TimingMainScreen;

	public void StartMainScreenTimer(){
		Debug.Log ("FLURRY LOGGER : [StartMainScreenTimer];");
		FlurryBinding.logEvent ("MainMenuStart",true);
		TimingMainScreen = true;
	}
	public void EndMainScreenTimer(){
		Debug.Log ("FLURRY LOGGER : [EndMainScreenTimer];");
		if (!TimingMainScreen) return;
		FlurryBinding.endTimedEvent ("MainMenuStart");
		TimingMainScreen = false;
	}



	public enum Minigame{Cuberunners, Gungame};
	private bool TimingMinigame;

	public void StartMinigame(Minigame game){
		
		Debug.Log ("FLURRY LOGGER : [StartMinigame];");

		Dictionary<string,string> dict = new Dictionary<string,string> ();
		dict.Add ("Minigame", game.ToString());

		FlurryBinding.logEventWithParameters ("TimeInMinigame", dict, true);
		TimingMinigame = true;
	}
	public void EndMinigame(){
		if (!TimingMinigame) return;
		Debug.Log ("FLURRY LOGGER : [EndMinigame];");

		Dictionary<string,string> dict = new Dictionary<string,string> ();
		dict.Add ("DateTime", DateTime.Now.ToString ("dd:MM:yyyy hh:mm"));

		FlurryBinding.logEventWithParameters ("TimeInMinigame", dict, true);

		TimingMinigame = false;
	}



	private bool TimingARCard;

	public void StartARCard(){
		Debug.Log ("FLURRY LOGGER : [StartARCard];");
		FlurryBinding.logEvent ("ARCardTime",false);
		TimingARCard = true;
	}
	public void EndARCard(){
		Debug.Log ("FLURRY LOGGER : [EndARCard];");
		if (!TimingARCard) return;
		FlurryBinding.endTimedEvent ("ARCardTime");
		TimingARCard = false;
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