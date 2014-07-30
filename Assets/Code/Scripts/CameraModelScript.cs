using UnityEngine;
using System.Collections;

public class CameraModelScript : MonoBehaviour 
{
	public GameObject SpriteRef;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		this.transform.position = ray.origin + ray.direction * 0.25f;//new Vector3(ray.origin.x * 4, ray.origin.y * 4, this.transform.localPosition.z);
		//this.transform.localScale = new Vector3(0.1f,0.1f,0.1f);

		if(this.transform.childCount > 0)
		{
			if(Input.GetButtonUp("Fire1"))
			{
				bool hadUItouch = false;				
				Camera nguiCam = UICamera.mainCamera;
				
				if( nguiCam != null )
				{
					Ray inputRay = nguiCam.ScreenPointToRay( Input.mousePosition );    
					RaycastHit hit;
					
					if (Physics.Raycast(inputRay, out hit))
					{
						if(hit.collider.gameObject.layer == LayerMask.NameToLayer( "NGUI" ))
						{
							DestroyChildModelAndHide();
							return;
						}
					}
				}

				RaycastHit hitInfo;
				if (Physics.Raycast(ray, out hitInfo))
				{
					Debug.Log(hitInfo.collider.name);

					if(hitInfo.collider.name.StartsWith("Invisible Ground Plane"))
					{
						Spawn(false);
					}
					else if(hitInfo.collider.gameObject == UIGlobalVariablesScript.Singleton.MainCharacterRef)
					{
						ReferencedObjectScript refScript = SpriteRef.GetComponent<ReferencedObjectScript>();
						UIPopupItemScript popScript = refScript.Reference.GetComponent<UIPopupItemScript>();

						CharacterProgressScript progressScript = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>();


						if(popScript.Type == PopupItemType.Food)
						{
							progressScript.OnInteractWithPopupItem(popScript);
							DestroyChildModelAndHide();
						}
						else if(popScript.Type == PopupItemType.Item)
						{
							if(progressScript.ObjectHolding == null)
							{
								Spawn(true);
							}
							else
							{
								DestroyChildModelAndHide();
							}
						}
						else if(popScript.Type == PopupItemType.Medicine)
						{
							progressScript.OnInteractWithPopupItem(popScript);
							DestroyChildModelAndHide();
						}
					}
					else
					{
						DestroyChildModelAndHide();
					}
				}
				else
				{
					DestroyChildModelAndHide();
				}
			}
		}
	}

	private void DestroyChildModelAndHide()
	{
		for(int i=0;i<this.transform.childCount;++i)
		{
			Destroy(this.transform.GetChild(i).gameObject);
		}

		SpriteRef = null;
	}

	private void Spawn(bool holdInHands)
	{
		if(SpriteRef != null)
		{
			ReferencedObjectScript refScript = SpriteRef.GetComponent<ReferencedObjectScript>();
			UIPopupItemScript popScript = refScript.Reference.GetComponent<UIPopupItemScript>();
			

			GameObject child = (GameObject)GameObject.Instantiate(popScript.Model3D);
			
			
			child.transform.parent = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().ActiveARScene.transform;
			
			child.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			child.transform.localRotation = Quaternion.Euler(0, UnityEngine.Random.Range(130, 230), 0);
			
			child.GetComponent<ReferencedObjectScript>().Reference = refScript.Reference;
			
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().DragedObjectedFromUIToWorld = true;
			
			
			if(popScript.Type == PopupItemType.Food)
				UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.DropFood);
			else if(popScript.Type == PopupItemType.Item)
				UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.DropItem);
			else if(popScript.Type == PopupItemType.Medicine)
				UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.DropMeds);
			
			//child.AddComponent<UIPopupItemScript>();
			//child.GetComponent<UIPopupItemScript>().Points = this.gameObject.GetComponent<UIPopupItemScript>().Points;
			//child.GetComponent<UIPopupItemScript>().Type = this.gameObject.GetComponent<UIPopupItemScript>().Type;
			
			Transform trans = child.transform;
			trans.position = UICamera.lastHit.point;	
			
			//					if (dds.rotatePlacedObject)
			//					{
			//						trans.rotation = Quaternion.LookRotation(UICamera.lastHit.normal) * Quaternion.Euler(90f, 0f, 0f);
			//					}


			CharacterProgressScript progressScript = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>();

			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().GroundItems.Add(child);
			if(holdInHands)
			{
				UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().PickupItem(child);

			}

			//Debug.Log("DCREATED!!!");
			// Destroy this icon as it's no longer needed
			//NGUITools.Destroy(gameObject);
			//return;
		}

		DestroyChildModelAndHide();

	}
}
