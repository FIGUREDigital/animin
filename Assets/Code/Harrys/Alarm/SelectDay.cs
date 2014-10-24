using UnityEngine;
using System.Collections;

public class SelectDay : MonoBehaviour {

	private int my_i;
	private bool m_clicked;

	// Use this for initialization
	void Start () {
		UIGrid grid = GetComponentInParent<UIGrid> ();
		if (grid == null)
						Debug.Log ("I didn't find Grid");
		for (int i=0; i <grid.GetComponentsInChildren<UILabel>().Length; i++) {
			Debug.Log("Comparing : ["+this+":"+grid.GetComponentsInChildren<UILabel>()[i].gameObject+"];");
			if (this.gameObject == grid.GetComponentsInChildren<UILabel>()[i].gameObject){
				Debug.Log("Found!");
				my_i = i;
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnClick(){
		Debug.Log ("Clicked: : ["+this.name+"]; i : ["+my_i+"];");


		Color col = (m_clicked?Color.white:Color.red);
		GetComponent<UIButton> ().defaultColor = col;
		m_clicked = !m_clicked;
	}
}
