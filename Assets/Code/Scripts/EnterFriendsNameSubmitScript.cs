using UnityEngine;
using System.Collections;

public class EnterFriendsNameSubmitScript : MonoBehaviour 
{
	UIInput mInput;

	public UILabel ReportingLabel;
	
	/// <summary>
	/// Add some dummy text to the text list.
	/// </summary>
	
	void Start ()
	{
		mInput = GetComponent<UIInput>();
		mInput.label.maxLineCount = 1;
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
			ReportingLabel.gameObject.SetActive(true);
			ReportingLabel.text = "Connecting...";

			GameController.instance.SetFriendUsername(mInput.value);
			GameObject.Find("MultiplayerObject").GetComponent<GameController>().SetMultiplayerJoinFriend();
			this.gameObject.SetActive(false);
	
			mInput.value = "";
			mInput.isSelected = false;
		}
	}
}
