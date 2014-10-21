using UnityEngine;
using System.Collections;

public class CheatDefs : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		if(Debug.isDebugBuild)
		{
			gameObject.SetActive(true);
		}
		else
		{
			gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
