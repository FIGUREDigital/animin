using UnityEngine;
using System.Collections.Generic;


public class MinigameCollectorScript : MonoBehaviour 
{
	private GameObject[,] CubeMatrix;
	private System.Random Randomizer = new System.Random();

	private List<GameObject> Collections = new List<GameObject>();
	public GameObject CharacterRef;
	private List<AnimationJob> AnimationJobs = new List<AnimationJob>();
	//private List<GameObject> AvailableSpotsToPlaceStars = new List<GameObject>();

	public List<GameObject> EvilCharacters = new List<GameObject>();
	public GameObject[] EvilCharacterPool;


	private int currentLevelId = -1;
	public List<int> CompletedLevels = new List<int>();
	public const int MaxLevels = 4;

	private const int MapWidth = 5;
	private const int MapHeight = 5;

	private List<int> LevelsToComplete = new List<int>();

	//private Vector3 CenterOffset = new Vector3(0.5f, 0, 0.5f);
	//private Vector3 CubeScaleSize = new Vector3(0.2f, 0.20f, 0.2f);
	public GameObject Stage;


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
		CubeMatrix = new GameObject[MapWidth, MapHeight];

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
		}

	

		HardcoreReset();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		UpdateAnimations ();
		CheckForPickupCollision ();
		if(CharacterRef.transform.localPosition.y <= -2)
		{
			CharacterRef.GetComponent<CharacterControllerScript>().IsResetFalling = true;
		}

		if (CharacterRef.transform.localPosition.y <= -5f) 
		{
			CharacterRef.GetComponent<CharacterProgressScript>().StarsOwned -= 3;
			if(CharacterRef.GetComponent<CharacterProgressScript>().StarsOwned < 0 ) CharacterRef.GetComponent<CharacterProgressScript>().StarsOwned = 0;
			Reset();
		}

		UIGlobalVariablesScript.Singleton.TextForStarsInMiniCollector.text = CharacterRef.GetComponent<CharacterProgressScript>().StarsOwned.ToString();
	
	}

	void ResetCharacter()
	{
		CharacterRef.transform.localPosition = Vector3.zero;
		CharacterRef.transform.position = new Vector3(0, 2, 0);
		CharacterRef.GetComponent<CharacterControllerScript>().IsResetFalling = true;

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
			if(EvilCharacters[i] == gameObject.transform.parent.gameObject)
			{
				//Debug.Log("HIT FROM ABOVE");

				gameObject.transform.parent.gameObject.AddComponent<EnemyDeathAnimationScript>();
				EvilCharacterPatternMovementScript movementScript = gameObject.transform.parent.gameObject.GetComponent<EvilCharacterPatternMovementScript>();
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

	private void UpdateAnimations()
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
	}

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
		LevelsToComplete.Add(111);
		LevelsToComplete.Add(112);
		LevelsToComplete.Add(113);
		LevelsToComplete.Add(114);

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
		ResetCharacter();

		for (int i=0; i<Collections.Count; ++i) {
			GameObject.Destroy (Collections [i]);
		}
		Collections.Clear ();

		int newLevel = 999;

		if(LevelsToComplete.Count > 0)
		   newLevel = LevelsToComplete[UnityEngine.Random.Range(0, LevelsToComplete.Count)];
		currentLevelId = newLevel;
	 	LevelBuilder level = RandomizeLevel (MapWidth, MapHeight, newLevel);

		BuildFromLevelBuilder(level);
		Debug.Log("MINIGAME TEST 1");
		for(int i=0;i<EvilCharacters.Count;++i)
		{
			EvilCharacters[i].SetActive(true);
		}
	}

	private LevelBuilder RandomizeLevel(int width, int height, int levelId)
	{
		switch (levelId) 
		{
			case 111:
			{
				LevelBuilder builder = new LevelBuilder();
				
				builder.CreateNormalCubeAt(0, 0);
				builder.CreateNormalCubeAt(0, 1);
				builder.CreateNormalCubeAt(0, 2);
				builder.CreateNormalCubeAt(0, 3);
				builder.CreateNormalCubeAt(0, 4);

				builder.CreateNormalCubeAt(4, 0);
				builder.CreateNormalCubeAt(4, 1);
				builder.CreateNormalCubeAt(4, 2);
				builder.CreateNormalCubeAt(4, 3);
				builder.CreateNormalCubeAt(4, 4);

				builder.CreateNormalCubeAt(1, 0);
				builder.CreateNormalCubeAt(2, 0);
				builder.CreateNormalCubeAt(3, 0);
				builder.CreateNormalCubeAt(1, 4);
				builder.CreateNormalCubeAt(2, 4);
				builder.CreateNormalCubeAt(3, 4);

				builder.CreateNormalCubeAt(2, 2);
				builder.CreateNormalCubeAt(2, 1);
				
				builder.AddCollection(2, 4);
				builder.AddCollection(0, 0);
				builder.AddCollection(0, 4);
				builder.AddCollection(4, 4);
				builder.AddCollection(4, 0);


				builder.CreateEvilPatternCharacter(new Vector3[] { 
				GetPositionForCell(0, 0, 0), 
				GetPositionForCell(4, 0, 0),
				GetPositionForCell(4, 0, 4),
				GetPositionForCell(0, 0, 4),
				GetPositionForCell(0, 0, 0) });

				return builder;
			}

			case 112:
			{
				LevelBuilder builder = new LevelBuilder();
				builder.CreateNormalCubeAt(2, 0);
				builder.CreateNormalCubeAt(2, 1);
				builder.CreateNormalCubeAt(2, 2);
				builder.CreateNormalCubeAt(2, 3);
				builder.CreateNormalCubeAt(2, 4);

				
				builder.AddCollection(2, 0);
				builder.AddCollection(2, 1);
				builder.AddCollection(2, 2);
				builder.AddCollection(2, 3);
				builder.AddCollection(2, 4);
				

				return builder;
			}


			case 113:
			{
				LevelBuilder builder = new LevelBuilder();
				builder.CreateNormalCubeAt(2, 0);
				builder.CreateNormalCubeAt(2, 1);
				builder.CreateNormalCubeAt(2, 2);
				builder.CreateNormalCubeAt(2, 3);
				builder.CreateNormalCubeAt(2, 4);

				builder.CreateNormalCubeAt(0, 1);
				builder.CreateNormalCubeAt(1, 1);
				builder.CreateNormalCubeAt(3, 1);
				builder.CreateNormalCubeAt(4, 1);

				builder.CreateNormalCubeAt(0, 3);
				builder.CreateNormalCubeAt(1, 3);
				builder.CreateNormalCubeAt(3, 3);
				builder.CreateNormalCubeAt(4, 3);
				
				
				builder.AddCollection(2, 0);
				builder.AddCollection(2, 1);
				builder.AddCollection(2, 2);
				builder.AddCollection(2, 3);
				builder.AddCollection(2, 4);


			builder.CreateEvilPatternCharacter(new Vector3[] { 
				GetPositionForCell(0, 0, 1), 
				GetPositionForCell(4, 0, 1),
				GetPositionForCell(0, 0, 1) });

			builder.CreateEvilPatternCharacter(new Vector3[] { 
				GetPositionForCell(0, 0, 3), 
				GetPositionForCell(4, 0, 3),
				GetPositionForCell(0, 0, 3) });
				
				
				return builder;
			}

		case 114:
		{
			LevelBuilder builder = new LevelBuilder();

			builder.CreateNormalCubeAt(0, 0);
			builder.CreateRaisedCubeAt(0, 1);
			builder.CreateNormalCubeAt(0, 2);
			builder.CreateRaisedCubeAt(0, 3);
			builder.CreateNormalCubeAt(0, 4);

			builder.CreateRaisedCubeAt(1, 0);
			builder.CreateNormalCubeAt(1, 1);
			builder.CreateRaisedCubeAt(1, 2);
			builder.CreateNormalCubeAt(1, 3);
			builder.CreateRaisedCubeAt(1, 4);


			builder.CreateNormalCubeAt(2, 0);
			builder.CreateRaisedCubeAt(2, 1);
			builder.CreateNormalCubeAt(2, 2);
			builder.CreateRaisedCubeAt(2, 3);
			builder.CreateNormalCubeAt(2, 4);

			builder.CreateRaisedCubeAt(3, 0);
			builder.CreateNormalCubeAt(3, 1);
			builder.CreateRaisedCubeAt(3, 2);
			builder.CreateNormalCubeAt(3, 3);
			builder.CreateRaisedCubeAt(3, 4);

			builder.CreateNormalCubeAt(4, 0);
			builder.CreateRaisedCubeAt(4, 1);
			builder.CreateNormalCubeAt(4, 2);
			builder.CreateRaisedCubeAt(4, 3);
			builder.CreateNormalCubeAt(4, 4);
			
		
			
			builder.AddCollection(2, 0);
			builder.AddCollection(2, 1);
			builder.AddCollection(2, 2);
			builder.AddCollection(2, 3);
			builder.AddCollection(2, 4);
			
			
		
			
			
			return builder;
		}

		case 999:
		{
			LevelBuilder builder = new LevelBuilder();

			for(int i=0;i<MapHeight;++i)
				for(int j=0;j<MapWidth;++j)
					builder.CreateNormalCubeAt(j, i);

			
			
			return builder;
		}

		}

		return null;
	}

		private Vector3 GetPositionForCell(int x, float height, int z)
	{
		return new Vector3(CubeMatrix[x, z].transform.position.x, height, CubeMatrix[x, z].transform.position.z );// new Vector3 ((x * CubeScaleSize.x) + 0.1f, height, (z* CubeScaleSize.z) + 0.1f) - CenterOffset;
	}


	private void BuildFromLevelBuilder(LevelBuilder builder)
	{
		for (int i=0; i<MapHeight; ++i) {
			for (int j=0; j<MapWidth; ++j) {

				Transform localTransform = CubeMatrix [j, i].transform;

				if(builder.NormalCubes.Contains(new Vector2(j, i)))
				{

					AnimationJobs.Add (new AnimationJob (CubeMatrix [j, i], localTransform.localPosition, new Vector3(localTransform.localPosition.x, 0, localTransform.localPosition.z)));
				}
				else if(builder.RaisedCubes.Contains(new Vector2(j, i)))
				{

					AnimationJobs.Add (new AnimationJob (CubeMatrix [j, i], localTransform.localPosition, new Vector3(localTransform.localPosition.x, 2, localTransform.localPosition.z)));
				}
				else
				{
					AnimationJobs.Add (new AnimationJob (CubeMatrix [j, i], localTransform.localPosition, new Vector3(localTransform.localPosition.x, -30, localTransform.localPosition.z)));
				}
			}
		}

		for(int i=0;i<builder.CollectionPoints.Count;++i)
		{
			GameObject randomParent = CubeMatrix [(int)builder.CollectionPoints[i].x, (int)builder.CollectionPoints[i].y];

			GameObject collection = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			Destroy (collection.rigidbody);
			
			Collections.Add (collection);
			

			//GameObject randomParent = AvailableSpotsToPlaceStars [Randomizer.Next (0, AvailableSpotsToPlaceStars.Count)];
			//AvailableSpotsToPlaceStars.Remove (randomParent);
			
			collection.transform.parent = randomParent.transform;
			collection.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
			collection.transform.localRotation = Quaternion.identity;
			collection.transform.localPosition = new Vector3(-1, 0.3f, -1.0f);
			
			collection.AddComponent<OscillationUpDownScript>();

			SphereCollider colliderToKill = collection.GetComponent<SphereCollider>();
			Destroy(colliderToKill);
			
			/*RaycastHit hitInfo;
		Vector3 origin = collection.transform.localPosition + new Vector3 (0, 20, 0);
		if(Physics.Raycast(origin, Vector3.down, out hitInfo, 25))
		{
			collection.transform.localPosition = hitInfo.distance * origin + new Vector3(0, 0.5f, 0);
			//Debug.Log(collection.transform.localPosition.ToString());
		}*/
			
			//		Debug.Log (collection.transform.localPosition.y.ToString ());
		}


		for(int i=0;i<builder.EvilCharactersInPatterns.Count;++i)
		{
			EvilCharacterPatternMovementScript component = EvilCharacterPool[i].AddComponent<EvilCharacterPatternMovementScript>();
			component.Pattern = builder.EvilCharactersInPatterns[i];
			component.Speed = 20.1f;
			component.Lerp = 0;
			component.Index = 0;
			
			EvilCharacters.Add(EvilCharacterPool[i]);
		}
	}


	/*private void Hide(GameObject gameObject)
	{
		gameObject.SetActive (false);
	}*/
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
