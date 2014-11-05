using UnityEngine;
using System.Collections;				// IEnumerator, ArrayList
using System.Collections.Generic;		// Dictionary, List


public class ServerManager : MonoBehaviour {

	public const string 	LOGIN_CALL_COMPLETE				=	"LoginCallComplete";
	public const string 	CREATE_CALL_COMPLETE			=	"CreateCallComplete";
	public const string 	FORGOTTEN_CALL_COMPLETE			=	"ForgottenCallComplete";
	public const string 	ACCESS_CALL_COMPLETE			=	"AccessCallComplete";


	private static 		ServerManager	__instance;			// SINGLETON, STORE REFERENCE

	// LOGIN
	private static		string 			__username;
	//private static		string 			__password;

	// REGISTER
	/*
	private static		string 			__age;
	private static		string 			__gender;
	private static		string 			__answer;
	*/

	// CODE
	private	static		string			__code;


	public void Awake() {
		DebugManager.ShowDebugLog("class", "ServerManager");
		
		__instance = this;

		AppDataManager.SetUp();
	}



	// PUBLIC
	// ---------------------------------------------------------------------------------------------------------------------------------------------------

	/*
	public static void Login(string username, string password) {
		__username = username;
		__password = password;

		__instance.StartCoroutine("Login");
	}
	*/

	public static void Register(string username, string password = "", string age = "", string gender = "", string answer = "") {
		__username = username;
		/*
		__password = password;
		__age = age;
		__gender = gender;
		__answer = answer;
		*/

		__instance.StartCoroutine("Register");
	}

	/*
	public static void Forgotten(string username, string answer) {
		__username = username;
		__answer = answer;

		__instance.StartCoroutine("Forgotten");
	}
	*/

	public static void Access(string code) {
		__code = code;
		
		__instance.StartCoroutine("Access");
	}

	private static int LeaderBoardIdGetScores;
	public static void GetLeaderboardScores(int leaderBoardId) 
	{
		LeaderBoardIdGetScores = leaderBoardId;
		__instance.StartCoroutine("GetScores");
	}

	private static int LeaderBoardIdScoreSubmit;
	private static int ScoreToSubmit;
	public static void AddLeaderboardScore(int score, int leaderBoardId) 
	{ 
		LeaderBoardIdScoreSubmit = leaderBoardId;
		ScoreToSubmit = score;
		__instance.StartCoroutine("AddScore"); 
	}

	
	
	// PRIVATE
	// ---------------------------------------------------------------------------------------------------------------------------------------------------

	// LOGIN
	//

	/*
	private IEnumerator Login() {
		DebugManager.ShowDebugLog("function", "Login");
		
		WWWForm data = new WWWForm();
		data.AddField("key", AppDataManager.serverCallKey);
		data.AddField("username", __username);
		data.AddField("password", __password);

		var request = new WWW("http://leaderboard.eu01.aws.af.cm/api/GET/login/", data);
		
		yield return request;
		
		
		DebugManager.ShowDebugLog("trace", "request.error	", request.error);
		
		bool success = false;
		
		if (request.error != null && request.error != "") {
			DebugManager.ShowDebugLog("trace", "ERROR");
		} else {
			DebugManager.ShowDebugLog("trace", "NO ERROR");
			
			DebugManager.ShowDebugLog("trace", "request.text	", request.text);

			//if (request.text != null && request.text != "") {
			//	DebugManager.ShowDebugLog("trace", "TEXT");
			//} else {
			//	DebugManager.ShowDebugLog("trace", "NO TEXT");
			//}
			
			if (request.text != "") {
				Dictionary<string, object> responseDictionary = Prime31.Json.decode(request.text) as Dictionary<string, object>;
				//DebugManager.ShowDebugLog("trace", "responseDictionary`	", responseDictionary);
				//DebugManager.ShowDebugLog("trace", "responseDictionary	", responseDictionary.Count);
				
				if (responseDictionary != null) {
					if (responseDictionary.ContainsKey("success")) DebugManager.ShowDebugLog("trace", "success\t\t\t", responseDictionary["success"]);
					if (responseDictionary.ContainsKey("username")) DebugManager.ShowDebugLog("trace", "username\t\t", responseDictionary["username"]);
					if (responseDictionary.ContainsKey("age")) DebugManager.ShowDebugLog("trace", "age\t\t\t\t", responseDictionary["age"]);
					if (responseDictionary.ContainsKey("indication")) DebugManager.ShowDebugLog("trace", "indication\t\t", responseDictionary["indication"]);

					if (responseDictionary.ContainsKey("success")) {
						if (responseDictionary["success"].ToString() == "1") {
							success = true;
							
							if (!AppDataManager.previouslyLoggedIn) AppDataManager.SetPreviouslyLoggedIn(true);

							AppDataManager.SetUsername(__username);
							AppDataManager.SetPassword(__password);
						}
					}
				}
			}
		}

		DebugManager.ShowDebugLog("trace", "success	", success);

		MainController.instance.Broadcast(LOGIN_CALL_COMPLETE, success, MenuManager.guiHolder, true);
	}
	*/


