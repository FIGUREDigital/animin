using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour {

	public PortalId Id;
	private float Timer;

	// Use this for initialization
	void Start () {
	
	}

	public void Show(bool isArMode)
	{
		this.gameObject.SetActive(true);

		if(isArMode)
		{
			this.transform.parent = UIGlobalVariablesScript.Singleton.ARSceneRef.transform;
		}
		else
		{
			this.transform.parent = UIGlobalVariablesScript.Singleton.NonSceneRef.transform;
		}

		this.transform.localPosition = Vector3.zero;
		//this.transform.position = Vector3.zero;


		Timer = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Timer += Time.deltaTime;

		if(Timer >= 3)
		{
			this.gameObject.SetActive(false);
		}

		/*if (Input.GetButtonDown("Fire1")) 
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
							UIGlobalVariablesScript.Singleton.ARSceneRef.SetActive(false);
							UIGlobalVariablesScript.Singleton.GardenSceneRef.SetActive(true);
							break;
						}

						case PortalId.ExitGarden:
						{
						UIGlobalVariablesScript.Singleton.ARSceneRef.SetActive(true);
							UIGlobalVariablesScript.Singleton.GardenSceneRef.SetActive(false);
							break;
						}
					}
				}
			}
		}*/
	}
}

public enum PortalId
{
	Garden = 0,
	ExitGarden,
}
