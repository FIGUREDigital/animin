using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using Prime31;

public class FlurryLogger{
	
	bool m_Inited = false;
	public void Init(){
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
		FlurryBinding.endTimedEvent ("SessionTime");
	}


	public void StartMainScreenTimer(){
		FlurryBinding.logEvent ("MainMenuStart",true);
	}
	public void EndMainScreenTimer(){
		FlurryBinding.endTimedEvent ("MainMenuStart");
	}

	public enum Minigame{Cuberunners, Gungame};
	public void StartMinigame(Minigame game){

		Dictionary<string,string> dict = new Dictionary<string,string> ();
		dict.Add ("Minigame", game.ToString());

		FlurryBinding.logEventWithParameters ("TimeInMinigame", dict, true);
	}
	public void EndMinigame(){

		Dictionary<string,string> dict = new Dictionary<string,string> ();
		dict.Add ("DateTime", DateTime.Now.ToString ("dd:MM:yyyy hh:mm"));

		FlurryBinding.logEventWithParameters ("TimeInMinigame", dict, true);
	}
	
	public void StartARCard(){
		FlurryBinding.logEvent ("ARCardTime",false);
	}
	public void EndARCard(){
		FlurryBinding.endTimedEvent ("ARCardTime");
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