	// REGISTER
	//

	private IEnumerator Register() {
		DebugManager.ShowDebugLog("function", "Register");
		
		WWWForm data = new WWWForm();
		data.AddField("key", AppDataManager.serverCallKey);
		data.AddField("username", __username);
		/*
		data.AddField("password", __password);
		data.AddField("age", __age);
		data.AddField("indication", __gender);
		data.AddField("secret", __answer);
		*/
		data.AddField("password", "");
		data.AddField("age", "");
		data.AddField("indication", "");
		data.AddField("secret", "");
		
		var request = new WWW("http://leaderboard.eu01.aws.af.cm/api/POST/register/", data);
		
		yield return request;
		
		
		DebugManager.ShowDebugLog("trace", "request.error	", request.error);
		
		bool success = false;
		string name = string.Empty;
		
		if (request.error != null && request.error != "") {
			DebugManager.ShowDebugLog("trace", "ERROR");
		} else {
			DebugManager.ShowDebugLog("trace", "NO ERROR");
			
			DebugManager.ShowDebugLog("trace", "request.text	", request.text);
			
			/*
			if (request.text != null && request.text != "") {
				DebugManager.ShowDebugLog("trace", "TEXT");
			} else {
				DebugManager.ShowDebugLog("trace", "NO TEXT");
			}
			*/
			
			if (request.text != "") {
				Dictionary<string, object> responseDictionary = Prime31.Json.decode(request.text) as Dictionary<string, object>;
				DebugManager.ShowDebugLog("trace", "responseDictionary`	", responseDictionary);
				DebugManager.ShowDebugLog("trace", "responseDictionary	", responseDictionary.Count);
				
				if (responseDictionary != null) {
					if (responseDictionary.ContainsKey("success")) DebugManager.ShowDebugLog("trace", "success\t\t\t", responseDictionary["success"]);
					if (responseDictionary.ContainsKey("alert")) DebugManager.ShowDebugLog("trace", "alert\t\t\t", responseDictionary["alert"]);
					
					if (responseDictionary.ContainsKey("username")) DebugManager.ShowDebugLog("trace", "username\t\t", responseDictionary["username"]);
					if (responseDictionary.ContainsKey("age")) DebugManager.ShowDebugLog("trace", "age\t\t\t\t", responseDictionary["age"]);
					if (responseDictionary.ContainsKey("indication")) DebugManager.ShowDebugLog("trace", "indication\t\t", responseDictionary["indication"]);
					
					if (responseDictionary.ContainsKey("success")) {
						if (responseDictionary["success"].ToString() == "1") {
							success = true;


							//AppDataManager.SetUsername(__username);
							//AppDataManager.SetPassword(__answer);
						}
					}
				}
			}
		}
		
		DebugManager.ShowDebugLog("trace", "success	", success);


		ProfilesManagementScript profileManagement = GameObject.FindObjectOfType<ProfilesManagementScript>();

		if(success)	profileManagement.OnAllowCreateProfile(__username);
		else profileManagement.OnRejectedProfile();
		

		//MainController.instance.Broadcast(CREATE_CALL_COMPLETE, success, MenuManager.guiHolder, true);
	}


	// FORGOTTEN
	//

