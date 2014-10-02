using UnityEngine;
using System.Collections;

public class SendAccessCodeToServerButtonClickScript : MonoBehaviour {

	public SendAccesCodeFromChatboxClickScript SubmitFunction;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick()
	{
		SubmitFunction.OnSubmit();
	}
}
