using UnityEngine;
using System.Collections;

public class MonsterSplatExplosionScript : MonoBehaviour 
{
	float myGravity = 400;
	public GameObject SplatPrefab;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(this.transform.localPosition.y <= -0.2f)
		{
			if(SplatPrefab != null)
			{

				GameObject instance = (GameObject)Instantiate(SplatPrefab);
				instance.transform.parent = this.transform.parent;
				instance.transform.position = this.transform.position;
				instance.transform.rotation = Quaternion.Euler(
					instance.transform.rotation.eulerAngles.x, 
					instance.transform.rotation.eulerAngles.y,
					Random.Range(0, 360));
				
				UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnedObjects.Add(instance);

			}


			UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnedObjects.Remove(this.gameObject);
			Destroy(this.gameObject);
		}
	}

	void FixedUpdate()
	{
		var curVel = rigidbody.velocity;
		curVel.y -= myGravity * Time.deltaTime; // apply fake gravity
		rigidbody.velocity = curVel;
	}
}
