using UnityEngine;
using System.Collections;				// IEnumerator, ArrayList
using System.Collections.Generic;		// Dictionary, List


public static class AppDataManager {
	
	//private	static	SaveManager 			__saveManager;
	private static 	string 					__serverCallKey;

	private static 	bool 					__previouslyLoggedIn;
	private static 	string 					__username;

	private static 	List<object> 			__topScores;
	private static 	List<object> 			__myTopScores;



	// GETTERS / SETTERS
	// ---------------------------------------------------------------------------------------------------------------------------------------------------

	public static string serverCallKey { get { return __serverCallKey;} }
	public static bool previouslyLoggedIn { get { return __previouslyLoggedIn;} }
	public static string username { get { return __username;} }
	public static List<object> topScores { get { return __topScores;} }
	public static List<object> myTopScores { get { return __myTopScores;} }
	
	public static void SetPreviouslyLoggedIn(bool previouslyLoggedIn) {
		__previouslyLoggedIn = previouslyLoggedIn;
		UpdateData("previouslyLoggedIn", __previouslyLoggedIn, "bool");
	}
	public static void SetUsername(string username) {
		__username = username;
		UpdateData("username", __username, "string");
	}
	public static void SetTopScores(List<object> topScores) { __topScores = topScores; }
	public static void SetMyTopScores(List<object> myTopScores) { __myTopScores = myTopScores; }



	// PUBLIC
	// ---------------------------------------------------------------------------------------------------------------------------------------------------

	public static void SetUp() {
		DebugManager.ShowDebugLog("class", "ServerManager");
		
		//__saveManager = new SaveManager();
		/*
		__saveManager.DeleteAllData();
		return;
		*/
		
		__serverCallKey = "popped";
		//__username = "shaun";
	}

	public static void CheckLocalData() {
		DebugManager.ShowDebugLog("function", "CheckLocalData");

		//if (PlayerPrefs.HasKey("previouslyLoggedIn")) __previouslyLoggedIn = PlayerPrefsX.GetBool("previouslyLoggedIn");
		//if (PlayerPrefs.HasKey("username")) __username = PlayerPrefs.GetString("username");

		DebugManager.ShowDebugLog("trace", "__previouslyLoggedIn\t", __previouslyLoggedIn);
		DebugManager.ShowDebugLog("trace", "__username\t\t\t\t", __username);
	}

	public static void UpdateData(string name, object value, string type) {
		//DebugManager.ShowDebugLog("function", "UpdateData", name, value);

		//if (name == "previouslyLoggedIn") __saveManager.SaveData(name, value, type);
		//if (name == "username") __saveManager.SaveData(name, value, type);
	}

	
	
	// PRIVATE
	// ---------------------------------------------------------------------------------------------------------------------------------------------------
}
