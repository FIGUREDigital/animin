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
	public string BulletSplat;

	
	// SHAUN START
	// ---------------------------------------------------------------------------------------------------------------------------------------------------
	
	private 	bool	__local;
	
	//public bool local { get { return __local;} }
	public void SetLocal(bool local) { __local = local; }
	
	private 	Vector3 				__networkPosition;
	private		Quaternion 				__networkRotation;
	
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		//Debug.Log("OnPhotonSerializeView\t:\t" + stream.isWriting);
		
		if (stream.isWriting) {
			// OUR PLAYER, SEND OTHERS OUR DATA
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		} else {
			// OTHER (NETWORK) PLAYER, RECEIVE DATA
			__networkPosition = (Vector3)stream.ReceiveNext();
			__networkRotation = (Quaternion)stream.ReceiveNext();
		}
	}
	
	private void UpdatePositionRemotely(Vector3 position) {
		transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 5);
	}
	
	private void UpdateRotationRemotely(Quaternion rotation) {
		transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5);
	}
	
	// SHAUN END
	// ---------------------------------------------------------------------------------------------------------------------------------------------------
	


	// Use this for initialization
	void Start () 
	{
		int level = int.Parse(GetComponent<PhotonView>().instantiationData[0].ToString());
//		Debug.Log("RECEIVED level: " + level.ToString());
		UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnEnemyEnd(this.gameObject, level);

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GameController.instance.gameType == GameType.NETWORK) {
			if (!__local) {
				if (PhotonNetwork.connectionStateDetailed == PeerState.Joined) {
					UpdatePositionRemotely(__networkPosition);
					UpdateRotationRemotely(__networkRotation);
				}
			}
		}
		
		/////
		
		if (__local) 
		{

			//GameObject mainCharacter = UIGlobalVariablesScript.Singleton.MainCharacterRef;
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
					GameObject newObject =	minigame.SpawnEnemyStart(Level + 1);
					newObject.transform.localPosition = this.gameObject.transform.localPosition;

					HasMerged = true;
					enemyScript.HasMerged = true;

					//minigame.SpawnedObjects.Remove(this.gameObject);
					//Destroy(this.gameObject);

					//minigame.SpawnedObjects.Remove(allEnemies[i]);
					//Destroy(allEnemies[i]);

					UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.GunGame_monsters_merge);

				}
				else if(radius <= 0.6f && minigame.PlayersCharacters.Contains(TargetToFollow ))
				{
					this.TargetToFollow = allEnemies[i];
					enemyScript.TargetToFollow = this.gameObject;
					Speed += Speed * 1.10f * Time.deltaTime;
				}
				else
				{
					this.TargetToFollow = minigame.PlayersCharacters[Random.Range(0, minigame.PlayersCharacters.Count)];
				}
			}

			RotateToLookAtPoint(TargetToFollow.transform.position);

			UpdateRotationLookAt();
		}
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

		GunsMinigameScript gunGame = UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>();
		
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

				if(TargetToFollow != null && !gunGame.PlayersCharacters.Contains(TargetToFollow ))
				{
					GunGameEnemyScript script = TargetToFollow.GetComponent<GunGameEnemyScript>();
					if(script == null) Debug.Log(TargetToFollow.name);

					if(script.TargetToFollow == this.gameObject)
					{
						script.TargetToFollow = gunGame.PlayersCharacters[Random.Range(0, gunGame.PlayersCharacters.Count)];
					}
				}

				UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().Points += 200;// * (Level + 1);
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
