using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour 
{
	private bool BeginFadeOut;
	private float TimerForArc = -0.6f;
	public GameObject SplatPrefab;

	// Use this for initialization
	void Start () 
	{
		Physics.IgnoreLayerCollision(
			LayerMask.NameToLayer("Character"),  
			LayerMask.NameToLayer("Projectiles"));

		Physics.IgnoreLayerCollision(
			LayerMask.NameToLayer("Projectiles"),  
			LayerMask.NameToLayer("Projectiles"));


		Physics.IgnoreLayerCollision(
			LayerMask.NameToLayer("Floor"),  
			LayerMask.NameToLayer("Projectiles"));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(BeginFadeOut)
		{
			bool destroy = false;
			if(renderer != null) 
			{
				float alpha = renderer.material.color.a;
				alpha -= Time.deltaTime *  7;
				if(alpha <= 0) 
				{
					alpha = 0;
					destroy = true;
				}

				renderer.material.color = new Color(
					renderer.material.color.r,
					renderer.material.color.g,
					renderer.material.color.b,
					alpha);
			}
			
			for(int a=0;a<transform.childCount;++a)
			{
				if(transform.GetChild(a).renderer == null) continue;
				

				float alpha = transform.GetChild(a).renderer.material.color.a;
				alpha -= Time.deltaTime * 7;
				if(alpha <= 0) 
				{
					alpha = 0;
					destroy = true;
				}
				transform.GetChild(a).renderer.material.color = new Color(
					transform.GetChild(a).renderer.material.color.r,
					transform.GetChild(a).renderer.material.color.g,
					transform.GetChild(a).renderer.material.color.b,
					alpha);
			}
			
			if(destroy)
			{
				UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnedObjects.Remove(this.gameObject);
				Destroy(this.gameObject);
			}
		}
		else
		{
			TimerForArc += Time.deltaTime * 2.8f;
				this.transform.localPosition = new Vector3(
					this.transform.localPosition.x,
				this.transform.localPosition.y - Time.deltaTime * TimerForArc,
					this.transform.localPosition.z);

				if(this.transform.localPosition.y <= -0.2f)
				{
					if(SplatPrefab != null)
					{
						GameObject instance = (GameObject)Instantiate(SplatPrefab);
							instance.transform.parent = this.transform.parent;
							instance.transform.position = this.transform.position;
						instance.transform.rotation = Quaternion.Euler(instance.transform.rotation.eulerAngles.x, instance.transform.rotation.eulerAngles.y, Random.Range(0, 360));
							
						UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnedObjects.Add(instance);
					}

					UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnedObjects.Remove(this.gameObject);
					Destroy(this.gameObject);
				}

		}
	}

	void OnCollisionEnter(Collision collision)
	{
//		Debug.Log("COLLISION DETECTED: " + collision.gameObject.tag);
		BeginFadeOut = true;

		if(renderer != null) 
		{
			renderer.material.shader = Shader.Find("Custom/ItemShader");
		}
		
		for(int a=0;a<transform.childCount;++a)
		{
			if(transform.GetChild(a).renderer == null) continue;
			
			transform.GetChild(a).renderer.material.shader = Shader.Find("Custom/ItemShader");
		}
	}
}
