using UnityEngine;
using System.Collections;

public class NonARSceneReadout : MonoBehaviour {
	
	UILabel m_label;
	// Use this for initialization
	void Start () {
		m_label = this.GetComponent<UILabel> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Debug.isDebugBuild) {
			bool isActive = UIGlobalVariablesScript.Singleton.NonARWorldRef.activeInHierarchy;
			m_label.text = ("NonARScene : [" + (isActive ? "enabled" : "disabled") + "]");
		} else {
			m_label.text = ("");
		}
	}
}
