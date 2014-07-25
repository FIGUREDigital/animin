using UnityEngine;
using System.Collections.Generic;


public class MinigameCollectorScript : MonoBehaviour 
{
	//private GameObject[,] CubeMatrix;

	private List<GameObject> Collections = new List<GameObject>();
	public GameObject CharacterRef;

	//private List<GameObject> AvailableSpotsToPlaceStars = new List<GameObject>();

	public List<GameObject> EvilCharacters = new List<GameObject>();
	public GameObject[] EvilCharacterPool;


	private int oldLevelId = -1;
	private int currentLevelId = -1;
	public List<int> CompletedLevels = new List<int>();
	public const int MaxLevels = 4;

	//private const int MapWidth = 5;
	//private const int MapHeight = 5;

	private List<int> LevelsToComplete = new List<int>();

	//private Vector3 CenterOffset = new Vector3(0.5f, 0, 0.5f);
	//private Vector3 CubeScaleSize = new Vector3(0.2f, 0.20f, 0.2f);
	public GameObject Stage;
	private float? GameStartDelay;


	// Use this for initialization
	void Awake () 
	{
		//Debug.Log("STAGES AWAKE!!: " + Stage.transform.childCount.ToString());

		for(int i=0;i<Stage.transform.childCount;++i)
		{
			// its a level!
			if(Stage.transform.GetChild(i).childCount > 0)
			{
				//Debug.Log("STAGES 1");
				Transform stageTransform = Stage.transform.GetChild(i);

				for(int a=0;a<stageTransform.childCount;++a)
				{
					//Debug.Log("STAGES 2");
					if(stageTransform.GetChild(a).name.StartsWith("cubes"))
					{
						//Debug.Log("STAGES 3");
						Transform stageCubes = stageTransform.GetChild(a);
						for(int b=0;b<stageCubes.childCount;++b)
						{
							stageCubes.GetChild(b).gameObject.AddComponent<MeshCollider>();
							CubeAnimatonScript script = stageCubes.GetChild(b).gameObject.AddComponent<CubeAnimatonScript>();
							script.ResetPosition = script.transform.position;
							script.ValueNext = script.transform.position;
						}
					}
				}
			}
		}
	}



	void OnGUI()
	{
//		GameObject obj = GameObject.Find("TextureBufferCamera");
//
//		GameObject hole = GameObject.Find("insideHole");
//
//		Texture texture = obj.GetComponent<Camera>().targetTexture;
//		hole.renderer.material.mainTexture = texture;
//
//
//		hole.renderer.material.SetTextureScale (
//			"_MainTex", 
//			new Vector2(0.05f, 0.05f)
//			);
//		
//		hole.renderer.material.SetTextureOffset (
//			"_MainTex", 
//			new Vector2(0.5f, 0.5f)
//			);

		//GUI.DrawTexture(new Rect(0, 0, 200, 200), texture);
	}
	
	// Update is called once per frame
	void Update () 
	{

		/*GameObject obj = GameObject.Find("TextureBufferCamera");
		
		GameObject hole = GameObject.Find("insideHole");

		if(obj != null)
		{
			Texture texture = obj.GetComponent<Camera>().targetTexture;
			hole.renderer.material.mainTexture = texture;*/
		/*	
			
			hole.renderer.material.SetTextureScale (
				"_MainTex", 
				new Vector2(0.05f, 0.05f)
				);
			
			hole.renderer.material.SetTextureOffset (
				"_MainTex", 
				new Vector2(0.5f, 0.5f)
				);*/
		//}

		if(GameStartDelay.HasValue)
		{
//			Debug.Log("DELAY: " + GameStartDelay.ToString());
			GameStartDelay -= Time.deltaTime;

			if(GameStartDelay <= 0)
			{
				for(int i=0;i<EvilCharacters.Count;++i)
					EvilCharacters[i].SetActive(true);

				if(oldLevelId != -1)
				{
					Stage.transform.GetChild(oldLevelId).gameObject.SetActive(false);


				}
				
				UIGlobalVariablesScript.Singleton.MainCharacterRef.SetActive(true);
				
				GameStartDelay = null;
			
				ResetCharacter();
			}
		}
		else
		{
		
			CheckForPickupCollision ();

			if (CharacterRef.transform.position.y <= -100f) 
			{
				CharacterRef.GetComponent<CharacterProgressScript>().StarsOwned -= 3;
				if(CharacterRef.GetComponent<CharacterProgressScript>().StarsOwned < 0 ) CharacterRef.GetComponent<CharacterProgressScript>().StarsOwned = 0;
				Reset();
			}

			//if(Input.GetKeyDown(KeyCode.F))
			//	Reset();
		}
//		if(CharacterRef.transform.localPosition.y <= -2)
//		{
//			CharacterRef.GetComponent<CharacterControllerScript>().IsResetFalling = true;
//		}



		UIGlobalVariablesScript.Singleton.TextForStarsInMiniCollector.text = CharacterRef.GetComponent<CharacterProgressScript>().StarsOwned.ToString();
	
	}

