using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunsMinigameScript : MonoBehaviour 
{
	public enum GameStateId
	{
		Initialize = 0,
		PrepareToStart,
		Countdown,
		Playing,
		Paused,
		Completed,
		PrepareToExit,
	}

	public GameObject GunPrefab;
	public GameObject[] BulletPrefab;
	public GameObject[] Barrels;
	public GameObject[] EnemyPrefabs;
	public UITexture MeterBar;
	public UITexture MeterBarBackground;
	public List<GameObject> CurrentBullets = new List<GameObject>();
	private GameStateId State;
	private float AmmoTimer;
	private float NextBarrelSpawnTimer;
	public List<GameObject> SpawnedObjects = new List<GameObject>();
	public Texture2D[] SlimeTextures;
	public Texture2D[] SlimeLevel2Textures;
	public Color[] SlimeColors;
	public GameObject[] MonsterSplatPrefabs;
	//public Texture2D[] BarTextures;
	public UITexture FrontBar;
	public GameObject[] SpecialBarrels;
	public GameObject[] BulletSplats;

	private const float BarrelSpawnMinTime = 2;
	private const float BarrelSpawnMaxTime = 5;
	private int Wave;
	private int[] WaveTimers = new int[] { 2, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
	private int[] WaveMinEnemies = new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 11, 15 };
	private int[] WaveMaxEnemies = new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 12, 17 };
	private float WaveTimerForNext;
	private float AutoShootCooldown;
	private float RandomCubeTimer;
	public GameObject RandomCubePrefab;
	
	// Use this for initialization
	void Start () 
	{
		CurrentBullets.Clear();
		CurrentBullets.Add(BulletPrefab[Random.Range(0, BulletPrefab.Length)]);
	}


	
	// Update is called once per frame
	void Update () 
	{
		switch(State)
		{
			case GameStateId.Initialize:
			{
				AmmoTimer = 1;
				GunPrefab.SetActive(true);
				State = GameStateId.PrepareToStart;
				NextBarrelSpawnTimer = Random.Range(BarrelSpawnMinTime, BarrelSpawnMaxTime);
				Wave = 0;
				WaveTimerForNext = WaveTimers[0];
				RandomCubeTimer = Random.Range(8, 20);

			//ShootBulletForward();
				break;
			}

			case GameStateId.PrepareToStart:
			{
				State = GameStateId.Countdown;
				break;
			}

			case GameStateId.Countdown:
			{
				State = GameStateId.Playing;

				GameObject.Find("GunfireLoop").GetComponent<AudioSource>().Play();

				break;
			}

			case GameStateId.Playing:
			{
				//if(Input.GetButtonDown("Fire1"))
				//	ShootBulletForward();

				NextBarrelSpawnTimer -= Time.deltaTime;
				if(NextBarrelSpawnTimer <= 0)
				{
					NextBarrelSpawnTimer = Random.Range(BarrelSpawnMinTime, BarrelSpawnMaxTime);
					if(Random.Range(0, 10) == 0)
						SpawnBarrel(true);
					else
						SpawnBarrel(false);
				}

				RandomCubeTimer -= Time.deltaTime;
				if(RandomCubeTimer <= 0)
				{
					SpawnRandomCube();
				RandomCubeTimer = Random.Range(8, 20);
				}

				AmmoTimer -= Time.deltaTime * 0.03f;
				if(AmmoTimer <= 0.001f)
				{
					AmmoTimer = 0.001f;
					State = GameStateId.Completed;
				}
				

				MeterBar.width = (int)(1351 * AmmoTimer);
				MeterBar.uvRect = new Rect(0, 0, AmmoTimer, 1);
				MeterBar.MarkAsChanged();
				//MeterBar.material.mainTextureOffset = new Vector2( AmmoTimer, 0);

				WaveTimerForNext -= Time.deltaTime;
				if(WaveTimerForNext <= 0)
				{
					Wave++;
					if(Wave == WaveTimers.Length)
					{
						State = GameStateId.Completed;
					}
					else
					{
						WaveTimerForNext = WaveTimers[Wave];
						int enemyCount = Random.Range(WaveMinEnemies[Wave], WaveMaxEnemies[Wave]);
						for(int a=0;a<enemyCount;++a)
						{
							SpawnEnemy(0);
						}
					}

				}

			AutoShootCooldown -= Time.deltaTime;
			if(AutoShootCooldown <= 0)
			{
				AutoShootCooldown = 0.18f;
				//UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.Jump);
				
				ShootBulletForward();
			}

				//MeterBar.renderer.set = (int)((AmmoTimer) * MeterBarBackground.width);

				break;
			}

			case GameStateId.Paused:
			{
				break;
			}

			case GameStateId.Completed:
			{
				State = GameStateId.PrepareToExit;
			GameObject.Find("GunfireLoop").GetComponent<AudioSource>().Stop();

				break;
			}

			case GameStateId.PrepareToExit:
			{
				
				UIClickButtonMasterScript.HandleClick(UIFunctionalityId.CloseCurrentMinigame, null);

				break;
			}
		}
	}


	public void Reset()
	{
		State = GameStateId.Initialize;
	}

	public void CloseGame()
	{
		GunPrefab.SetActive(false);
		this.gameObject.SetActive(false);

		for(int i=0;i<SpawnedObjects.Count;++i)
			Destroy(SpawnedObjects[i]);
		
		SpawnedObjects.Clear();
	}

	public void OnHitByEnemy(GameObject enemy)
	{
		AmmoTimer -= 0.2f;

		TemporaryDisableCollisionEvent collisionEvent = new TemporaryDisableCollisionEvent(gameObject);
		PresentationEventManager.Create(collisionEvent);
		UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.Bump_Into_Baddy);
		
		UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterControllerScript>().Forces.Add(
			new CharacterForces() { Speed = 800, Direction = -UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.forward, Length = 0.3f }
		);


		int randomCount = Random.Range(5, 8);
		for(int i=0;i<randomCount;++i)
		{
			ShootBulletLost(Random.Range(0.30f, 0.60f));
		}

		UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.GunGame_Bump_Into_Baddy);