	/*
	private IEnumerator Forgotten() {
		DebugManager.ShowDebugLog("function", "Forgotten");
		
		WWWForm data = new WWWForm();
		data.AddField("key", AppDataManager.serverCallKey);
		data.AddField("username", __username);
		data.AddField("secret", __answer);
		
		var request = new WWW("http://leaderboard.eu01.aws.af.cm/api/GET/forgotten/", data);
		
		yield return request;
		
		
		DebugManager.ShowDebugLog("trace", "request.error	", request.error);
		
		if (request.error != null && request.error != "") {
			DebugManager.ShowDebugLog("trace", "ERROR");
		} else {
			DebugManager.ShowDebugLog("trace", "NO ERROR");
			
			DebugManager.ShowDebugLog("trace", "request.text	", request.text);

			//if (request.text != null && request.text != "") {
			//	DebugManager.ShowDebugLog("trace", "TEXT");
			//} else {
			//	DebugManager.ShowDebugLog("trace", "NO TEXT");
			//}

			__password = "";

			if (request.text != "") {
				Dictionary<string, object> responseDictionary = Prime31.Json.decode(request.text) as Dictionary<string, object>;
				//DebugManager.ShowDebugLog("trace", "responseDictionary`	", responseDictionary);
				DebugManager.ShowDebugLog("trace", "responseDictionary	", responseDictionary.Count);
				
				if (responseDictionary != null) {
					if (responseDictionary.ContainsKey("success")) DebugManager.ShowDebugLog("trace", "success\t\t\t", responseDictionary["success"]);
					if (responseDictionary.ContainsKey("username")) DebugManager.ShowDebugLog("trace", "username\t\t", responseDictionary["username"]);
					if (responseDictionary.ContainsKey("password")) DebugManager.ShowDebugLog("trace", "password\t\t", responseDictionary["password"]);
					if (responseDictionary.ContainsKey("age")) DebugManager.ShowDebugLog("trace", "age\t\t\t\t", responseDictionary["age"]);
					if (responseDictionary.ContainsKey("indication")) DebugManager.ShowDebugLog("trace", "indication\t\t", responseDictionary["indication"]);

					if (responseDictionary.ContainsKey("success")) {
						if (responseDictionary["success"].ToString() == "1") {
							if (responseDictionary.ContainsKey("password")) __password = responseDictionary["password"].ToString();
						}
					}
				}
			}
		}

		MainController.instance.Broadcast(FORGOTTEN_CALL_COMPLETE, __password, MenuManager.guiHolder, true);
	}
	*/


	// CODE
	//
	
	private IEnumerator Access() {
		DebugManager.ShowDebugLog("function", "Access		", __code);

		WWWForm data = new WWWForm();
		//data.AddField("key", AppDataManager.serverCallKey);
		////data.AddField("username", __username);
		//data.AddField("code", __code);

        data.AddField( "CardNumber", __code );
        data.AddField( "UserID", Account.Instance.UniqueID );
        data.AddField("Animin", ProfilesManagementScript.Singleton.AniminToUnlockId);

		var request = new WWW("http://terahard.org/Teratest/DatabaseAndScripts/CheckCardLegitimacy.php", data);
        //new WWW("http://leaderboard.eu01.aws.af.cm/api/GET/access/", data);
		
		yield return request;
		
		
		DebugManager.ShowDebugLog("trace", "request.error	", request.error);
		
		bool success = false;
		string returnCode = string.Empty;
		
		if (request.error != null && request.error != "") {
			DebugManager.ShowDebugLog("trace", "ERROR");
		} else {
			DebugManager.ShowDebugLog("trace", "NO ERROR");
			
			DebugManager.ShowDebugLog("trace", "request.text	", request.text);
			
			/*
			if (request.text != null && request.text != "") {
				DebugManager.ShowDebugLog("trace", "TEXT");
			} else {
				DebugManager.ShowDebugLog("trace", "NO TEXT");
			}
			*/
			
			if (request.text != "") {
                
                Debug.Log( request.text );
                if( request.text == "Card successfully activated" )
                {
                    success = true;
                }

				returnCode = request.text;

                /*
				Dictionary<string, object> responseDictionary = Prime31.Json.decode(request.text) as Dictionary<string, object>;
				//DebugManager.ShowDebugLog("trace", "responseDictionary	", responseDictionary);
				DebugManager.ShowDebugLog("trace", "responseDictionary	", responseDictionary.Count);
				
                
				if (responseDictionary != null) 
				{
					if (responseDictionary.ContainsKey("status")) 
						DebugManager.ShowDebugLog("trace", "status\t\t\t", responseDictionary["status"]);
					
					if (responseDictionary.ContainsKey("status")) 
					{
						returnCode = responseDictionary["status"].ToString();
						if (responseDictionary["status"].ToString() == "1") {
							// Code activated
							success = true;
						} else if (responseDictionary["status"].ToString() == "2") {
							// Code already used
						} else if (responseDictionary["status"].ToString() == "3") {
							// Code not valid
						}
					}
				}*/
			}
		}
		
		DebugManager.ShowDebugLog("trace", "success	", success);

		ProfilesManagementScript profileManagement = GameObject.FindObjectOfType<ProfilesManagementScript>();
		profileManagement.OnAccessCodeResult(returnCode);

		
		//MainController.instance.Broadcast(ACCESS_CALL_COMPLETE, success, MenuManager.guiHolder, true);
	}