	void ResetCharacter()
	{
		//CharacterRef.transform.localPosition = Vector3.zero;
		CharacterRef.GetComponent<CharacterControllerScript>().IsResetFalling = true;


		Transform stageTransform = Stage.transform.GetChild(currentLevelId);
		
		for(int a=0;a<stageTransform.childCount;++a)
		{
			//Debug.Log("STAGES 2");
			if(stageTransform.GetChild(a).name.StartsWith("dummies"))
			{
				//Debug.Log("STAGES 3");
				Transform dummies = stageTransform.GetChild(a);
				for(int b=0;b<dummies.childCount;++b)
				{
					if(dummies.GetChild(b).name.StartsWith("start"))
					{
//						Debug.Log("STAGES 4: " + dummies.GetChild(b).transform.position.ToString());
						CharacterRef.transform.position = dummies.GetChild(b).transform.position;
					}
				}
			}
		}
			


	}
	/*
	void OnGUI()
	{
		//GUI.skin.label.fontSize = 30;
		//GUI.skin.button.fontSize = 30;


		if (GUI.Button (new Rect (10, 10, 120, 50), "Reset")) {
			Reset();
			
		}
		//GUI.Label (new Rect (Screen.width - 500, 10, 200, 50), "Stars: " + StarsOwned.ToString());
	}
*/

	public void OnEvilCharacterHitFromTop(GameObject gameObject)
	{
		for(int i=0;i<EvilCharacters.Count;++i)
		{
			if(EvilCharacters[i] == gameObject)
			{
				//Debug.Log("HIT FROM ABOVE");

				gameObject.AddComponent<EnemyDeathAnimationScript>();
				EvilCharacterPatternMovementScript movementScript = gameObject.GetComponent<EvilCharacterPatternMovementScript>();
				if(movementScript != null) Destroy(movementScript);

				EvilCharacters.RemoveAt(i);
				i--;

				//Debug.Log("chars found:" + EvilCharacters.Count.ToString());
			}
		}
	}

	public void OnEvilCharacterHit(GameObject gameObject)
	{
		CharacterRef.GetComponent<CharacterProgressScript>().StarsOwned -= 3;

		gameObject.GetComponent<BoxCollider>().enabled = false;

		TemporaryDisableCollisionEvent collisionEvent = new TemporaryDisableCollisionEvent(gameObject);
		PresentationEventManager.Create(collisionEvent);

		Debug.Log("ADDING FORCE");
		CharacterRef.GetComponent<CharacterControllerScript>().Forces.Add(
			new CharacterForces() { Speed = 900, Direction = -CharacterRef.transform.forward, Length = 0.3f }
		);

		//CharacterRef.AddComponent<FlashMaterialColorScript>();
	}

	/*private void UpdateAnimations()
	{
		for (int i=0; i<AnimationJobs.Count; ++i) {

			AnimationJobs [i].Lerp += Time.deltaTime;
			if (AnimationJobs [i].Lerp >= 1) {
				AnimationJobs [i].Lerp = 1;
			}

			AnimationJobs [i].AnimatedObject.transform.localPosition = Vector3.Lerp (AnimationJobs [i].StartPosition, AnimationJobs [i].EndPosition, Mathf.Sin (AnimationJobs [i].Lerp * 90 * Mathf.Deg2Rad));
		
			if (AnimationJobs [i].Lerp >= 1) {
				AnimationJobs.RemoveAt (i);
				i--;
			}
		
		}
	}*/

	private void CheckForPickupCollision()
	{
		for (int i=0; i<Collections.Count; ++i) {
				
			float d = Vector3.Distance(Collections[i].transform.position, CharacterRef.transform.position);
//			Debug.Log(d);
			if(d <= 25)
			{
				CharacterRef.GetComponent<CharacterProgressScript>().StarsOwned++;
				GameObject.Destroy(Collections[i]);

				Collections.RemoveAt(i);
				i--;

				if (Collections.Count <= 0) {
					LevelsToComplete.Remove(currentLevelId);
					Reset ();
				}
			}
		}
	}


