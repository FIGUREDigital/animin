using UnityEngine;
using System.Collections;

public class SendAccesCodeFromChatboxClickScript : MonoBehaviour {

	UIInput mInput;
	public GameObject ReportingLabel;
	
	/// <summary>
	/// Add some dummy text to the text list.
	/// </summary>
	
	void Start ()
	{
		mInput = GetComponent<UIInput>();
		mInput.label.maxLineCount = 1;
		ReportingLabel.SetActive(false);
	}
	
	/// <summary>
	/// Submit notification is sent by UIInput when 'enter' is pressed or iOS/Android keyboard finalizes input.
	/// </summary>
	public void OnSubmit ()
	{
		// It's a good idea to strip out all symbols as we don't want user input to alter colors, add new lines, etc
		string text = NGUIText.StripSymbols(mInput.value);
		
		if (!string.IsNullOrEmpty(text))
		{
			ServerManager.Access(text);

			//ServerManager.Register(text);
			mInput.enabled = false;
			ReportingLabel.SetActive(true);
			//AppDataManager.SetUsername("serverProfile");
			
			
			//GameObject.Find("MultiplayerObject").GetComponent<GameController>().SetMultiplayerJoinFriend();
			
			//ServerManager.Register("serverProfile");
			
			
			//mInput.value = "";
			
		}
	}
}