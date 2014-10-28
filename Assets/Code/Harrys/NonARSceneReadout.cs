using UnityEngine;
using System.Collections;

public class NonARSceneReadout : MonoBehaviour {
	
	UILabel m_label;
	// Use this for initialization
	void Start () {
		if (!Debug.isDebugBuild) {
			gameObject.SetActive(false);
		} else {
			m_label = this.GetComponent<UILabel> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.activeInHierarchy) {
			bool isActive = UIGlobalVariablesScript.Singleton.NonARWorldRef.activeInHierarchy;
			m_label.text = ("NonARScene : [" + (isActive ? "enabled" : "disabled") + "]");
		}
	}
}