	public void HardcoreReset()
	{
		oldLevelId = -1;
		currentLevelId = -1;
		CompletedLevels.Clear();

		LevelsToComplete.Clear();
		for(int i=0;i<Stage.transform.childCount;++i)
		{
			if(Stage.transform.GetChild(i).transform.childCount > 0)
			{
				LevelsToComplete.Add(i);
			}
		}


		for(int i=0;i<Stage.transform.childCount;++i)
		{
			// its a level!
			if(Stage.transform.GetChild(i).childCount > 0)
			{
				//Debug.Log("STAGES 1");
				Transform stageTransform = Stage.transform.GetChild(i);
				
				for(int a=0;a<stageTransform.childCount;++a)
				{
					//Debug.Log("STAGES 2");
					if(stageTransform.GetChild(a).name.StartsWith("cubes"))
					{
						//Debug.Log("STAGES 3");
						Transform stageCubes = stageTransform.GetChild(a);
						for(int b=0;b<stageCubes.childCount;++b)
						{

							CubeAnimatonScript script = stageCubes.GetChild(b).gameObject.GetComponent<CubeAnimatonScript>();
							script.transform.position = script.ResetPosition;
							script.ValueNext = script.ResetPosition;
						}
					}
				}
			}
		}


		Reset();

	}

