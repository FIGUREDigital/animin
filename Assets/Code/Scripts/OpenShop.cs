using UnityEngine;
using System.Collections;

public class OpenShop : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		UnlockCharacterManager.Instance.OpenShop();
	}

	void OnEnable()
	{
		UnlockCharacterManager.Instance.OpenShop();
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
