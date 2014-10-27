using UnityEngine;
using System.Collections;

public class CheatDefs : MonoBehaviour 
{
	private UILabel mOutputLabel;
	private string mOutputText = "";
	public string OutputText
	{
		set
		{
			mOutputText = value;
		}
	}
	// Use this for initialization
	void Start () 
	{
		if(Debug.isDebugBuild || Application.isEditor)
		{
			gameObject.SetActive(true);
			mOutputLabel = transform.FindChild("Output").GetComponent<UILabel>();
		}
		else
		{
			gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		mOutputLabel.text = mOutputText;	
	}
}