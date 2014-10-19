﻿using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour 
{
	private bool BeginFadeOut;
	private float TimerForArc = -0.6f;
	public GameObject SplatPrefab;

    // SHAUN START
    // ---------------------------------------------------------------------------------------------------------------------------------------------------

    private bool __local;

    //public bool local { get { return __local;} }
    public void SetLocal(bool local) { __local = local; }

    // SHAUN END
    // ---------------------------------------------------------------------------------------------------------------------------------------------------
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{

		}
		else
		{
		
		}
	}

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

        GunsMinigameScript miniGame = UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>();

        if (GameController.instance.gameType == GameType.NETWORK)
        {
          
            int playerIndex = int.Parse(GetComponent<PhotonView>().instantiationData[0].ToString());

            UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().ShootBulletForwardEnd(this.gameObject, miniGame.PlayersCharacters[playerIndex]);
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
        // BULLET HAS BOUNCED ON ENEMY, BEGIN FADE OUT AND DESTROY
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
				//UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnedObjects.Remove(this.gameObject);
				Destroy(this.gameObject);
			}
		}

        // DO ARC MOVEMENT STYLE AND DESTROY ON GROUND IMPACT
		else
		{
			TimerForArc += Time.deltaTime * 2.8f;
				this.transform.localPosition = new Vector3(
					this.transform.localPosition.x,
				this.transform.localPosition.y - Time.deltaTime * TimerForArc,
					this.transform.localPosition.z);

			if(this.transform.localPosition.y <= 0.0f)
			{
				if(SplatPrefab != null)
				{
					GameObject instance = (GameObject)Instantiate(SplatPrefab);
					//if(instance == null) Debug.Log("GameObject instance = (GameObject)Instantiate(SplatPrefab);");
					instance.transform.parent = UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnedObjectsAllOthers.transform;

					instance.transform.localPosition = new Vector3(this.transform.localPosition.x, 0, this.transform.localPosition.z);
					instance.transform.rotation = Quaternion.Euler(instance.transform.rotation.eulerAngles.x, instance.transform.rotation.eulerAngles.y, Random.Range(0, 360));
						
					//UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnedObjects.Add(instance);
				}

				//UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnedObjects.Remove(this.gameObject);
				Destroy(this.gameObject);
			}

		}
	}

	void OnCollisionEnter(Collision collision)
	{
        if (!__local) return;

 //       Debug.Log("Collision : [" + collision.gameObject + "];");

        if (collision.gameObject == UIGlobalVariablesScript.Singleton.MainCharacterRef) return;

//		Debug.Log("COLLISION DETECTED: " + collision.gameObject.name);
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
