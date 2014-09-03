using UnityEngine;
using System.Collections;

public class BarrelCollisionScript : MonoBehaviour 
{
	private bool BeginDestroy;
	//private float Timer = 0.3f;

	float leftBoundary;
	float rightBoundary;
	float lerp;
	float sign = 1;
	float lerpSpeed = 10;

	public GameObject[] BulletPrefabs;
	public Texture2D BarFrontTexture;
	public Texture2D BarBackgroundTexture;
	public GameObject DestroyedPrefab;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(BeginDestroy)
		{
			//Timer -= Time.deltaTime;
			//if(Timer <= 0)

		
			lerpSpeed += lerpSpeed * Time.deltaTime;
			lerp += Time.deltaTime * sign * lerpSpeed;
			if(lerp >= 1)
			{
				lerp = 1;
				sign *= -1;


			}
			else if(lerp <= 0)
			{
				lerp = 0;
				sign *= -1;


			}

			this.transform.localPosition = new Vector3(
				Mathf.Lerp(leftBoundary, rightBoundary, lerp),
				this.transform.localPosition.y,
				this.transform.localPosition.z);
		}
	}

	void OnCollisionEnter(Collision collision)
	{

		//Debug.Log("COLLISION DETECTED: " + collision.gameObject.tag);

		if(collision.gameObject.tag == "Bullet")
		{
			UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().MeterBar.mainTexture = BarFrontTexture;
			UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().MeterBarBackground.mainTexture = BarBackgroundTexture;
			UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().OnBulletHitBarrel(this.gameObject);
			if(!BeginDestroy)
			{
				BeginDestroy = true;
				leftBoundary = this.transform.localPosition.x - 0.02f;
				rightBoundary = this.transform.localPosition.x + 0.02f;


				if(DestroyedPrefab != null)
				{
					GameObject newProjectile = Instantiate( DestroyedPrefab ) as GameObject;
					newProjectile.transform.parent = this.transform.parent;
					newProjectile.transform.position = Vector3.zero;
					newProjectile.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
					newProjectile.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
					newProjectile.transform.localPosition = this.transform.localPosition;
					UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnedObjects.Add(newProjectile);
				}
				
				UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnedObjects.Remove(this.gameObject);
				Destroy(this.gameObject);

			}
		}
	}
}
