using UnityEngine;
using System.Collections;

public class PopulateTimer : MonoBehaviour {

	public int num;
	// Use this for initialization
	void Start () {

		UIGrid Grid = GetComponentInChildren<UIGrid> ();
		UILabel[] ChildLabels= Grid.GetComponentsInChildren<UILabel> ();

		GameObject template = ChildLabels [ChildLabels.Length - 1].gameObject;
		


		for (int i = ChildLabels.Length ; i < num ;i++) {


			GameObject go = GameObject.Instantiate(template) as GameObject;
			go.transform.parent = Grid.transform;
			go.transform.localPosition = template.transform.localPosition - new Vector3(0,Grid.cellHeight,0);
			go.transform.localRotation = template.transform.localRotation;
			go.transform.localScale = template.transform.localScale;

			int id = int.Parse(template.name) + 1;
			string idName = id < 10 ? "0"+id.ToString():id.ToString();

			go.name = idName;
			go.GetComponent<UILabel>().text = idName;
			
			//Debug.Log ("Newlabel : ["+go.name+"]; Pos : ["+go.transform.localPosition+"];");

			template = go;
		}
		Grid.Reposition ();
	}
}