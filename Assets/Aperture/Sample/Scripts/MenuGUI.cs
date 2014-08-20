using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {

	// Use this for initialization
	void OnGUI() {
		if (GUI.Button(new Rect(10, 10, 100, 50), "Start")) {
			Application.LoadLevel("Demo");
		}
	}
}
