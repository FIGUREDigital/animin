using UnityEngine;
using System.Collections;

public class TestPauser : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.isEditor && Input.GetKeyDown (KeyCode.Space)) {
			UnityEditor.EditorApplication.isPaused = true;
		}
	}
}