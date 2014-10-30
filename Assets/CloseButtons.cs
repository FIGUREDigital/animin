using UnityEngine;
using System.Collections;

public class CloseButtons : MonoBehaviour {

	private UIButton[] mButtons;
	private OpenInGamePurchaseView view;
	// Use this for initialization
	void Start () 
	{
		Init ();
	}
	void Init()
	{
		mButtons = GetComponentsInChildren<UIButton>();
		view = GetComponentInChildren<OpenInGamePurchaseView>();
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
		view.gameObject.SetActive(true);
		view.Parent.SetActive(false);
		view.Form.SetActive(false);
		view.ExitWebview.SetActive(false);
	}
}