	// LEADERBOARD
	//

	private IEnumerator GetScores() {
		DebugManager.ShowDebugLog("function", "GetScores");
		
		WWWForm data = new WWWForm();
		data.AddField("key", AppDataManager.serverCallKey);
		data.AddField("username", AppDataManager.username);
		data.AddField("gameid", LeaderBoardIdGetScores);

		var request = new WWW("http://leaderboard.eu01.aws.af.cm/api/GET/scores/", data);
		
		yield return request;
		
		
		DebugManager.ShowDebugLog("trace", "request.error	", request.error);
		
		if (request.error != null && request.error != "") {
			DebugManager.ShowDebugLog("trace", "ERROR");
		} else {
			DebugManager.ShowDebugLog("trace", "NO ERROR");
			
			DebugManager.ShowDebugLog("trace", "request.text	", request.text);
			
			/*
			if (request.text != null && request.text != "") {
				DebugManager.ShowDebugLog("trace", "TEXT");
			} else {
				DebugManager.ShowDebugLog("trace", "NO TEXT");
			}
			*/
			
			if (request.text != "") {
				Dictionary<string, object> responseDictionary = Prime31.Json.decode(request.text) as Dictionary<string, object>;
				DebugManager.ShowDebugLog("trace", "responseDictionary`	", responseDictionary);
				DebugManager.ShowDebugLog("trace", "responseDictionary	", responseDictionary.Count);
				
				if (responseDictionary != null) {
					if (responseDictionary.ContainsKey("top_scores")) DebugManager.ShowDebugLog("trace", "top_scores\t\t\t\t", responseDictionary["top_scores"]);
					if (responseDictionary.ContainsKey("my_top_scores")) DebugManager.ShowDebugLog("trace", "my_top_scores\t\t", responseDictionary["my_top_scores"]);
					
					if (responseDictionary.ContainsKey("top_scores")) AppDataManager.SetTopScores(responseDictionary["top_scores"] as List<object>);
					if (responseDictionary.ContainsKey("my_top_scores")) AppDataManager.SetMyTopScores(responseDictionary["my_top_scores"] as List<object>);
				}
			}
		}
	}

	private IEnumerator AddScore() {
		DebugManager.ShowDebugLog("function", "AddScore");
		
		WWWForm data = new WWWForm();
		data.AddField("key", AppDataManager.serverCallKey);
		data.AddField("username", AppDataManager.username);
		data.AddField("gameid", LeaderBoardIdScoreSubmit);
		data.AddField("score", ScoreToSubmit);

		var request = new WWW("http://leaderboard.eu01.aws.af.cm/api/POST/score/", data);
	
		yield return request;
		
		
		DebugManager.ShowDebugLog("trace", "request.error	", request.error);
		
		if (request.error != null && request.error != "") {
			DebugManager.ShowDebugLog("trace", "ERROR");
		} else {
			DebugManager.ShowDebugLog("trace", "NO ERROR");
			
			DebugManager.ShowDebugLog("trace", "request.text	", request.text);
			
			/*
			if (request.text != null && request.text != "") {
				DebugManager.ShowDebugLog("trace", "TEXT");
			} else {
				DebugManager.ShowDebugLog("trace", "NO TEXT");
			}
			*/
			
			if (request.text != "") {
				Dictionary<string, object> responseDictionary = Prime31.Json.decode(request.text) as Dictionary<string, object>;
				DebugManager.ShowDebugLog("trace", "responseDictionary`	", responseDictionary);
				DebugManager.ShowDebugLog("trace", "responseDictionary	", responseDictionary.Count);
				
				if (responseDictionary != null) {
					if (responseDictionary.ContainsKey("success")) {
						if (responseDictionary["success"].ToString() == "1") {
							DebugManager.ShowDebugLog("trace", "SCORE ADDED");
						} else {
							DebugManager.ShowDebugLog("trace", "SCORE NOT ADDED");
						}
					}
					if (responseDictionary.ContainsKey("alert")) DebugManager.ShowDebugLog("trace", "alert\t", responseDictionary["alert"]);
				}
			}
		}
		
		//GetLeaderboardScores();
	}
}
