using UnityEngine;
using System.Collections;

public class IdLabel : MonoBehaviour {

	UILabel label;
	// Use this for initialization
	void Start () 
	{
		FindLabel();
	}

	void FindLabel()
	{
		label = gameObject.GetComponent<UILabel>();
	}
	void OnEnable()
	{
		if(label == null)
		{
			FindLabel();
		}
		label.text = "Profile ID: " + Account.Instance.UniqueID;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
