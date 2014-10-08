using UnityEngine;
using System.Collections;

public class PressEdmBoxKeyScript : MonoBehaviour 
{
	public bool SwitchOn;
	public int KeyIndex;
	public GameObject OtherOnOffIcon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	void OnEnable()
	{
		EDMBoxScript script = this.transform.parent.transform.parent.GetComponent<UIWidget>().bottomAnchor.target.GetComponent<EDMBoxScript>();//GameObject.FindObjectOfType<EDMBoxScript>();
		Debug.Log(script.gameObject.name);
		Debug.Log("STATE: " + script.GetKeyOn(KeyIndex).ToString());

		if(script.GetKeyOn(KeyIndex) == SwitchOn)
		{
			Debug.Log("SHOW");
			this.gameObject.SetActive(true);
		}



		OtherOnOffIcon.SetActive(false);
		this.gameObject.SetActive(false);

		if(script.GetKeyOn(KeyIndex))
		{
			this.gameObject.SetActive(true);
		}
		else
		{
			OtherOnOffIcon.SetActive(true);
		}
	}
	*/

//	void OnDisable()
//	{
//		this.gameObject.SetActive(false);
//	}

	void OnClick()
	{
		EDMBoxScript script = this.transform.parent.transform.parent.GetComponent<UIWidget>().bottomAnchor.target.GetComponent<EDMBoxScript>();//GameObject.FindObjectOfType<EDMBoxScript>();
		script.SetKeyOn(KeyIndex, SwitchOn);

		OtherOnOffIcon.SetActive(true);
		this.gameObject.SetActive(false);
//
//		for(int i=0;i<transform.parent.transform.childCount;++i)
//		{
//			if(transform.parent.transform.GetChild(i) == this.transform)
//			{
//				script.KeysOn[i] = !script.KeysOn[i];
//				if(script.KeysOn[i]) 
//				{
//					this.GetComponent<UITexture>().color = new Color(1,1,1,1);
//					this.GetComponent<UIButton>().defaultColor = new Color(this.GetComponent<UIButton>().defaultColor.r,this.GetComponent<UIButton>().defaultColor.g, this.GetComponent<UIButton>().defaultColor.b,1);
//					this.GetComponent<UIButton>().hover = new Color(this.GetComponent<UIButton>().hover.r,this.GetComponent<UIButton>().hover.g, this.GetComponent<UIButton>().hover.b,1);
//					this.GetComponent<UIButton>().pressed = new Color(this.GetComponent<UIButton>().pressed.r,this.GetComponent<UIButton>().pressed.g, this.GetComponent<UIButton>().pressed.b,1);
//
//				}
//				else 
//				{
//					this.GetComponent<UITexture>().color = new Color(0,0,0,0);
//					this.GetComponent<UIButton>().defaultColor = new Color(this.GetComponent<UIButton>().defaultColor.r,this.GetComponent<UIButton>().defaultColor.g, this.GetComponent<UIButton>().defaultColor.b, 0);
//					this.GetComponent<UIButton>().hover = new Color(this.GetComponent<UIButton>().hover.r,this.GetComponent<UIButton>().hover.g, this.GetComponent<UIButton>().hover.b, 0);
//					this.GetComponent<UIButton>().pressed = new Color(this.GetComponent<UIButton>().pressed.r,this.GetComponent<UIButton>().pressed.g, this.GetComponent<UIButton>().pressed.b, 0);
//
//				}
//
//				break;
//			}
//		}


	}
}
