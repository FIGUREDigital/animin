using UnityEngine;
using System.Collections;

public class DetectDragIconScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnDragStart ()
	{
		UIGlobalVariablesScript.Singleton.DragableUI3DObject.GetComponent<CameraModelScript>().SpriteRef = this.gameObject;





		ReferencedObjectScript refScript = this.GetComponent<ReferencedObjectScript>();
		UIPopupItemScript popScript = refScript.Reference.GetComponent<UIPopupItemScript>();
		
		GameObject child = (GameObject)GameObject.Instantiate(popScript.Model3D);

		child.GetComponent<BoxCollider>().enabled = false;

		child.transform.parent = UIGlobalVariablesScript.Singleton.DragableUI3DObject.transform;
		child.transform.position = Vector3.zero;
		child.transform.rotation = Quaternion.identity;
		child.transform.localScale = Vector3.one;
		child.transform.localRotation = Quaternion.identity;
		child.transform.localPosition = Vector3.zero;

	}
}
