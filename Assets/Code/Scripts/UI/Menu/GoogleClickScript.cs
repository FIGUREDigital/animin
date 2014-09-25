using UnityEngine;
using System.Collections;

public class GoogleClickScript : MonoBehaviour {

	void OnClick()
	{
		Application.OpenURL("http://www.google.com");
	}
}
