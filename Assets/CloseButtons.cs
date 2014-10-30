using UnityEngine;
using System.Collections;

public class CloseButtons : MonoBehaviour {

	private UIButton[] mButtons;
	// Use this for initialization
	void Start () 
	{
		Init ();
	}
	void Init()
	{
		mButtons = GetComponentsInChildren<UIButton>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Close()
	{
		if(mButtons == null)
		{
			Init ();
		}
		foreach(UIButton button in mButtons)
		{
			button.gameObject.SetActive(false);
		}
	}

	public void Open()
	{
		if(mButtons == null)
		{
			Init ();
		}
		foreach(UIButton button in mButtons)
		{
			button.gameObject.SetActive(true);
		}
	}
}
