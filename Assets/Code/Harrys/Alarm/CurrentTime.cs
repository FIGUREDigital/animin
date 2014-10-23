using UnityEngine;

using System;
using System.Collections;

public class CurrentTime : MonoBehaviour {

	UILabel m_CurrentLabel;
	// Use this for initialization
	void Start () {
		m_CurrentLabel = GetComponent<UILabel> ();
	}
	
	// Update is called once per frame
	void Update () {
		m_CurrentLabel.text = DateTime.Now.ToString ("hh:mm");
	}
}
