using UnityEngine;
using System.Collections;

public class CloseButtons : MonoBehaviour {

	private UIButton[] mButtons;
    private OpenInGamePurchaseView view;
    private ShowForm buyForm;

    private bool Exception(UIButton uib){
        if (uib.gameObject == view.gameObject)
            return true;
        if (uib.gameObject.GetComponent<ShowForm>()!=null)
            return true;
        return false;
    }
	// Use this for initialization
	void Start () 
	{
		Init ();
	}
	void Init()
	{
        mButtons = GetComponentsInChildren<UIButton>(true);
        view = GetComponentInChildren<OpenInGamePurchaseView>();
        buyForm = view.gameObject.GetComponentInChildren<ShowForm>();
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
            if (Exception(button))continue;
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
            if (Exception(button))continue;
			button.gameObject.SetActive(true);
		}
		view.gameObject.SetActive(true);
		view.Parent.SetActive(false);
		view.Form.SetActive(false);
		view.ExitWebview.SetActive(false);
	}
}
