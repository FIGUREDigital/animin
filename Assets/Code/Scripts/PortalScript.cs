using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour {

	public PortalId Id;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown("Fire1")) 
		{
			RaycastHit hitInfo;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hitInfo))
			{
				if(hitInfo.collider == this.collider)
				{
					switch(Id)
					{
						case PortalId.Garden:
						{
							UIGlobalVariablesScript.Singleton.DefaultARSceneRef.SetActive(false);
							UIGlobalVariablesScript.Singleton.GardenSceneRef.SetActive(true);
							break;
						}

						case PortalId.ExitGarden:
						{
							UIGlobalVariablesScript.Singleton.DefaultARSceneRef.SetActive(true);
							UIGlobalVariablesScript.Singleton.GardenSceneRef.SetActive(false);
							break;
						}
					}
				}
			}
		}
	}
}

public enum PortalId
{
	Garden = 0,
	ExitGarden,
}
