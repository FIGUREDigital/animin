using UnityEngine;
using System.Collections;

public class GunGameEnemyScript : MonoBehaviour 
{
	public float Speed;
	public GameObject TargetToFollow;
	public int Level;
	public bool HasMerged;
	public GameObject Splat;
	public Color SkinColor;
	public GameObject BulletSplat;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		GameObject mainCharacter = UIGlobalVariablesScript.Singleton.MainCharacterRef;
		GunsMinigameScript minigame = UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>();

		if(HasMerged) 
		{
			minigame.SpawnedObjects.Remove(this.gameObject);
			Destroy(this.gameObject);
			return;
		}

		Vector3 direction = Vector3.Normalize(TargetToFollow.transform.localPosition - this.transform.localPosition);
		this.transform.localPosition += direction * Speed * Time.deltaTime;

		GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("EnemyGunGame");
		for(int i=0;i<allEnemies.Length;++i)
		{
			if(allEnemies[i] == this.gameObject) continue;
			GunGameEnemyScript enemyScript = allEnemies[i].GetComponent<GunGameEnemyScript>();
			if(enemyScript.Level != this.Level) continue;
			if(enemyScript.HasMerged) continue;

			float radius = Vector3.Distance(allEnemies[i].transform.localPosition, this.gameObject.transform.localPosition);

			if(radius <= 0.06f)
			{
				Debug.Log("the enemy has been merged");
				GameObject newObject =	minigame.SpawnEnemy(Level + 1);
				newObject.transform.localPosition = this.gameObject.transform.localPosition;

				HasMerged = true;
				enemyScript.HasMerged = true;

				//minigame.SpawnedObjects.Remove(this.gameObject);
				//Destroy(this.gameObject);

				//minigame.SpawnedObjects.Remove(allEnemies[i]);
				//Destroy(allEnemies[i]);

				UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.GunGame_monsters_merge);

			}
			else if(radius <= 0.6f && enemyScript.TargetToFollow == mainCharacter)
			{
				this.TargetToFollow = allEnemies[i];
				enemyScript.TargetToFollow = this.gameObject;
				Speed += Speed * 1.10f * Time.deltaTime;
			}
			else
			{
				this.TargetToFollow = mainCharacter;
			}
		}

		RotateToLookAtPoint(TargetToFollow.transform.position);

		UpdateRotationLookAt();
	}

	private void UpdateRotationLookAt()
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, RotateDirectionLookAt, Time.deltaTime * 6);
	}
	
	public void ResetRotation()
	{
		RotateDirectionLookAt = transform.rotation;
	}
	
	private Quaternion RotateDirectionLookAt = Quaternion.Euler(0, 180, 0);
	
	public void RotateToLookAtPoint(Vector3 worldPoint)
	{
		RotateDirectionLookAt = Quaternion.LookRotation(Vector3.Normalize(worldPoint - transform.position));
	}

	void OnCollisionEnter(Collision collision)
	{
//		Debug.Log("COLLISION DETECTED: " + collision.gameObject.name + "_" + this.name);
		
		if(collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Player")
		{
			bool isRightColor = false;

			if(collision.gameObject.name.Contains("Green") && this.name.Contains("green"))
				isRightColor = true;

			if(collision.gameObject.name.Contains("Yellow") && this.name.Contains("yellow"))
				isRightColor = true;

			if(collision.gameObject.name.Contains("Blue") && this.name.Contains("blue"))
				isRightColor = true;

			if(collision.gameObject.name.Contains("Red") && this.name.Contains("red"))
				isRightColor = true;

			if(collision.gameObject.name.Contains("ALL"))
				isRightColor = true;



			if(isRightColor)
			{
				if(TargetToFollow != null)
				{
					Debug.Log("TargetToFollow != null: " + TargetToFollow.name);
				}

				if(TargetToFollow != null && TargetToFollow != UIGlobalVariablesScript.Singleton.MainCharacterRef)
				{
					Debug.Log("the new code works");

					GunGameEnemyScript script = TargetToFollow.GetComponent<GunGameEnemyScript>();

					if(script.TargetToFollow == this.gameObject)
					{
						script.TargetToFollow = UIGlobalVariablesScript.Singleton.MainCharacterRef;
					}
				}

				UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnedObjects.Remove(this.gameObject);
				Destroy(this.gameObject);

				GameObject instance = (GameObject)Instantiate(Splat);
				instance.transform.parent = this.transform.parent;
				instance.transform.position = this.transform.position;
				instance.transform.rotation = Quaternion.Euler(instance.transform.rotation.eulerAngles.x, instance.transform.rotation.eulerAngles.y, Random.Range(0, 360));
				
				UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnedObjects.Add(instance);

				for(int i=0;i<20;++i)
				{
					UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().ShootEnemyDestroyedEffects(Random.Range(0.30f, 0.50f), this.gameObject.transform.localPosition, SkinColor, BulletSplat);
				}

			}
			
			
		}
	}
}
