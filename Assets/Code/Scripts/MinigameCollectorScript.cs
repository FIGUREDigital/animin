using UnityEngine;
using System.Collections.Generic;


public class MinigameCollectorScript : MonoBehaviour 
{
	//private GameObject[,] CubeMatrix;

	private List<GameObject> Collections = new List<GameObject>();
	public GameObject CharacterRef;
	private List<AnimationJob> AnimationJobs = new List<AnimationJob>();
	//private List<GameObject> AvailableSpotsToPlaceStars = new List<GameObject>();

	public List<GameObject> EvilCharacters = new List<GameObject>();
	public GameObject[] EvilCharacterPool;


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

	private class AnimationJob
	{
		public Vector3 StartPosition;
		public Vector3 EndPosition;
		public float Lerp;
		public GameObject AnimatedObject;

		public AnimationJob(GameObject animatedObject, Vector3 start, Vector3 end)
		{
			StartPosition = start;
			EndPosition = end;
			Lerp = 0;
			AnimatedObject = animatedObject;

		}
	}

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
					if(stageTransform.GetChild(a).name == "cubes")
					{
						//Debug.Log("STAGES 3");
						Transform stageCubes = stageTransform.GetChild(a);
						for(int b=0;b<stageCubes.childCount;++b)
						{
							stageCubes.GetChild(b).gameObject.AddComponent<BoxCollider>();
						}
					}
				}
			}
		}

	/*	CubeMatrix = new GameObject[MapWidth, MapHeight];

		int xCounter = 0;
		int yCounter = 0;

		for(int i=0;i<Stage.transform.childCount;++i)
		{
			if(Stage.transform.GetChild(i).name.StartsWith("Cube"))
			{
				CubeMatrix[yCounter, xCounter] = Stage.transform.GetChild(i).gameObject;
				xCounter++;
				if(xCounter == 5)
				{
					xCounter = 0;
					yCounter++;
				}
			}
		}
		
		for (int i=0; i<MapHeight; ++i) 
		{
			for(int j=0; j<MapWidth; ++j)
			{
				//CubeMatrix[j,i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
				//CubeMatrix[j,i].transform.parent = this.transform;
				
				//CubeMatrix[j,i].transform.localPosition = new Vector3((j * CubeScaleSize.x) + 0.1f, -0.10f, (i * CubeScaleSize.z) + 0.1f) - CenterOffset;
				//CubeMatrix[j,i].transform.localScale = new Vector3(CubeScaleSize.x, CubeScaleSize.y, CubeScaleSize.z);
				//CubeMatrix[j,i].transform.localRotation = Quaternion.identity;
				
			}
		}*/

	

		//HardcoreReset();
	}
	
	// Update is called once per frame
	void Update () 
	{

		if(GameStartDelay.HasValue)
		{
//			Debug.Log("DELAY: " + GameStartDelay.ToString());
			GameStartDelay -= Time.deltaTime;

			if(GameStartDelay <= 0)
			{
				for(int i=0;i<EvilCharacters.Count;++i)
					EvilCharacters[i].SetActive(true);
				
				UIGlobalVariablesScript.Singleton.MainCharacterRef.SetActive(true);
				
				GameStartDelay = null;
			
				ResetCharacter();
			}
		}
		else
		{
		
			CheckForPickupCollision ();
		}
//		if(CharacterRef.transform.localPosition.y <= -2)
//		{
//			CharacterRef.GetComponent<CharacterControllerScript>().IsResetFalling = true;
//		}

//		if (CharacterRef.transform.localPosition.y <= -5f) 
//		{
//			CharacterRef.GetComponent<CharacterProgressScript>().StarsOwned -= 3;
//			if(CharacterRef.GetComponent<CharacterProgressScript>().StarsOwned < 0 ) CharacterRef.GetComponent<CharacterProgressScript>().StarsOwned = 0;
//			Reset();
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
			if(stageTransform.GetChild(a).name == "dummies")
			{
				//Debug.Log("STAGES 3");
				Transform dummies = stageTransform.GetChild(a);
				for(int b=0;b<dummies.childCount;++b)
				{
					if(dummies.GetChild(b).name == "start")
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
				
			float d = Vector3.Distance(Collections[i].transform.position, CharacterRef.transform.position + new Vector3(0, 0.2f, 0));
//			Debug.Log(d);
			if(d <= 26)
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

		int newLevel = 0;

		//if(LevelsToComplete.Count > 0)
		//   newLevel = LevelsToComplete[UnityEngine.Random.Range(0, LevelsToComplete.Count)];
		currentLevelId = newLevel;


		for(int i=0;i<Stage.transform.childCount;++i)
		{
			if(Stage.transform.GetChild(i).name.StartsWith("stage"))
			{
				Stage.transform.GetChild(i).gameObject.SetActive(false);
			}
		}

		Stage.transform.GetChild(currentLevelId).gameObject.SetActive(true);


		GameStartDelay = 0.5f;

		int badGuyCounter = 0;
		Transform stageTransform = Stage.transform.GetChild(currentLevelId);

		// BUILD BAD GYES
		for(int a=0;a<stageTransform.childCount;++a)
		{
			if(stageTransform.GetChild(a).name == "dummies")
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
							
						EvilCharacters.Add(EvilCharacterPool[badGuyCounter]);
						badGuyCounter++;

					}
				}
			}
		}

		for(int i=0;i<EvilCharacters.Count;++i)
			EvilCharacters[i].SetActive(false);

		UIGlobalVariablesScript.Singleton.MainCharacterRef.SetActive(false);

		Debug.Log("TOTAL CHILDREN: " + stageTransform.childCount.ToString());
		// BUILD ANIMATION FOR CUBES
		for(int a=0;a<stageTransform.childCount;++a)
		{
			if(stageTransform.GetChild(a).name == "cubes")
			{
				Transform cubes = stageTransform.GetChild(a);
				Debug.Log("CUBES TOTAL: " + cubes.childCount.ToString());

				for(int b=0;b<cubes.childCount;++b)
				{
					CubeAnimatonScript cubeScript = cubes.GetChild(b).gameObject.AddComponent<CubeAnimatonScript>();
					cubeScript.ValueNext = cubeScript.transform.position;
					cubeScript.transform.position = cubeScript.transform.position + new Vector3(0, 400, 0);
					cubeScript.Delay = 0.04f * b;
					GameStartDelay += 0.04f;

				}
			}
		}
	}
}

public class LevelBuilder
{
	public List<Vector2> RaisedCubes = new List<Vector2>();
	public List<Vector2> NormalCubes = new List<Vector2>();
	public List<Vector2> CollectionPoints = new List<Vector2>();
	public List<Vector3[]> EvilCharactersInPatterns = new List<Vector3[]>();

	public void CreateRaisedCubeAt(int x, int z)
	{
		RaisedCubes.Add(new Vector2(x, z));
	}

	public void CreateNormalCubeAt(int x, int z)
	{
		NormalCubes.Add(new Vector2(x, z));
	}

	public void AddCollection(int x, int z)
	{
		CollectionPoints.Add(new Vector2(x, z));
	}

	public void CreateEvilPatternCharacter(Vector3[] points)
	{
		EvilCharactersInPatterns.Add(points);

	}
}