//		GameObject instance = (GameObject)Instantiate(enemy.GetComponent<GunGameEnemyScript>().Splat);
//		instance.transform.parent = enemy.transform.parent;
//		instance.transform.position = enemy.transform.position;
//		instance.transform.rotation = Quaternion.Euler(instance.transform.rotation.eulerAngles.x, instance.transform.rotation.eulerAngles.y, Random.Range(0, 360));
//		
//		UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnedObjects.Add(instance);
	}
	
	public void OnBulletHitBarrel(GameObject barrel)
	{
		//Destroy(barrel);
		AmmoTimer += 0.07f;
		if(AmmoTimer >= 1) AmmoTimer = 1;
		GameObject[] prefabs = barrel.GetComponent<BarrelCollisionScript>().BulletPrefabs;
		CurrentBullets.Clear();
		CurrentBullets.AddRange(prefabs);

		UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.GunGame_barrel_destroy);
	}

	public GameObject SpawnEnemy(int level)
	{
		if(level >= EnemyPrefabs.Length) level = EnemyPrefabs.Length - 1;

		Texture2D[] textures = SlimeTextures;
		if(level == 1) textures = SlimeLevel2Textures;

		float scale = 1;
		if(level == 1) scale = 3;

		GameObject newProjectile = Instantiate( EnemyPrefabs[level] ) as GameObject;
		newProjectile.transform.parent = this.transform;
		newProjectile.transform.position = Vector3.zero;
		newProjectile.transform.rotation = Quaternion.identity;
		newProjectile.transform.localScale = new Vector3(0.06f * scale, 0.06f * scale, 0.06f * scale);
		newProjectile.transform.localPosition = new Vector3(Random.Range(-2.5f, 2.5f), 0, Random.Range(-2.5f, 2.5f));

		//if(level == 1)
		//	newProjectile.transform.rotation = Quaternion.Euler(15, 0, 0);

		int textureIndex = Random.Range(0, textures.Length);
		Texture2D texture = textures[textureIndex];

		for(int i=0;i<newProjectile.transform.childCount;++i)
			if(newProjectile.transform.GetChild(i).renderer != null)
				newProjectile.transform.GetChild(i).renderer.material.mainTexture = texture;

		newProjectile.GetComponent<GunGameEnemyScript>().BulletSplat = BulletSplats[textureIndex];
		newProjectile.GetComponent<GunGameEnemyScript>().SkinColor = SlimeColors[textureIndex];
		newProjectile.GetComponent<GunGameEnemyScript>().Speed = Random.Range(0.05f, 0.11f) * (level + 1);
		newProjectile.GetComponent<GunGameEnemyScript>().Splat = MonsterSplatPrefabs[textureIndex];
		newProjectile.GetComponent<GunGameEnemyScript>().Level = level;
		newProjectile.GetComponent<GunGameEnemyScript>().TargetToFollow = UIGlobalVariablesScript.Singleton.MainCharacterRef;
		newProjectile.name = texture.name;
		SpawnedObjects.Add(newProjectile);
		return newProjectile;
	}

	public void SpawnBarrel(bool special)
	{
		GameObject newProjectile = null;

		if(special)
		{
			newProjectile = Instantiate( SpecialBarrels[Random.Range(0, SpecialBarrels.Length)] ) as GameObject;
		}
		else
		{
			newProjectile = Instantiate( Barrels[Random.Range(0, Barrels.Length)] ) as GameObject;
		}

		newProjectile.transform.parent = this.transform;
		newProjectile.transform.position = Vector3.zero;
		newProjectile.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
		newProjectile.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
		newProjectile.transform.localPosition = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));

		SpawnedObjects.Add(newProjectile);
	}

	public void SpawnRandomCube()
	{
		GameObject newProjectile = Instantiate( RandomCubePrefab ) as GameObject;
				
		newProjectile.transform.parent = this.transform;
		newProjectile.transform.position = Vector3.zero;
		newProjectile.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
		newProjectile.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
		newProjectile.transform.localPosition = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
		
		SpawnedObjects.Add(newProjectile);
	}

	public void ShootBulletForward()
	{
		GameObject newProjectile = Instantiate( CurrentBullets[Random.Range(0,  CurrentBullets.Count)] ) as GameObject;
		newProjectile.transform.parent = this.transform;
		newProjectile.transform.position = UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.position;
		newProjectile.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0,360), Random.Range(0,360));
		newProjectile.transform.localScale = new Vector3(0.116f, 0.116f, 0.116f);
		newProjectile.transform.localPosition = UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localPosition + UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.forward * 0.14f + new Vector3(0, 0.05f, 0);
		//newProjectile.AddComponent<ProjectileScript>();
		//newProjectile.velocity = transform.TransformDirection( Vector3( 0, 0, speed) );
		//newProjectile.AddComponent<MeshCollider>();
		newProjectile.GetComponent<Rigidbody>().AddForce(UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.forward * 20000);
		SpawnedObjects.Add(newProjectile);
	}



	public void ShootBulletLost(float speedVariationFactor)
	{
		GameObject newProjectile = Instantiate( CurrentBullets[Random.Range(0,  CurrentBullets.Count)] ) as GameObject;
		newProjectile.transform.parent = this.transform;
		newProjectile.transform.position = UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.position;
		newProjectile.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0,360), Random.Range(0,360));
		newProjectile.transform.localScale = new Vector3(0.116f, 0.116f, 0.116f) * Random.Range(0.80f, 1.0f);
		newProjectile.transform.localPosition = UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localPosition + UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.forward * 0.14f + new Vector3(0, 0.3f, 0);
		//newProjectile.AddComponent<ProjectileScript>();
		//newProjectile.velocity = transform.TransformDirection( Vector3( 0, 0, speed) );
		//newProjectile.AddComponent<MeshCollider>();

		Vector3 direction = Vector3.zero;
		direction.x = Random.Range(-0.3f, 0.3f);
		direction.z = Random.Range(-0.3f, 0.3f);


		newProjectile.GetComponent<Rigidbody>().AddForce((UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.up + direction) * 20000 * speedVariationFactor);
		
		SpawnedObjects.Add(newProjectile);
	}


	public void ShootEnemyDestroyedEffects(float speedVariationFactor, Vector3 position, Color color, GameObject bulletSplat)
	{
		GameObject newProjectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		newProjectile.transform.parent = this.transform;
		newProjectile.transform.position = UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.position;
		newProjectile.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0,360), Random.Range(0,360));
		newProjectile.transform.localScale = new Vector3(0.116f, 0.116f, 0.116f) * Random.Range(0.20f, 0.70f);
		newProjectile.transform.localPosition = position;
				
		Vector3 direction = Vector3.zero;
		direction.x = Random.Range(-0.3f, 0.3f);
		direction.z = Random.Range(-0.3f, 0.3f);

		//newProjectile.layer = LayerMask.NameToLayer("Default");
		//newProjectile.tag = "Untagged";

		newProjectile.layer = LayerMask.NameToLayer("Projectiles");
		newProjectile.AddComponent<Rigidbody>();
		newProjectile.GetComponent<Rigidbody>().velocity = (Vector3.up + direction) * 500 * speedVariationFactor;
		newProjectile.AddComponent<MonsterSplatExplosionScript>();
		newProjectile.GetComponent<MonsterSplatExplosionScript>().SplatPrefab = bulletSplat;

		//newProjectile.GetComponent<Rigidbody>().AddForce(Vector3.down * 20000);
		newProjectile.renderer.material.color = color;
		//newProjectile.GetComponent<Rigidbody>().AddForce((Vector3.up + direction) * 5000 * speedVariationFactor);
		//newProjectile.GetComponent<Rigidbody>().AddExplosionForce(10, position + new Vector3(0,1,0), 1);


		SpawnedObjects.Add(newProjectile);
	}











}
