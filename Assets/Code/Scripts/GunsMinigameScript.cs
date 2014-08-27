using UnityEngine;
using System.Collections;

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
	public UISprite MeterBar;
	public UISprite MeterBarBackground;
	public GameObject CurrentBullet;
	private GameStateId State;
	private float AmmoTimer;
	private float NextBarrelSpawnTimer;

	private const float BarrelSpawnMinTime = 2;
	private const float BarrelSpawnMaxTime = 5;



	// Use this for initialization
	void Start () 
	{
		CurrentBullet = BulletPrefab[Random.Range(0, BulletPrefab.Length)];
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
					SpawnBarrel();
				}
			   

				AmmoTimer -= Time.deltaTime * 0.03f;
				if(AmmoTimer <= 0)
				{
					AmmoTimer = 0;
					State = GameStateId.Completed;
				}
				MeterBar.width = (int)((AmmoTimer) * MeterBarBackground.width);

				break;
			}

			case GameStateId.Paused:
			{
				break;
			}

			case GameStateId.Completed:
			{
				State = GameStateId.PrepareToExit;

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
	}

	public void OnBulletHitBarrel(GameObject barrel)
	{
		//Destroy(barrel);
		AmmoTimer += 0.02f;
		if(AmmoTimer >= 1) AmmoTimer = 1;
		CurrentBullet = barrel.GetComponent<BarrelCollisionScript>().BulletPrefab;

	}

	public void SpawnBarrel()
	{
		GameObject newProjectile = Instantiate( Barrels[Random.Range(0, Barrels.Length)] ) as GameObject;
		newProjectile.transform.parent = this.transform;
		newProjectile.transform.position = Vector3.zero;
		newProjectile.transform.rotation = Quaternion.identity;
		newProjectile.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
		newProjectile.transform.localPosition = new Vector3(Random.Range(-1.5f, 1.5f), 0, Random.Range(-1.5f, 1.5f));
		newProjectile.AddComponent<MeshCollider>();
		newProjectile.tag = "Bullet";
	}

	public void ShootBulletForward()
	{
		GameObject newProjectile = Instantiate( CurrentBullet ) as GameObject;
		newProjectile.transform.parent = this.transform;
		newProjectile.transform.position = UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.position;
		newProjectile.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0,360), Random.Range(0,360));
		newProjectile.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		newProjectile.transform.localPosition = UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localPosition + UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.forward * 0.14f;
		//newProjectile.AddComponent<ProjectileScript>();
		//newProjectile.velocity = transform.TransformDirection( Vector3( 0, 0, speed) );
		//newProjectile.AddComponent<MeshCollider>();
		newProjectile.GetComponent<Rigidbody>().AddForce(UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.forward * 20000);
	}


}
