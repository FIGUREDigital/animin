using UnityEngine;
using System.Collections;

public class Toggleable : MonoBehaviour {
	
	[SerializeField]
	private string OnSpriteName = "alarm_active_on";
	[SerializeField]
	private string OffSpriteName = "alarm_active_off";

	private bool m_On;
	private UISprite m_Sprite;
	private UIButton m_Button;

	void Start(){
		m_On = false;
		m_Sprite = this.GetComponent<UISprite> ();
		m_Button = this.GetComponent<UIButton> ();
	}

	void OnClick(){

		m_On = !m_On;

		string newSprite = (m_On ? OnSpriteName : OffSpriteName);
		m_Button.normalSprite = newSprite;


		/*
		if (ToggleableOn.activeInHierarchy) {
			ToggleableOn.SetActive (false);
			ToggleableOff.SetActive (true);
		} else  {
			ToggleableOn.SetActive (true);
			ToggleableOff.SetActive (false);
		}
		*/
	}
}
