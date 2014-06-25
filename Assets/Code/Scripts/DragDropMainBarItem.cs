﻿using UnityEngine;
using System.Collections;

public class DragDropMainBarItem : UIDragDropItem
{

	/// <summary>
	/// Prefab object that will be instantiated on the DragDropSurface if it receives the OnDrop event.
	/// </summary>
	
	//public GameObject prefab;
	
	/// <summary>
	/// Drop a 3D game object onto the surface.
	/// </summary>

	protected override void OnDragDropStart ()
	{
		//this.gameObject.GetComponent<BoxCollider>().enabled = false;

		base.OnDragDropStart ();
	}
	
	protected override void OnDragDropRelease (GameObject surface)
	{
		Debug.Log("DETECTED DRAG DROP RELEASE");
		if(surface != null) Debug.Log(surface.name);
		else Debug.Log("the surface is null");

		//this.gameObject.GetComponent<BoxCollider>().enabled = true;

		if (surface != null)
		{
			ExampleDragDropSurface dds = surface.GetComponent<ExampleDragDropSurface>();
			
			if (dds != null)
			{
				ReferencedObjectScript refScript = this.GetComponent<ReferencedObjectScript>();
				UIPopupItemScript popScript = refScript.Reference.GetComponent<UIPopupItemScript>();

				// its a character drag and drop
				if(dds.GetComponent<CharacterProgressScript>() != null)
				{
					//UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().DragedObjectedFromUIToWorld = true;
					//dds.GetComponent<CharacterProgressScript>().OnInteractWithPopupItem(popScript);
				}
				else
				{
					GameObject child = (GameObject)GameObject.Instantiate(popScript.Model3D);
					child.transform.parent = UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.parent;
					child.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
					child.transform.localRotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);

					child.GetComponent<ReferencedObjectScript>().Reference = refScript.Reference;

					UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().DragedObjectedFromUIToWorld = true;

					//child.AddComponent<UIPopupItemScript>();
					//child.GetComponent<UIPopupItemScript>().Points = this.gameObject.GetComponent<UIPopupItemScript>().Points;
					//child.GetComponent<UIPopupItemScript>().Type = this.gameObject.GetComponent<UIPopupItemScript>().Type;
					
					Transform trans = child.transform;
					trans.position = UICamera.lastHit.point;	
					
//					if (dds.rotatePlacedObject)
//					{
//						trans.rotation = Quaternion.LookRotation(UICamera.lastHit.normal) * Quaternion.Euler(90f, 0f, 0f);
//					}

					UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().GroundItems.Add(child);
					//Debug.Log("DCREATED!!!");
					// Destroy this icon as it's no longer needed
					//NGUITools.Destroy(gameObject);
					//return;
				}
			}
		}

		base.OnDragDropRelease(surface);
	}
}