	public void Reset()
	{
		EvilCharacters.Clear();

		for(int i=0;i<EvilCharacterPool.Length;++i)
		{
			EvilCharacterPool[i].SetActive(false);

			EvilCharacterPatternMovementScript component = EvilCharacterPool[i].GetComponent<EvilCharacterPatternMovementScript>();
			if(component != null) Destroy(component);
		}

		//AvailableSpotsToPlaceStars.Clear ();


		for (int i=0; i<Collections.Count; ++i) {
			GameObject.Destroy (Collections [i]);
		}
		Collections.Clear ();

		Transform stageTransformOld = null;
		// FADE OUT EXISTING LEVEL
		if(currentLevelId != -1)
		{
			stageTransformOld = Stage.transform.GetChild(currentLevelId);

			for(int a=0;a<stageTransformOld.childCount;++a)
			{
				if(stageTransformOld.GetChild(a).name.StartsWith("cubes"))
				{
					Transform cubes = stageTransformOld.GetChild(a);
					
					for(int b=0;b<cubes.childCount;++b)
					{
						CubeAnimatonScript cubeScript = cubes.GetChild(b).gameObject.GetComponent<CubeAnimatonScript>();


						cubeScript.ValueNext = cubeScript.transform.position + new Vector3(0, -600, 0);
						//cubeScript.transform.position = cubeScript.transform.position + new Vector3(0, -200, 0);
						cubeScript.Delay = 0.03f * b;
						cubeScript.ResetPosition = cubeScript.transform.position;

					}
				}
			}
		}

		oldLevelId = currentLevelId;
		if(currentLevelId == -1)
		{
			currentLevelId = LevelsToComplete[0];
		}
		else if(LevelsToComplete.Count > 0)
		{
			LevelsToComplete.Remove(currentLevelId);
			currentLevelId = LevelsToComplete[UnityEngine.Random.Range(0, LevelsToComplete.Count)];
		}


		//Debug.Log("Stage.transform.childCount: " + Stage.transform.childCount.ToString());
		for(int i=0;i<Stage.transform.childCount;++i)
		{
			if(Stage.transform.GetChild(i).childCount > 0)
			{
				if(stageTransformOld != Stage.transform.GetChild(i).transform)
					Stage.transform.GetChild(i).gameObject.SetActive(false);
			}
		}

		Stage.transform.GetChild(currentLevelId).gameObject.SetActive(true);
		//Debug.Log("Stage.transform.GetChild(currentLevelId): " + Stage.transform.GetChild(currentLevelId).name);


		GameStartDelay = 0.5f;

		int badGuyCounter = 0;
		Transform stageTransform = Stage.transform.GetChild(currentLevelId);

		// BUILD BAD GYES
		for(int a=0;a<stageTransform.childCount;++a)
		{

			if(stageTransform.GetChild(a).name.StartsWith("dummies"))
			{
				Transform dummies = stageTransform.GetChild(a);
				for(int b=0;b<dummies.childCount;++b)
				{
					if(dummies.GetChild(b).name.StartsWith("badguy"))
					{
						CharacterRef.transform.position = dummies.GetChild(b).transform.position;

						EvilCharacterPool[badGuyCounter].SetActive(true);

						EvilCharacterPatternMovementScript component = EvilCharacterPool[badGuyCounter].AddComponent<EvilCharacterPatternMovementScript>();
						component.Pattern = new Vector3[dummies.GetChild(b).transform.childCount];
						for(int c=0;c<dummies.GetChild(b).childCount;++c)
						{
							component.Pattern[c] = dummies.GetChild(b).transform.GetChild(c).transform.position;
						}
						component.transform.position = dummies.GetChild(b).transform.GetChild(0).transform.position;
						component.Speed = 30.1f;
						component.Lerp = 0;
						component.Index = 0;


						EvilCharacterPool[badGuyCounter].GetComponent<BoxCollider>().enabled = true;
						for(int i=0;i<EvilCharacterPool[badGuyCounter].transform.childCount;++i)
						{
							if(EvilCharacterPool[badGuyCounter].transform.GetChild(i).name == "Sphere")
								EvilCharacterPool[badGuyCounter].transform.GetChild(i).gameObject.SetActive(false);
							else 
								EvilCharacterPool[badGuyCounter].transform.GetChild(i).gameObject.SetActive(true);
						}



						//EvilCharacterPool[badGuyCounter].GetComponent<Animator>().SetBool("None", false );
						//EvilCharacterPool[badGuyCounter].transform.localScale = new Vector3(EvilCharacterPool[badGuyCounter].transform.localScale.x, EvilCharacterPool[badGuyCounter].transform.localScale.x, EvilCharacterPool[badGuyCounter].transform.localScale.x);
						EvilCharacters.Add(EvilCharacterPool[badGuyCounter]);
						badGuyCounter++;

					}

					// BUILD STARS
					if(dummies.GetChild(b).name.StartsWith("star") && !dummies.GetChild(b).name.StartsWith("start"))
					{
						//GameObject randomParent = CubeMatrix [(int)builder.CollectionPoints[i].x, (int)builder.CollectionPoints[i].y];
						
						GameObject collection = GameObject.CreatePrimitive(PrimitiveType.Sphere);
						Destroy (collection.rigidbody);
						
						Collections.Add (collection);
						
						
						//GameObject randomParent = AvailableSpotsToPlaceStars [Randomizer.Next (0, AvailableSpotsToPlaceStars.Count)];
						//AvailableSpotsToPlaceStars.Remove (randomParent);

						collection.transform.parent = dummies.GetChild(b).transform.parent;
						collection.transform.position = dummies.GetChild(b).transform.position;
						collection.transform.localScale = new Vector3(1, 1, 1);
						//collection.transform.localRotation = Quaternion.identity;
						//collection.transform.localPosition = new Vector3(-1, 0.3f, -1.0f);
						
						collection.AddComponent<OscillationUpDownScript>();
						
						SphereCollider colliderToKill = collection.GetComponent<SphereCollider>();
						Destroy(colliderToKill);
						
					}
				}
			}


		}

	

		for(int i=0;i<EvilCharacters.Count;++i)
			EvilCharacters[i].SetActive(false);

		UIGlobalVariablesScript.Singleton.MainCharacterRef.SetActive(false);

		//Debug.Log("stageTransform.childCount: " + stageTransform.childCount.ToString());

		// BUILD ANIMATION FOR CUBES
		for(int a=0;a<stageTransform.childCount;++a)
		{
			if(stageTransform.GetChild(a).name.StartsWith("cubes"))
			{
				Transform cubes = stageTransform.GetChild(a);
				//Debug.Log("cubes: " + cubes.childCount.ToString());

				for(int b=0;b<cubes.childCount;++b)
				{
//					Debug.Log("adding cube animation down to: " + cubes.GetChild(b).name);
					CubeAnimatonScript cubeScript = cubes.GetChild(b).gameObject.GetComponent<CubeAnimatonScript>();
					cubeScript.ValueNext = cubeScript.ResetPosition;
					cubeScript.transform.position = cubeScript.ResetPosition + new Vector3(0, 800, 0);
					cubeScript.Delay = 0.2f + 0.04f * b;
					GameStartDelay += 0.04f;

				}
			}
		}
	}
}

