using UnityEngine;
using System;
using System.Collections.Generic;

public class ItemPickupSavedData
{
	public CharacterProgressScript ScriptRef;
	public Vector3 Position;
	public Vector3 Rotation;
	public bool WasInHands;


	public void RevertToThis()
	{
		if(WasInHands)
		{

		}
	}
}


public enum InventoryItemId
{
	None = 0,

	Strawberry,
	Blueberry,
	Spinach,
	AlmondMilk,
	Avocado,
	//Noodles,
	Carrot,
	Chips,
	Toast,
	watermelon,
	//Banana,

	Boombox,
	Clock,
	EDMJuno,
	EDM808,
	EDMKsynth,
	Lightbulb,
	//mintclock,
	paperCalendar,
	woodSword,
	woodFrame,
	Camera,
	FartButton,

	Pill,
	Syringe,
	Plaster,

	Count,
}


public class InventoryItemBankData
{
	public InventoryItemId Id;
	public string PrefabId;
	public string SpriteName;
	public PopupItemType ItemType;
}

public class InventoryItemData
{
	public static InventoryItemBankData[] Items;

	public static void Initialize()
	{
		Items = new InventoryItemBankData[(int)InventoryItemId.Count];
		Items[(int)InventoryItemId.Strawberry] = new InventoryItemBankData() { Id = InventoryItemId.Strawberry, PrefabId = "Prefabs/strawberry2", SpriteName = "strawberry", ItemType = PopupItemType.Food };
		Items[(int)InventoryItemId.Spinach] = new InventoryItemBankData() { Id = InventoryItemId.Spinach, PrefabId = "Prefabs/spinach", SpriteName = "spinach_icon", ItemType = PopupItemType.Food };
		Items[(int)InventoryItemId.Blueberry] = new InventoryItemBankData() { Id = InventoryItemId.Blueberry, PrefabId = "Prefabs/blueberry", SpriteName = "blueberry_icon", ItemType = PopupItemType.Food };
		Items[(int)InventoryItemId.AlmondMilk] = new InventoryItemBankData() { Id = InventoryItemId.AlmondMilk, PrefabId = "Prefabs/almondMilk", SpriteName = "almondmilk_icon", ItemType = PopupItemType.Food };
		Items[(int)InventoryItemId.Avocado] = new InventoryItemBankData() { Id = InventoryItemId.Avocado, PrefabId = "Prefabs/avocado", SpriteName = "avocado_icon", ItemType = PopupItemType.Food };
		//Items[(int)InventoryItemId.Noodles] = new InventoryItemBankData() { Id = InventoryItemId.Blueberry, PrefabId = "Prefabs/blueberry", SpriteName = "blueberry_icon", ItemType = PopupItemType.Food };
		Items[(int)InventoryItemId.Carrot] = new InventoryItemBankData() { Id = InventoryItemId.Carrot, PrefabId = "Prefabs/carrot", SpriteName = "carrot_icon", ItemType = PopupItemType.Food };
		Items[(int)InventoryItemId.Chips] = new InventoryItemBankData() { Id = InventoryItemId.Chips, PrefabId = "Prefabs/chips", SpriteName = "chips_icon", ItemType = PopupItemType.Food };
		Items[(int)InventoryItemId.Toast] = new InventoryItemBankData() { Id = InventoryItemId.Toast, PrefabId = "Prefabs/toast", SpriteName = "jam_icon", ItemType = PopupItemType.Food };
		Items[(int)InventoryItemId.watermelon] = new InventoryItemBankData() { Id = InventoryItemId.watermelon, PrefabId = "Prefabs/watermelon", SpriteName = "watermelon_icon", ItemType = PopupItemType.Food };
		//Items[(int)InventoryItemId.Noodles] = new InventoryItemBankData() { Id = InventoryItemId.Noodles, PrefabId = "Prefabs/Noodles", SpriteName = "blueberry_icon", ItemType = PopupItemType.Food };
		Items[(int)InventoryItemId.Carrot] = new InventoryItemBankData() { Id = InventoryItemId.Carrot, PrefabId = "Prefabs/Carrot", SpriteName = "carrot_icon", ItemType = PopupItemType.Food };
		//Items[(int)InventoryItemId.Banana] = new InventoryItemBankData() { Id = InventoryItemId.Banana, PrefabId = "Prefabs/banana", SpriteName = "banana_icon", ItemType = PopupItemType.Food };

		Items[(int)InventoryItemId.Boombox] = new InventoryItemBankData() { Id = InventoryItemId.Boombox, PrefabId = "Prefabs/boombox", SpriteName = "boombox_icon", ItemType = PopupItemType.Item };
		Items[(int)InventoryItemId.Clock] = new InventoryItemBankData() { Id = InventoryItemId.Clock, PrefabId = "Prefabs/mintclock", SpriteName = "mintclock_icon", ItemType = PopupItemType.Item };
		Items[(int)InventoryItemId.EDMJuno] = new InventoryItemBankData() { Id = InventoryItemId.EDMJuno, PrefabId = "Prefabs/juno", SpriteName = "juno_icon", ItemType = PopupItemType.Item };
		Items[(int)InventoryItemId.EDM808] = new InventoryItemBankData() { Id = InventoryItemId.EDM808, PrefabId = "Prefabs/808", SpriteName = "808_icon", ItemType = PopupItemType.Item };
		Items[(int)InventoryItemId.EDMKsynth] = new InventoryItemBankData() { Id = InventoryItemId.EDMKsynth, PrefabId = "Prefabs/ksynth", SpriteName = "ksynth_icon", ItemType = PopupItemType.Item };
		Items[(int)InventoryItemId.Lightbulb] = new InventoryItemBankData() { Id = InventoryItemId.Lightbulb, PrefabId = "Prefabs/Lightbulb", SpriteName = "lightbulb_icon", ItemType = PopupItemType.Item };
		//Items[(int)InventoryItemId.mintclock] = new InventoryItemBankData() { Id = InventoryItemId.mintclock, PrefabId = "Prefabs/mintclock", SpriteName = "mintclock_icon", ItemType = PopupItemType.Item };
		Items[(int)InventoryItemId.paperCalendar] = new InventoryItemBankData() { Id = InventoryItemId.paperCalendar, PrefabId = "Prefabs/paperCalendar", SpriteName = "calendar_icon", ItemType = PopupItemType.Item };
		Items[(int)InventoryItemId.Camera] = new InventoryItemBankData() { Id = InventoryItemId.Camera, PrefabId = "Prefabs/camera", SpriteName = "camera_icon", ItemType = PopupItemType.Item };
		Items[(int)InventoryItemId.FartButton] = new InventoryItemBankData() { Id = InventoryItemId.FartButton, PrefabId = "Prefabs/fartbutton", SpriteName = "fartbutton_icon", ItemType = PopupItemType.Item };

		Items[(int)InventoryItemId.woodSword] = new InventoryItemBankData() { Id = InventoryItemId.woodSword, PrefabId = "Prefabs/woodSword", SpriteName = "woodSword_icon", ItemType = PopupItemType.Item };
		Items[(int)InventoryItemId.woodFrame] = new InventoryItemBankData() { Id = InventoryItemId.woodFrame, PrefabId = "Prefabs/woodFrame", SpriteName = "woodFrame_icon", ItemType = PopupItemType.Item };


		Items[(int)InventoryItemId.Pill] = new InventoryItemBankData() { Id = InventoryItemId.Pill, PrefabId = "Prefabs/capsule", SpriteName = "mediumpill_icon", ItemType = PopupItemType.Medicine };
		Items[(int)InventoryItemId.Plaster] = new InventoryItemBankData() { Id = InventoryItemId.Plaster, PrefabId = "Prefabs/plaster", SpriteName = "plaster_icon", ItemType = PopupItemType.Medicine };
	}

	public InventoryItemId Id;
	public int Count;
}

public enum AniminSubevolutionStageId
{
	First = 0,
	Second,
	Third,
	Fourth,
	Fifth,
	Sixth,
	Seventh,
	Eighth,

	Count,
}

public class AniminSubevolutionStageData
{
	public static float[] Stages = new float[] { 10, 20, 30, 40, 50, 60, 70, 80};

}

public class HappyStateRange
{
	public static HappyStateRange[] HappyStates = new HappyStateRange[] 
	{
		new HappyStateRange() { Min = 0, Max = 14, Id = AnimationHappyId.Sad4 },
		new HappyStateRange() { Min = 8, Max = 26 , Id = AnimationHappyId.Sad3},
		new HappyStateRange() { Min = 20, Max = 38, Id = AnimationHappyId.Sad2 },
		new HappyStateRange() { Min = 32, Max = 50, Id = AnimationHappyId.Sad1 },
		new HappyStateRange() { Min = 45, Max = 65, Id = AnimationHappyId.Happy1 },
		new HappyStateRange() { Min = 60, Max = 80, Id = AnimationHappyId.Happy2 },
		new HappyStateRange() { Min = 75, Max = 95 , Id = AnimationHappyId.Happy3},
		new HappyStateRange() { Min = 90, Max = 110, Id = AnimationHappyId.Happy4 },
		new HappyStateRange() { Min = 105, Max = 125, Id = AnimationHappyId.Happy5 },
	};

	public float Min;
	public float Max;
	public AnimationHappyId Id;
}


public class CharacterProgressScript : MonoBehaviour 
{
	//public List<AchievementId> Achievements = new List<AchievementId>();
	public DateTime NextHappynBonusTimeAt;
	public DateTime LastSavePerformed;
	public DateTime LastTimeToilet;

	private bool IsDetectingSwipeRight;
	private int SwipesDetectedCount;
	private bool AtLeastOneSwipeDetected;
	private List<GameObject> TouchesObjcesWhileSwiping = new List<GameObject>();
	//private Vector3 lastMousePosition;

	private List<GameObject> groundItemsOnARscene = new List<GameObject>();
	private List<GameObject> groundItemsOnNonARScene = new List<GameObject>();

	public List<GameObject> GroundItems 
	{
		get
		{
			if(UIGlobalVariablesScript.Singleton.ARSceneRef.activeInHierarchy)
			{
				return groundItemsOnARscene;
			}
			else
			{
				return groundItemsOnNonARScene;
			}
		}
	}

	public GameObject ActiveWorld
	{
		get
		{
			if(UIGlobalVariablesScript.Singleton.ARSceneRef.activeInHierarchy)
				return UIGlobalVariablesScript.Singleton.ARWorldRef;
			else
				return UIGlobalVariablesScript.Singleton.NonARWorldRef;
		}
	}

	public GameObject PooPrefab;
	public GameObject PissPrefab;

	private Vector3 DestinationLocation;

	//public TextMesh TextTest;
	public AnimationControllerScript animationController;
	public bool IsMovingTowardsLocation;
	//public GameObject ObjectCarryAttachmentBone;
	private GameObject DragableObject;
	public GameObject ObjectHolding;
	private float LastTapClick;

	private int RequestedToMoveToCounter;
	private float RequestedTime;

	private bool RequestedToMoveTo;
	private bool ShouldDragIfMouseMoves;
	private Vector3 MousePositionAtDragIfMouseMoves;
	public GameObject IsGoingToPickUpObject;
	public bool InteractWithItemOnPickup;
	public bool ShouldThrowObjectAfterPickup;
	public bool DragedObjectedFromUIToWorld;
	public ActionId CurrentAction;

	public GameObject SleepBoundingBox;

	public GameObject GroundPlane;

	ItemPickupSavedData pickupItemSavedData = new ItemPickupSavedData();
	RaycastHit moveHitInfo;
	RaycastHit detectDragHit;
	ActionId lastActionId;
	bool hadButtonDownLastFrame;
	bool IsDetectingMouseMoveForDrag;
	bool IsDetectFlick;
	float FeedMyselfTimer;
	public bool HadUITouchLastFrame;
	private GameObject LastKnownObjectWithMenuUp;
	public const float ConsideredHungryLevels = 70;

	//float TimeForNextHungryUnwellSadAnimation;
	//float LengthOfHungryUnwellSadAnimation;
	//HungrySadUnwellLoopId sadUnwellLoopState;
	float HoldingLeftButtonDownTimer;
	private bool TriggeredHoldAction;
	private List<Vector3> SwipeHistoryPositions = new List<Vector3>();
	public float PortalTimer;
	public float SmallCooldownTimer;

	// Use this for initialization
	void Awake () 
	{
		LastSavePerformed = DateTime.Now;
		LastTimeToilet = DateTime.Now;

		InventoryItemData.Initialize();

	
		//PersistentData.Singleton.SetDefault();
		//PersistentData.Singleton.Load();

		if(PlayerProfileData.ActiveProfile == null)
		{
			PlayerProfileData.ActiveProfile = PlayerProfileData.CreateNewProfile("buildintest");
            PersistentData.Singleton = PlayerProfileData.ActiveProfile.Characters[(int)PersistentData.TypesOfAnimin.Tbo];
		}





		//TextTest.color = new Color(1, 1, 1, 0.0f);

		Physics.IgnoreLayerCollision(
			LayerMask.NameToLayer( "IgnoreCollisionWithCharacter"),  
			LayerMask.NameToLayer( "Character"));


		CurrentAction = ActionId.EnterSleep;


		animationController = GetComponent<AnimationControllerScript>();
		//animationController.IsSleeping = true;
		//CurrentAction = ActionId.Sleep;
		//SleepBoundingBox.SetActive(true);
	}

	void Start()
	{	
		GetComponent<CharacterControllerScript>().SetLocal(true);
		UIClickButtonMasterScript.SetSoundSprite();


        //HARRY: REMEMEBR TO REMOVE THESE COMMENTS, FOR CHRIST'S SAKE.
		this.GetComponent<CharacterSwapManagementScript>().LoadCharacter(PersistentData.Singleton.PlayerAniminId, PersistentData.Singleton.AniminEvolutionId);
        //this.GetComponent<CharacterSwapManagementScript>().LoadCharacter(AniminId.Mandi   , AniminEvolutionStageId.Adult);
        
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.AlmondMilk, 3);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.Avocado, 1);
		//AddItemToInventory(InventoryItemId.Banana, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.Blueberry, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.Boombox, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.Camera, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.Carrot, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.Chips, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.Clock, 4);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.EDM808, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.EDMJuno, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.EDMKsynth, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.FartButton, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.Lightbulb, 1);
		//AddItemToInventory(InventoryItemId.mintclock, 1);
		//AddItemToInventory(InventoryItemId.Noodles, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.paperCalendar, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.Pill, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.Plaster, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.Spinach, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.Strawberry, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.Syringe, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.Toast, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.watermelon, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.woodFrame, 1);
		PersistentData.Singleton.AddItemToInventory(InventoryItemId.woodSword, 1);

		//Adrian, please forgive me, but I couldn't think of where else in the labrynth to put this little UI update thingymaboodle.

		//End Forgiveness


        if (BetweenSceneData.Instance.ReturnFromMiniGame)
        {
            if (BetweenSceneData.Instance.Points >= 0)
            {
                if (BetweenSceneData.Instance.Points >= 10000)
                    AchievementsScript.Singleton.Show(AchievementTypeId.Gold, BetweenSceneData.Instance.Points);
                else if (BetweenSceneData.Instance.Points >= 5000)
                    AchievementsScript.Singleton.Show(AchievementTypeId.Silver, BetweenSceneData.Instance.Points);
                else
                    AchievementsScript.Singleton.Show(AchievementTypeId.Bronze, BetweenSceneData.Instance.Points);
                BetweenSceneData.Instance.ResetPoints();
            }
		    SpawnChests();
            exitSleep();
        }
	}



	void OnApplicationPause(bool pauseStatus)
	{
		//Debug.Log("pauseStatus:" + pauseStatus.ToString());
		if(pauseStatus)
		{
			Stop(true);
			//CurrentAction = ActionId.EnterSleep;
		}
		else
		{
			//Stop(true);
		}
	}

	public GameObject SpawnStageItem(string prefabId, Vector3 position)
	{
		//Argi, you're a cunt.
		CharacterProgressScript script = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>();
		
		GameObject resource = Resources.Load<GameObject>(prefabId);
		
		GameObject gameObject = GameObject.Instantiate(resource) as GameObject;
		gameObject.transform.parent = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().ActiveWorld.transform;
		
		gameObject.transform.localPosition = position;
		//gameObject.transform.localRotation = Quaternion.Euler(0, UnityEngine.Random.Range(-180, 180), 0);

		UIPopupItemScript scriptRef = gameObject.GetComponent<UIPopupItemScript> ();

		float scale = 0.1f;
		gameObject.transform.localScale = new Vector3 (scale, scale, scale);
		gameObject.transform.localRotation = Quaternion.Euler(0, UnityEngine.Random.Range(130, 230), 0);

		script.GroundItems.Add(gameObject);
		return gameObject;
	}

	public GameObject SpawnZef(Vector3 position)
	{
		CharacterProgressScript script = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>();

		GameObject resource = Resources.Load<GameObject>(@"Prefabs/ZefToken");
		
		GameObject gameObject = GameObject.Instantiate(resource) as GameObject;
		gameObject.transform.parent = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().ActiveWorld.transform;
		
		gameObject.transform.localPosition = position;
		//gameObject.transform.localRotation = Quaternion.Euler(0, UnityEngine.Random.Range(-180, 180), 0);
		
		script.GroundItems.Add(gameObject);
		return gameObject;
	}


	public GameObject GetRandomItem()
	{
		List<GameObject> list = new List<GameObject>();
		
		for(int i=0;i<GroundItems.Count;++i)
		{
			//if(GroundItems[i].GetComponent<ReferencedObjectScript>() == null) continue;
			
			UIPopupItemScript itemData = GroundItems[i]/*.GetComponent<ReferencedObjectScript>().Reference*/.GetComponent<UIPopupItemScript>();
			if(itemData == null) continue;

			if(itemData.Type == PopupItemType.Item)
			{
				list.Add(GroundItems[i]);
			}
		}

		if(list.Count > 0)
			return list[UnityEngine.Random.Range(0, list.Count)];
		else
			return null;
	}

	public void ThrowItemFromHands(Vector3 throwdirection)
	{
		//DragableObject = ObjectHolding;
		animationController.IsHoldingItem = false;

		this.gameObject.GetComponent<CharacterControllerScript>().RotateToLookAtPoint(this.transform.position + throwdirection * 50);
		
		
		//pickupItemSavedData.Position = DragableObject.transform.position;
		//pickupItemSavedData.Rotation = DragableObject.transform.rotation.eulerAngles;
		
		ObjectHolding.transform.parent = ActiveWorld.transform;

		
		GetComponent<CharacterSwapManagementScript> ().CurrentModel.GetComponent<HeadReferenceScript> ().HoldingObject = null;
		
		ThrowAnimationScript throwScript = ObjectHolding.AddComponent<ThrowAnimationScript>();
		float maxDistance = Vector3.Distance(Input.mousePosition, MousePositionAtDragIfMouseMoves) * 0.35f;
		if(maxDistance >= 160) maxDistance = 160;
		
		throwScript.Begin(throwdirection, maxDistance);

		UIGlobalVariablesScript.Singleton.SoundEngine.Play(PersistentData.Singleton.PlayerAniminId, PersistentData.Singleton.AniminEvolutionId, CreatureSoundId.Throw);
		//ObjectHolding.transform.position = this.transform.position;
		
		
		
		
		GroundItems.Add(ObjectHolding);
		
		ObjectHolding.transform.rotation = Quaternion.Euler(0, ObjectHolding.transform.rotation.eulerAngles.y, 0);
		pickupItemSavedData.WasInHands = true;
		animationController.IsThrowing = true;
		IsDetectFlick = false;
		ObjectHolding = null;
		//CurrentAction = ActionId.None;
	}

	public void HidePopupMenus()
	{
		UIGlobalVariablesScript.Singleton.AlarmUI.SetActive(false);
		UIGlobalVariablesScript.Singleton.StereoUI.SetActive(false);
		UIGlobalVariablesScript.Singleton.LightbulbUI.SetActive(false);
		UIGlobalVariablesScript.Singleton.EDMBoxUI.SetActive(false);
		UIGlobalVariablesScript.Singleton.JunoUI.SetActive(false);
		UIGlobalVariablesScript.Singleton.PianoUI.SetActive(false);
	}
	
	private GameObject GetClosestFoodToEat()
	{
		GameObject closestFood = null;

		//Debug.Log("GROUND ITEMs: " + GroundItems.Count.ToString());

		for(int i=0;i<GroundItems.Count;++i)
		{
			if(GroundItems[i].GetComponent<ReferencedObjectScript>() == null) continue;

			UIPopupItemScript itemData = GroundItems[i].GetComponent<ReferencedObjectScript>().Reference.GetComponent<UIPopupItemScript>();
			if(itemData.Type == PopupItemType.Food)
			{
				if(closestFood == null)
				{
					closestFood = GroundItems[i];
				}
				else
				{
					if(Vector3.Distance(this.transform.position, GroundItems[i].transform.position)  < Vector3.Distance(this.transform.position, closestFood.transform.position))
					{
						closestFood = GroundItems[i];
					}
				}
			}
		}

		return closestFood;
	}

	public void SpawnChests()
	{
		Debug.Log ("Spawn Chests");
		GameObject resource = Resources.Load<GameObject>("Prefabs/chest_gold");

		GameObject instance = Instantiate(resource) as GameObject;
		instance.transform.parent = ActiveWorld.transform;
		instance.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(170, 190), 0);
		instance.transform.position = Vector3.zero;
		instance.transform.localPosition = new Vector3(UnityEngine.Random.Range(-0.67f, 0.67f), this.transform.localPosition.y, UnityEngine.Random.Range(-0.67f, 0.67f));
		instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
	}

    public void AnimateJumpOutOfPortal()
    {
        //Harry copied this from OnTrackingLost
        CharacterProgressScript progressScript = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>();

        UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimationControllerScript>().IsExitPortal = true;
        UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.rotation = Quaternion.Euler(0, 180, 0);
        UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterControllerScript>().ResetRotation();

        UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimateCharacterOutPortalScript>().Timer = 0;
        UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimateCharacterOutPortalScript>().JumbId = AnimateCharacterOutPortalScript.JumbStateId.Jumbout;
        UIGlobalVariablesScript.Singleton.SoundEngine.Play(PersistentData.Singleton.PlayerAniminId, PersistentData.Singleton.AniminEvolutionId, CreatureSoundId.JumbOutPortal);
        //UIGlobalVariablesScript.Singleton.ARPortal.GetComponent<PortalScript>().Show(true);
        progressScript.CurrentAction = ActionId.SmallCooldownPeriod;
        progressScript.SmallCooldownTimer = 0.5f;

        //End Guesswork
    }

	private float EatAlphaTimer;
	private bool PlayedEatingSound;

    public Color ShaderColor
    {
        get
        {
            //For simplicity's sake this just returns the first colour it can find. However, because the set method changes all materials, it should all be the same. Regardless, let that be known.
            return this.GetComponent<CharacterSwapManagementScript>().CurrentModel.GetComponentInChildren<SkinnedMeshRenderer>().material.color;
        }
        set
        {
            SkinnedMeshRenderer[] Meshes = this.GetComponent<CharacterSwapManagementScript>().CurrentModel.GetComponentsInChildren<SkinnedMeshRenderer>();
            for (int i = 0; i < Meshes.Length; i++)
            {
                Meshes[i].material.color = value;
            }
        }
    }
	
	// Update is called once per frame
	void Update () 
	{

		EvolutionManager.Instance.UpdateEvo();


		PersistentData.Singleton.Hungry -= Time.deltaTime * 0.2f;
		PersistentData.Singleton.Fitness -= Time.deltaTime * 0.2f;
		PersistentData.Singleton.Health -= Time.deltaTime * 0.2f;
	
		//TextTest.color = new Color(1,1,1, TextTest.color.a - Time.deltaTime * 0.6f);
		//if(TextTest.color.a < 0)
		//	TextTest.color = new Color(1,1,1, 0);


		PersistentData.Singleton.Happy = ((
			(PersistentData.Singleton.Hungry / 100.0f) + 
			(PersistentData.Singleton.Fitness / 100.0f) + 
			(PersistentData.Singleton.Health / 100.0f)) 
		         / 3.0f) 
			* PersistentData.MaxHappy;


		//Debug.Log("Hungry: " + (Hungry / 100.0f).ToString());
		//Debug.Log("Fitness: " + (Fitness / 100.0f).ToString());
		//Debug.Log("Health: " + (Health / 100.0f).ToString());
		//Debug.Log("Happy: " + (Happy / MaxHappy).ToString());
		// EVOLUTION BAR
//		{
//			//Evolution += (Happy / MaxHappy) * Time.deltaTime * 0.1f;
//			//if(Evolution >= 100) Evolution = 100;
//
			float percentage = PersistentData.Singleton.Evolution;
			UITexture EvoProgress = UIGlobalVariablesScript.Singleton.EvolutionProgressSprite;
			float scale = UIGlobalVariablesScript.Singleton.gameObject.transform.localScale.x;
			int screenWidth = 1330;
			int width = (int)(screenWidth * percentage);
			Vector3 pos = EvoProgress.transform.position;
			pos.x = ((-screenWidth + width) * 0.5f) * scale;
			pos.y = 275.0f * scale;
			EvoProgress.transform.position = pos;
			EvoProgress.width = width;
			EvoProgress.uvRect = new Rect(0, 0, percentage, 1);
			EvoProgress.MarkAsChanged();
//
//
//			//UIGlobalVariablesScript.Singleton.EvolutionProgressSprite.width = (int)(1330.0f * (Evolution / 100.0f));
//			
//			if(NextHappynBonusTimeAt >= DateTime.Now)
//			{
//				// do bonus of happyness
//			}
//		}

		GameObject indicator = GetComponent<CharacterSwapManagementScript>().CurrentModel.GetComponent<HeadReferenceScript>().Indicator;

		if(ObjectHolding != null || animationController.IsSleeping == true)
		{
			indicator.SetActive(true);
		}
		else
		{
			indicator.SetActive(false);
		}

		bool hadUItouch = false;

		// CHECK FOR UI TOUCH
		if(Input.GetButton("Fire1") || Input.GetButtonDown("Fire1") || Input.GetButtonUp("Fire1"))
		{
			// This grabs the camera attached to the NGUI UI_Root object.
			Camera nguiCam = UICamera.mainCamera;
			
			if( nguiCam != null )
			{
				// pos is the Vector3 representing the screen position of the input
				Ray inputRay = nguiCam.ScreenPointToRay( Input.mousePosition );    
				RaycastHit hit;

				if (Physics.Raycast(inputRay, out hit))
				{
					//Debug.Log("TOUCH: " + hit.collider.gameObject.layer.ToString());

					//Debug.Log("normalUIRAY:" + hit.collider.gameObject);
					if(hit.collider.gameObject.layer == LayerMask.NameToLayer( "NGUI" ))
					{
						//Debug.Log("normalUIRAY +++++:" + hit.collider.gameObject);
						hadUItouch = true;
						//Debug.Log("UI TOUCH");
					}

				}
			}
		}


		RaycastHit hitInfo;
		bool hadRayCollision = false;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hitInfo))
		{
			hadRayCollision = true;
			//Debug.Log ("Ray Collision : ["+hitInfo.collider.gameObject.name+"];");
		}



		//Debug.Log(CurrentAction.ToString());
		switch(CurrentAction)
		{
		case ActionId.SmallCooldownPeriod:
		{
			SmallCooldownTimer -= Time.deltaTime;
			if(SmallCooldownTimer <= 0)
			{
				CurrentAction = ActionId.None;
			}

			break;
		}

		case ActionId.ThrowItemAfterPickup:
		{
			if(animationController.IsHoldingItemComplete)
			{
				Vector3 throwdirection = Vector3.Normalize(new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0, UnityEngine.Random.Range(-1.0f, 1.0f) ));
				ThrowItemFromHands(throwdirection);
				CurrentAction = ActionId.None;
			}


			break;
		}

		case ActionId.EatItem:
		{

			CurrentAction = ActionId.WaitEatingFinish;
			animationController.IsHoldingItem = false;
			animationController.IsEating = true;
			EatAlphaTimer = 0;

			PlayedEatingSound = false;
			break;
		}
		case ActionId.WaitEatingFinish:
		{
			if(!animationController.IsEating)
			{

				PopupItemType itemType = ObjectHolding./*GetComponent<ReferencedObjectScript>().Reference.*/GetComponent<UIPopupItemScript>().Type;

				Debug.Log("FINISHED EATING");

				OnInteractWithPopupItem(ObjectHolding./*GetComponent<ReferencedObjectScript>().Reference.*/GetComponent<UIPopupItemScript>());
				this.GetComponent<CharacterProgressScript>().GroundItems.Remove(ObjectHolding);
				Destroy(ObjectHolding);
				

				pickupItemSavedData.WasInHands = true;
				ObjectHolding = null;
				CurrentAction = ActionId.None;
			}
			else
			{
				EatAlphaTimer += Time.deltaTime;

				if(EatAlphaTimer >= 0.7f)
				{
					if(!PlayedEatingSound)
					{
						UIPopupItemScript popup = ObjectHolding/*.GetComponent<ReferencedObjectScript>().Reference*/.GetComponent<UIPopupItemScript>();

						PlayedEatingSound = true;
						if(popup.SpecialId == SpecialFunctionalityId.Liquid)
							UIGlobalVariablesScript.Singleton.SoundEngine.Play(PersistentData.Singleton.PlayerAniminId, PersistentData.Singleton.AniminEvolutionId, CreatureSoundId.FeedDrink);
						else
							UIGlobalVariablesScript.Singleton.SoundEngine.Play(PersistentData.Singleton.PlayerAniminId, PersistentData.Singleton.AniminEvolutionId, CreatureSoundId.FeedFood);

					}
				}

				if(EatAlphaTimer >= 1)
				{
					if(ObjectHolding.renderer != null) 
					{
						ObjectHolding.renderer.material.shader = Shader.Find("Custom/ItemShader");

						float alpha = ObjectHolding.renderer.material.color.a;
						alpha -= Time.deltaTime *  3;
						if(alpha <= 0) alpha = 0;
						ObjectHolding.renderer.material.color = new Color(
							ObjectHolding.renderer.material.color.r,
							ObjectHolding.renderer.material.color.g,
							ObjectHolding.renderer.material.color.b,
							alpha);
					}

					for(int a=0;a<ObjectHolding.transform.childCount;++a)
					{
						if(ObjectHolding.transform.GetChild(a).renderer == null) continue;

						ObjectHolding.transform.GetChild(a).renderer.material.shader = Shader.Find("Custom/ItemShader");
						
						float alpha = ObjectHolding.transform.GetChild(a).renderer.material.color.a;
						alpha -= Time.deltaTime *  3;
						if(alpha <= 0) alpha = 0;
						ObjectHolding.transform.GetChild(a).renderer.material.color = new Color(
							ObjectHolding.transform.GetChild(a).renderer.material.color.r,
							ObjectHolding.transform.GetChild(a).renderer.material.color.g,
							ObjectHolding.transform.GetChild(a).renderer.material.color.b,
							alpha);
					}
				}

			}

			break;
		}


		case ActionId.ExitPortalMainStage:
		{
			Debug.Log ("ExitPortalmainStage");
			SpawnChests();	
			
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimateCharacterOutPortalScript>().Timer = 0;
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimateCharacterOutPortalScript>().JumbId = AnimateCharacterOutPortalScript.JumbStateId.Jumbout;

			if(UIGlobalVariablesScript.Singleton.ARSceneRef.activeInHierarchy)
				UIGlobalVariablesScript.Singleton.ARPortal.GetComponent<PortalScript>().Show(PortalStageId.ARscene, false);
			else
				UIGlobalVariablesScript.Singleton.ARPortal.GetComponent<PortalScript>().Show(PortalStageId.NonARScene, false);
			
			UIGlobalVariablesScript.Singleton.SoundEngine.Play(PersistentData.Singleton.PlayerAniminId, PersistentData.Singleton.AniminEvolutionId, CreatureSoundId.JumbOutPortal);

			animationController.IsExitPortal = true;

			CurrentAction = ActionId.SmallCooldownPeriod;
			SmallCooldownTimer = 0.5f;

			break;
		}



		case ActionId.EnterPortalToAR:
        {
            const float StopAt = 0.5f;
            const float StartFade = 0.2f;
				//OnEnterARScene();
				PortalTimer += Time.deltaTime;

                //Debug.Log("Portal Timer : [" + PortalTimer + "];");

                if (PortalTimer >= StopAt)
                {
                    //[PTM1] Harry's Mine: Timer to transition to AR Scene
                    //Debug.Log("EnterPortalToAR finished");
                    CurrentAction = ActionId.None;

                    UIGlobalVariablesScript.Singleton.NonSceneRef.SetActive(false);
                    UIGlobalVariablesScript.Singleton.ARSceneRef.SetActive(true);
                    UIGlobalVariablesScript.Singleton.Vuforia.OnCharacterEnterARScene();


                    //UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimateCharacterOutPortalScript>().Timer = 0;
                    //UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimateCharacterOutPortalScript>().JumbId = AnimateCharacterOutPortalScript.JumbStateId.Jumbout;

                    UIGlobalVariablesScript.Singleton.ARPortal.GetComponent<PortalScript>().Show(PortalStageId.ARscene, false);

                    UIGlobalVariablesScript.Singleton.SoundEngine.Play(PersistentData.Singleton.PlayerAniminId, PersistentData.Singleton.AniminEvolutionId, CreatureSoundId.JumbOutPortal);

                    ShaderColor = Color.white;
                }
                else if (PortalTimer >= StartFade)
                {
                    /*
                    Color c = ShaderColor;

                    float diff = PortalTimer * (1 / (StopAt - StartFade));
                    float alpha = 1 - diff;
                    c.a = alpha;


                    Debug.Log("Diff : [" + diff + "]; Alpha : [" + alpha + "];");

                    ShaderColor = c;
                     */
                    ShaderColor = Color.Lerp(ShaderColor, Color.clear, 0.5f);
                }

				break;
			}

			/*case ActionId.EnterPortalToNonAR:
			{
				PortalTimer += Time.deltaTime;
			
				if(PortalTimer >= 1.10f)
				{
					Debug.Log("EnterPortalToNonAR finished");
					CurrentAction = ActionId.None;
					UIGlobalVariablesScript.Singleton.Vuforia.OnExitAR();
					UIGlobalVariablesScript.Singleton.ARPortal.GetComponent<PortalScript>().Show(false);
				}
				
				break;
			}
*/


			case ActionId.EnterSleep:
			{
				animationController.IsSleeping = true;
				CurrentAction = ActionId.Sleep;
				SleepBoundingBox.SetActive(true);
			UIGlobalVariablesScript.Singleton.SoundEngine.PlayLoop(PersistentData.Singleton.PlayerAniminId, PersistentData.Singleton.AniminEvolutionId, CreatureSoundId.SnoringSleeping);

				break;
			}

			case ActionId.Sleep:
			{
				if( Input.GetButtonUp("Fire1") )
				{
					if (!hadUItouch && hadRayCollision && hitInfo.collider.gameObject == SleepBoundingBox)
					{
					exitSleep();
						
					}
				}

				break;
			}
			
			case ActionId.None:
			{
				//Debug.Log("INSIDE NONE");

				if(Input.GetButton("Fire1"))
					HoldingLeftButtonDownTimer += Time.deltaTime;
				else
					HoldingLeftButtonDownTimer = 0;

							
				if(lastActionId != ActionId.None)
				{
					//Debug.Log("if(lastActionId != ActionId.None)");
				}
			   
				else if(HadUITouchLastFrame || hadUItouch || DragedObjectedFromUIToWorld)
				{
					//Debug.Log("UI TOUCH");

					break;
				}
					
				else if(Input.GetButtonDown("Fire1"))
				{
					//Debug.Log("else if(Input.GetButtonDown(Fire1))");

					if(hadRayCollision)
					{
						if(hitInfo.collider.gameObject == ObjectHolding || hitInfo.collider.gameObject == this.gameObject)
						{
							IsDetectFlick = true;
							//CurrentAction = ActionId.DetectFlickAndThrow;
							MousePositionAtDragIfMouseMoves = Input.mousePosition;
						}
					else if(hitInfo.collider.tag == "Items" && hitInfo.collider.transform)
						{
							IsDetectingMouseMoveForDrag = true;
							//CurrentAction = ActionId.DetectMouseMoveAndDrag;
							MousePositionAtDragIfMouseMoves = Input.mousePosition;
							detectDragHit = hitInfo;
							//Debug.Log("DetectMouseMoveAndDrag");
						}
					}

				}
				/*else if(HoldingLeftButtonDownTimer >= 0.70f && Input.GetButton("Fire1") && hadRayCollision && hitInfo.collider.tag == "Items" && (hitInfo.collider.GetComponent<ReferencedObjectScript>().Reference.GetComponent<UIPopupItemScript>().Menu != MenuFunctionalityUI.None))
				{


					if(hitInfo.collider.GetComponent<ReferencedObjectScript>().Reference.GetComponent<UIPopupItemScript>().Menu == MenuFunctionalityUI.Clock)
					{
						UIGlobalVariablesScript.Singleton.Item3DPopupMenu.GetComponent<UIWidget>().SetAnchor(hitInfo.collider.gameObject);
						TriggeredHoldAction = true;
						UIGlobalVariablesScript.Singleton.Item3DPopupMenu.SetActive(true);
					}
				

				}*/
				else if(IsDetectFlick && !Input.GetButton("Fire1") && (Vector3.Distance(Input.mousePosition, MousePositionAtDragIfMouseMoves)> 25) && ObjectHolding != null)
				{
					//Debug.Log("IsDetectFlick");

					Vector3 throwdirection = Vector3.Normalize(Input.mousePosition - MousePositionAtDragIfMouseMoves);
					throwdirection.z = throwdirection.y;
					throwdirection.y = 0;

					ThrowItemFromHands(throwdirection);

				}
				else if(IsDetectingMouseMoveForDrag && Vector3.Distance(Input.mousePosition, MousePositionAtDragIfMouseMoves) >= 5 && Input.GetButton("Fire1"))
				{
					//Debug.Log("IsDetectingMouseMoveForDrag");

				#region Some Commented Out Code
					
					// DRAG ITEM FROM CHARACTER AWAY FROM HIM
					/*if(hit.collider.name.StartsWith("MainCharacter") && DragableObject == null && animationController.IsHoldingItem)
					{
						//pickupItemSavedData.Position = DragableObject.transform.position;
						//pickupItemSavedData.Rotation = DragableObject.transform.rotation.eulerAngles;
						//pickupItemSavedData.WasInHands = true;

						//Debug.Log("SELECTED OBJECT:" + hit.collider.name);
						//DragableObject = ObjectHolding;
						//DragableObject.GetComponent<BoxCollider>().enabled = false;
						DragableObject.transform.parent = this.transform.parent;
						animationController.IsHoldingItem = false;
						animationController.IsThrowing = true;
						//Physics.IgnoreCollision(DragableObject.collider, this.collider, true);


					}
					
					// GRAB ITEM ITSELF EITHER FROM HANDS OR FLOOR
					else*/ 
					
					// THROW IT AWAY
					//if(animationController.IsHoldingItem)
					//{
					//	CurrentAction = ActionId.DetectFlickAndThrow;
					//}
					
					// GRAB FROM FLOOR
					//else
				#endregion
					

					DragableObject = detectDragHit.collider.gameObject;
					
					if (DragableObject.GetComponent<UIPopupItemScript>().Type != PopupItemType.Token){

						pickupItemSavedData.WasInHands = false;
						//Debug.Log("IT SHOULD GO AND DRAG NOW");
						pickupItemSavedData.WasInHands = false;
						
						pickupItemSavedData.Position = DragableObject.transform.position;
						pickupItemSavedData.Rotation = DragableObject.transform.rotation.eulerAngles;
						//Physics.IgnoreCollision(DragableObject.collider, this.collider, true);
						//Debug.Log("DISABLING COLLISION");
						
						CurrentAction = ActionId.DragItemAround;
						IsDetectingMouseMoveForDrag = false;

						DragableObject.layer = LayerMask.NameToLayer("IgnoreCollisionWithCharacter");
						DragableObject.GetComponent<BoxCollider>().enabled = false;
					}

				}
				else if(Input.GetButton("Fire1"))
				{


					//Debug.Log("DETECT SWIPE MECHANISM");

					/*if(SwipeHistoryPositions.Count == 0)
					{
						SwipeHistoryPositions.Add(Input.mousePosition);
					}
					else
					{*/
//						Debug.Log("DIFFERENCE: " + (Input.mousePosition.x - SwipeHistoryPositions[SwipeHistoryPositions.Count - 1].x).ToString());

						bool triedOnce = false;
						if(IsDetectingSwipeRight || SwipesDetectedCount == 0)
						{
							bool hadEnoughMovement = false;
							for(int i=0;i<SwipeHistoryPositions.Count;++i)
								if((Input.mousePosition.x - SwipeHistoryPositions[i].x) >= 30)
								{
									hadEnoughMovement = true;
									break;
								}

							if(hadEnoughMovement)
							{
								//Debug.Log((Input.mousePosition.x - SwipeHistoryPositions[SwipeHistoryPositions.Count - 1].x).ToString());
								
								IsDetectingSwipeRight = !IsDetectingSwipeRight;
								SwipesDetectedCount++;
								//Debug.Log("swipe moving right: " + SwipesDetectedCount.ToString());
								triedOnce = true;
								SwipeHistoryPositions.Clear();
							}
						}
						
						if(!triedOnce && (!IsDetectingSwipeRight || SwipesDetectedCount == 0))
						{
							bool hadEnoughMovement = false;
							for(int i=0;i<SwipeHistoryPositions.Count;++i)
							if((SwipeHistoryPositions[i].x - Input.mousePosition.x) >= 30)
									hadEnoughMovement = true;

							if(hadEnoughMovement)
							{
								//Debug.Log((Input.mousePosition.x - SwipeHistoryPositions[SwipeHistoryPositions.Count - 1].x).ToString());
								//SwipeHistoryPositions.Add(Input.mousePosition);
								IsDetectingSwipeRight = !IsDetectingSwipeRight;
								SwipesDetectedCount++;
								//Debug.Log("swipe moving left: " + SwipesDetectedCount.ToString());
								SwipeHistoryPositions.Clear();
							}
						}

						if(hadRayCollision)
						{
							if(!TouchesObjcesWhileSwiping.Contains(hitInfo.collider.gameObject))
								TouchesObjcesWhileSwiping.Add(hitInfo.collider.gameObject);
						}

						if(SwipesDetectedCount >= 3)
						{
							AtLeastOneSwipeDetected = true;
							//Debug.Log("SWIPE DETECTED");
							IsDetectingSwipeRight = !IsDetectingSwipeRight;
							SwipesDetectedCount = 0;
							

							bool cleanedShit = false;
							for(int i=0;i<TouchesObjcesWhileSwiping.Count;++i)
							{
								if(TouchesObjcesWhileSwiping[i].tag == "Shit" || TouchesObjcesWhileSwiping[i].tag == "Items")
								{
									if(TouchesObjcesWhileSwiping[i].tag == "Shit") cleanedShit = true;

									if(TouchesObjcesWhileSwiping[i].GetComponent<UIPopupItemScript>() != null)
									{
										PersistentData.Singleton.AddItemToInventory(TouchesObjcesWhileSwiping[i].GetComponent<UIPopupItemScript>().Id, 1);
									}

									if(TouchesObjcesWhileSwiping[i].GetComponent<EDMBoxScript>() != null)
									{
										TouchesObjcesWhileSwiping[i].GetComponent<EDMBoxScript>().Stop();
									}
							   
							  		GroundItems.Remove(TouchesObjcesWhileSwiping[i]);
									Destroy(TouchesObjcesWhileSwiping[i]);
									TouchesObjcesWhileSwiping.RemoveAt(i);

									HidePopupMenus();

									if(ObjectHolding == hitInfo.collider.gameObject)
									{
										ObjectHolding = null;
										animationController.IsHoldingItem = false;
									}

									i--;
								}
							}

						if(cleanedShit) UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.CleanPooPiss);

												
						if(TouchesObjcesWhileSwiping.Contains(this.gameObject) && !cleanedShit && !animationController.IsTickled)
						{
							Stop(true);
							animationController.IsTickled = true;

						UIGlobalVariablesScript.Singleton.SoundEngine.Play(PersistentData.Singleton.PlayerAniminId, PersistentData.Singleton.AniminEvolutionId, 
						                                                   (CreatureSoundId)((int)CreatureSoundId.Tickle + UnityEngine.Random.Range(0, 3)));
							
							
							
						}
					}

				
				SwipeHistoryPositions.Add(Input.mousePosition);
				if(SwipeHistoryPositions.Count >= 100)
				{
					SwipeHistoryPositions.RemoveAt(0);
				}




				}
				else if (Input.GetButtonUp("Fire1")) 
				{
					//Debug.Log("Input.GetButtonUp(Fire1)");
					IsDetectFlick = false;
					IsDetectingMouseMoveForDrag = false;



				if(UIGlobalVariablesScript.Singleton.DragableUI3DObject.transform.childCount == 1 && UIGlobalVariablesScript.Singleton.DragableUI3DObject.transform.GetChild(0).name == "Broom")
				{
					if(hadRayCollision && (hitInfo.collider.tag == "Items" || hitInfo.collider.tag == "Shit") && GroundItems.Contains(hitInfo.collider.gameObject))
					{



						GroundItems.Remove(hitInfo.collider.gameObject);
						Destroy(hitInfo.collider.gameObject);
					}
				}
				else if (!AtLeastOneSwipeDetected && hadRayCollision/* && !TriggeredHoldAction*/)
					{

						if(hitInfo.collider.name.StartsWith("MainCharacter") || hitInfo.collider.gameObject == ObjectHolding)
						{
							//Debug.Log("HIT THE CHARACTER FOR INTERACTION");

							if(ObjectHolding != null && !ObjectHolding./*GetComponent<ReferencedObjectScript>().Reference.*/GetComponent<UIPopupItemScript>().NonInteractable)
							{
								//Debug.Log("HIT THE CHARACTER FOR INTERACTION 2");

								UIPopupItemScript item = ObjectHolding./*GetComponent<ReferencedObjectScript>().Reference.*/GetComponent<UIPopupItemScript>();


								if(item.Type == PopupItemType.Food)
								{
									//Debug.Log("HIT THE CHARACTER FOR INTERACTION 3");

									this.GetComponent<CharacterProgressScript>().CurrentAction = ActionId.EatItem;
								}
								else if(OnInteractWithPopupItem(item))
								{
								//Debug.Log("HIT THE CHARACTER FOR INTERACTION 4");
									Destroy(ObjectHolding);
									ObjectHolding = null;
									animationController.IsHoldingItem = false;
								}

							}
							else if(ObjectHolding != null && ObjectHolding/*.GetComponent<ReferencedObjectScript>().Reference*/.GetComponent<UIPopupItemScript>().NonInteractable)
							{
							//Debug.Log("HIT THE CHARACTER FOR INTERACTION 3");
								UIGlobalVariablesScript.Singleton.SoundEngine.PlayFart();
							}
							else if(ObjectHolding == null && UIGlobalVariablesScript.Singleton.DragableUI3DObject.transform.childCount == 0 && !animationController.IsPat)
							{
							//Debug.Log("HIT THE CHARACTER FOR INTERACTION 4");
								Stop(true);
								animationController.IsPat = true;
								//Debug.Log("IS TICKLED");
								
							UIGlobalVariablesScript.Singleton.SoundEngine.Play(PersistentData.Singleton.PlayerAniminId, PersistentData.Singleton.AniminEvolutionId, CreatureSoundId.PatReact);
							}
						}

						else if((hitInfo.collider.tag == "Items") && hitInfo.collider/*.GetComponent<ReferencedObjectScript>().Reference*/.GetComponent<UIPopupItemScript>().Type == PopupItemType.Token)
						{
							OnInteractWithPopupItem(hitInfo.collider./*GetComponent<ReferencedObjectScript>().Reference.*/GetComponent<UIPopupItemScript>());
							this.GetComponent<CharacterProgressScript>().GroundItems.Remove(hitInfo.collider.gameObject);
							Destroy(hitInfo.collider.gameObject);
						}
					else if(hitInfo.collider.name.StartsWith("Invisible Ground Plane") || (hitInfo.collider.tag == "Items"))
						{
							//float distane = Vector3.Distance(hitInfo.point, this.transform.position);
							//MoveTo(hitInfo.point, distane > 220.0f ? true : false);
							if(RequestedToMoveToCounter == 0) RequestedTime = Time.time;
							RequestedToMoveToCounter++;
							moveHitInfo = hitInfo;

							if(ObjectHolding != null && (hitInfo.collider.tag == "Items"))
							{
								ObjectHolding.layer = LayerMask.NameToLayer("Default");
								ObjectHolding.transform.parent = ActiveWorld.transform;

								GetComponent<CharacterSwapManagementScript> ().CurrentModel.GetComponent<HeadReferenceScript> ().HoldingObject = null;

								ObjectHolding.transform.localPosition = new Vector3(ObjectHolding.transform.localPosition.x, 0, ObjectHolding.transform.localPosition.z);

								GroundItems.Add(ObjectHolding);
								ObjectHolding = null;
								animationController.IsHoldingItem = false;

							}
					   	}
						else
						{
							Stop(true);
							Debug.Log("STOPING BECAUSE NOTHING HIT");
						}
					}
//					else if(UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.activeInHierarchy)
//					{
//						UIGlobalVariablesScript.Singleton.ImageTarget.transform.rotation = 
//							Quaternion.Euler(
//								UIGlobalVariablesScript.Singleton.ImageTarget.transform.rotation.eulerAngles.x, 
//								UIGlobalVariablesScript.Singleton.ImageTarget.transform.rotation.eulerAngles.y + 90, 
//								UIGlobalVariablesScript.Singleton.ImageTarget.transform.rotation.eulerAngles.z);
//					}

					//TriggeredHoldAction = false;
				}

				//Debug.Log("END OF NONE");


				if(RequestedToMoveToCounter > 0)
				{
					if((Time.time - RequestedTime) >= 0.17f)
					{
						Stop(true);

						bool preventMovingTo = false;
						Vector3 point = moveHitInfo.point;
						if(moveHitInfo.collider.tag == "Items")
						{
							moveHitInfo.collider.gameObject.AddComponent<FlashObjectScript>();

							point = moveHitInfo.transform.position;
							

							bool isItemAlreadyOn = false;
							if((UIGlobalVariablesScript.Singleton.AlarmUI.activeInHierarchy 
						    || UIGlobalVariablesScript.Singleton.StereoUI.activeInHierarchy
						    || UIGlobalVariablesScript.Singleton.JunoUI.activeInHierarchy
						    || UIGlobalVariablesScript.Singleton.EDMBoxUI.activeInHierarchy
						    || UIGlobalVariablesScript.Singleton.PianoUI.activeInHierarchy
						    || UIGlobalVariablesScript.Singleton.LightbulbUI.activeInHierarchy) 
						   && (LastKnownObjectWithMenuUp == moveHitInfo.collider.gameObject))
							{
								isItemAlreadyOn = true;
							}

							if(RequestedToMoveToCounter == 1 && !isItemAlreadyOn && (moveHitInfo.collider/*.GetComponent<ReferencedObjectScript>().Reference*/.GetComponent<UIPopupItemScript>().Menu != MenuFunctionalityUI.None))
							{
								if( moveHitInfo.collider/*.GetComponent<ReferencedObjectScript>().Reference*/.GetComponent<UIPopupItemScript>().Menu == MenuFunctionalityUI.Clock)
								{
									HidePopupMenus();
									
									UIGlobalVariablesScript.Singleton.AlarmUI.GetComponent<UIWidget>().SetAnchor(moveHitInfo.collider.gameObject);
									//TriggeredHoldAction = true;
									UIGlobalVariablesScript.Singleton.AlarmUI.SetActive(true);
									LastKnownObjectWithMenuUp = moveHitInfo.collider.gameObject;
									preventMovingTo = true;
								}
								else if( moveHitInfo.collider/*.GetComponent<ReferencedObjectScript>().Reference*/.GetComponent<UIPopupItemScript>().Menu == MenuFunctionalityUI.EDMBox)
								{
									EDMBoxScript edmScript = moveHitInfo.collider.gameObject.GetComponent<EDMBoxScript>();
									HidePopupMenus();

									if(edmScript != null)
									{
										edmScript.SetInterface(UIGlobalVariablesScript.Singleton.EDMBoxUI);
										UIGlobalVariablesScript.Singleton.EDMBoxUI.GetComponent<UIWidget>().SetAnchor(moveHitInfo.collider.gameObject);
										UIGlobalVariablesScript.Singleton.EDMBoxUI.SetActive(true);
									}
									else
									{
										Debug.Log("edmScript is null");
									}
									
									LastKnownObjectWithMenuUp = moveHitInfo.collider.gameObject;
									preventMovingTo = true;
								}
								else if( moveHitInfo.collider/*.GetComponent<ReferencedObjectScript>().Reference*/.GetComponent<UIPopupItemScript>().Menu == MenuFunctionalityUI.Juno)
								{

									HidePopupMenus();
									moveHitInfo.collider.gameObject.GetComponent<EDMBoxScript>().SetInterface(UIGlobalVariablesScript.Singleton.JunoUI);
								UIGlobalVariablesScript.Singleton.JunoUI.GetComponent<UIWidget>().SetAnchor(moveHitInfo.collider.gameObject);
									UIGlobalVariablesScript.Singleton.JunoUI.SetActive(true);
									
									LastKnownObjectWithMenuUp = moveHitInfo.collider.gameObject;
									preventMovingTo = true;
								}
								else if( moveHitInfo.collider/*.GetComponent<ReferencedObjectScript>().Reference*/.GetComponent<UIPopupItemScript>().Menu == MenuFunctionalityUI.Piano)
								{
									HidePopupMenus();

									moveHitInfo.collider.gameObject.GetComponent<EDMBoxScript>().SetInterface(UIGlobalVariablesScript.Singleton.PianoUI);
									UIGlobalVariablesScript.Singleton.PianoUI.GetComponent<UIWidget>().SetAnchor(moveHitInfo.collider.gameObject);
									UIGlobalVariablesScript.Singleton.PianoUI.SetActive(true);
									
									LastKnownObjectWithMenuUp = moveHitInfo.collider.gameObject;
									preventMovingTo = true;
								}
								else if( moveHitInfo.collider/*.GetComponent<ReferencedObjectScript>().Reference*/.GetComponent<UIPopupItemScript>().Menu == MenuFunctionalityUI.Mp3Player)
								{
									HidePopupMenus();
									UIGlobalVariablesScript.Singleton.StereoUI.GetComponent<UIWidget>().SetAnchor(moveHitInfo.collider.gameObject);
									UIGlobalVariablesScript.Singleton.StereoUI.SetActive(true);

									LastKnownObjectWithMenuUp = moveHitInfo.collider.gameObject;
									preventMovingTo = true;
								}
								else if( moveHitInfo.collider/*.GetComponent<ReferencedObjectScript>().Reference*/.GetComponent<UIPopupItemScript>().Menu == MenuFunctionalityUI.Lightbulb)
								{
									HidePopupMenus();
									UIGlobalVariablesScript.Singleton.LightbulbUI.GetComponent<UIWidget>().SetAnchor(moveHitInfo.collider.gameObject);
									UIGlobalVariablesScript.Singleton.LightbulbUI.SetActive(true);
									
									LastKnownObjectWithMenuUp = moveHitInfo.collider.gameObject;
									preventMovingTo = true;
								}
							}
							
							else if(ObjectHolding == null)
							{
								IsGoingToPickUpObject = moveHitInfo.collider.gameObject;
								Debug.Log("going to pickup");
							}
							else
							{
								Debug.Log("will not pick up, i already have item");
							}
						}
						point.y = this.transform.position.y;

						if(!preventMovingTo)
						{
							HidePopupMenus();

							if(RequestedToMoveToCounter > 1)
								MoveTo(point, true);
							else
								MoveTo(point, false);
						}
						
						
						RequestedToMoveToCounter = 0;
					}
				}
				else
				{
				if(!IsMovingTowardsLocation && !animationController.IsWakingUp && ObjectHolding == null && PersistentData.Singleton.Hungry <= ConsideredHungryLevels && !animationController.IsTickled)
					{
						FeedMyselfTimer += Time.deltaTime;

						if(FeedMyselfTimer >= 1)
						{
							GameObject closestFood = GetClosestFoodToEat();
							if(closestFood != null)
							{
								IsGoingToPickUpObject = closestFood;
								MoveTo(closestFood.transform.position, false);
							}

							FeedMyselfTimer = 0;
						}
					}
				}
				
				

				break;
			}




		

			case ActionId.DropItem:
			{
				bool validDrop = false;
				
				  
				// DRAG ITEM ON TO THE CHARACTER
				if(hadRayCollision && hitInfo.collider.name.StartsWith("MainCharacter") && !animationController.IsHoldingItem)
				{

				//if(GroundItems.Contains(DragableObject)) Debug.Log("BUG REPORT!!!!!!!");

				GroundItems.Remove(DragableObject);
					PutItemInHands(DragableObject);
					validDrop = true;
				}
				else if(hadRayCollision && hitInfo.collider.name.StartsWith("Invisible Ground Plane"))
				{
				DragableObject.transform.parent = ActiveWorld.transform;
					validDrop = true;
					//GroundItems.Add(DragableObject);
					DragableObject.layer = LayerMask.NameToLayer("Default");

				}
				else
				{
					Debug.Log("DROPED IN UNKNOWN LOCATION");
				}

				DragableObject.GetComponent<BoxCollider>().enabled = true;
				DragableObject = null;
				CurrentAction = ActionId.None;
	
				break;
			}

			case ActionId.DragItemAround:
			{




			if(hadRayCollision && (hitInfo.collider.name.StartsWith("Invisible Ground Plane") || hitInfo.collider.name.StartsWith("Extended")))
			//if(hadRayCollision && hitInfo.collider.name.StartsWith("SecondGroundPlane"))
				{
					Debug.Log("DRAGGING");
					DragableObject.transform.position = hitInfo.point;
					//DragableObject.transform.parent = hit.transform;
				}

				if(!Input.GetButton("Fire1"))
				{
					if(hitInfo.collider.name.StartsWith("Extended")){
					DragableObject.AddComponent<DroppedItemScript>();
					}
					CurrentAction = ActionId.DropItem;
				}
				

				break;
			}
		}

		if((DateTime.Now - LastTimeToilet).TotalSeconds >= 90 && !animationController.IsSleeping && animationController.IsIdle && !IsMovingTowardsLocation)
		{
			GameObject newPoo;
			if(UnityEngine.Random.Range(0, 2) == 0)
			{
				newPoo = GameObject.Instantiate(PooPrefab) as GameObject;
				UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.TakePoo);
			}
			else
			{
				newPoo = GameObject.Instantiate(PissPrefab) as GameObject;
				UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.TakePiss);
			}

			newPoo.transform.parent = ActiveWorld.transform;
			newPoo.transform.position = this.transform.position;
			newPoo.transform.rotation = Quaternion.Euler(0, 180 + UnityEngine.Random.Range(-30.0f, 30.0f), 0);

			GroundItems.Add(newPoo);

			int sign = -1;
			if(UnityEngine.Random.Range(0, 2) == 0) sign = 1;
			float randomDistanceA = UnityEngine.Random.Range(30, 40);
			//if(Physics.Raycast(new Ray(UIGlobalVariablesScript.Singleton.main

			MoveTo(this.transform.position + new Vector3(UnityEngine.Random.Range(-40, 40), 0, randomDistanceA * sign), false);

			LastTimeToilet = DateTime.Now;
		}


		if(UIGlobalVariablesScript.Singleton.NonSceneRef.activeInHierarchy && UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localPosition.y <= -2)
		{

			UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localPosition = new Vector3(0, 1.01f, 0);
			UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.rotation =
				Quaternion.Euler(0,
				                 UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.rotation.eulerAngles.y,
				                 0);
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterControllerScript>().ResetRotation();
			Stop(true);
		}


		
		if(IsMovingTowardsLocation)
		{
			Vector3 direction = Vector3.Normalize(DestinationLocation - this.transform.position);
			this.gameObject.GetComponent<CharacterControllerScript>().MovementDirection = direction;

			this.gameObject.GetComponent<CharacterControllerScript>().RotateToLookAtPoint(DestinationLocation);

			if(Vector3.Distance(DestinationLocation , transform.position) <= 5)
			{
				Stop(true);
			}
		}
		else
		{

		}
		 

		UIGlobalVariablesScript.Singleton.HungryControlBarRef.transform.localPosition = new Vector3(Mathf.Lerp(-80.51972f, 617.2906f, PersistentData.Singleton.Hungry / 100.0f), UIGlobalVariablesScript.Singleton.HungryControlBarRef.transform.localPosition.y, 0);
		UIGlobalVariablesScript.Singleton.HealthControlBarRef.transform.localPosition = new Vector3(Mathf.Lerp(-80.51972f, 617.2906f, PersistentData.Singleton.Health / 100.0f), UIGlobalVariablesScript.Singleton.HealthControlBarRef.transform.localPosition.y, 0);
		UIGlobalVariablesScript.Singleton.HapynessControlBarRef.transform.localPosition = new Vector3(Mathf.Lerp(-80.51972f, 617.2906f, 
		                                                                                                         PersistentData.Singleton.Happy / PersistentData.MaxHappy), UIGlobalVariablesScript.Singleton.HapynessControlBarRef.transform.localPosition.y, 0);
		UIGlobalVariablesScript.Singleton.FitnessControlBarRef.transform.localPosition = new Vector3(Mathf.Lerp(-80.51972f, 617.2906f, PersistentData.Singleton.Fitness / 100.0f), UIGlobalVariablesScript.Singleton.FitnessControlBarRef.transform.localPosition.y, 0);
		//UIGlobalVariablesScript.Singleton.EvolutionControlBarRef.GetComponent<UISlider>().value = Evolution / 100.0f;

		if((DateTime.Now - LastSavePerformed).TotalSeconds >= 4)
		{
            SaveAndLoad.Instance.SaveDataToProfile();
		}

		if(Input.GetButtonDown("Fire1"))
			hadButtonDownLastFrame = true;
		else
			hadButtonDownLastFrame = false;


		if(!Input.GetButton("Fire1"))
		{
			IsDetectingSwipeRight = false;
			SwipesDetectedCount = 0;
			SwipeHistoryPositions.Clear();
			AtLeastOneSwipeDetected = false;
			TouchesObjcesWhileSwiping.Clear();
		}
	

		UIGlobalVariablesScript.Singleton.ZefTokensUI.text = PersistentData.Singleton.ZefTokens.ToString();
	
		//lastMousePosition = Input.mousePosition;
		DragedObjectedFromUIToWorld = false;
		lastActionId = CurrentAction;
		HadUITouchLastFrame = hadUItouch;
	}

//	public void ShowText(string text)
//	{
//		TextTest.text = text;
//		TextTest.color = new Color(1,1,1,1);
//	}


	public void exitSleep(){
		LastTimeToilet = DateTime.Now;
		Debug.Log("exit sleep");
		animationController.IsSleeping = false;
		CurrentAction = ActionId.None;
		SleepBoundingBox.SetActive(false);
		UIGlobalVariablesScript.Singleton.SoundEngine.Play(PersistentData.Singleton.PlayerAniminId, PersistentData.Singleton.AniminEvolutionId, CreatureSoundId.SleepToIdle);
		UIGlobalVariablesScript.Singleton.SoundEngine.StopLoop();
	}




	public void PickupItem(GameObject item)
	{
		Debug.Log("void PickupItem(GameObject item)");
		UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.ItemPickup);
		this.GetComponent<CharacterProgressScript>().GroundItems.Remove(item);
		Stop(true);
		//Physics.IgnoreCollision(item.collider, this.collider, true);
		PutItemInHands(item);
//		Debug.Log("DISABLING COLLISION");
	}



	public void PutItemInHands(GameObject item)
	{
		//item.GetComponent<BoxCollider>().enabled = false;
		//if(item.collider.gameObject.activeInHierarchy)



		item.layer = LayerMask.NameToLayer("IgnoreCollisionWithCharacter");
		//item.transform.parent = GetComponent<CharacterSwapManagementScript>().CurrentModel.GetComponent<HeadReferenceScript>().ObjectCarryAttachmentBone.transform;
		GetComponent<CharacterSwapManagementScript> ().CurrentModel.GetComponent<HeadReferenceScript> ().HoldingObject = item;
		animationController.IsHoldingItem = true;
		ObjectHolding = item;

		item.transform.localPosition = Vector3.zero;
		item.transform.localRotation = Quaternion.identity;
		
		/*item.transform.localPosition = new Vector3(2.137475f, 
		                                           -1.834323f, 
		                                           0.3105991f);
		
		item.transform.localRotation = Quaternion.Euler(44.08633f,
		                                                159.2195f,
		                                                -100.7192f);*/

	}

	public bool OnInteractWithPopupItem(UIPopupItemScript item)
	{
		switch(item.Type)
		{
			case PopupItemType.Token:
			{
				//Stop(true);
				EvolutionManager.Instance.AddZef();

//				for(int i=0;i<(int)AniminSubevolutionStageId.Count;++i)
//				{
//					if(PersistentData.Singleton.Evolution >= AniminSubevolutionStageData.Stages[i])
//					{
//					if(!PersistentData.Singleton.SubstagesCompleted.Contains((AniminSubevolutionStageId)i))
//						{
//						PersistentData.Singleton.SubstagesCompleted.Add((AniminSubevolutionStageId)i);
//							AchievementsScript.Singleton.Show(AchievementTypeId.Evolution, 0);
//						}
//					}
//
//				}

			/*if(PersistentData.Singleton.Evolution >= 100)
				{
				if(PersistentData.Singleton.AniminEvolutionId != AniminEvolutionStageId.Adult)
					{
					PersistentData.Singleton.Evolution = 0;
					PersistentData.Singleton.AniminEvolutionId = (AniminEvolutionStageId)((int)PersistentData.Singleton.AniminEvolutionId + 1);
						UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterSwapManagementScript>().LoadCharacter(PersistentData.Singleton.PlayerAniminId, PersistentData.Singleton.AniminEvolutionId);

					}
				}*/


				//UIGlobalVariablesScript.Singleton.EvolutionProgressSprite.width = (int)(1330.0f * (Evolution / 100.0f));
				Debug.Log("TOKEN COLLECTED");

				break;
			}

			case PopupItemType.Food:
			{
			/*if(Hungry >= 95)
			{
				animationController.IsNo = true;
				return false;
			}
			else
			{*/
				//ShowText("yum yum");
			PersistentData.Singleton.Hungry += item.Points;
			
			AchievementManager.Instance.AddToAchievment(AchievementManager.Achievements.EatFruit);
				//Stop(true);
						

		
					//}
				break;
			}

			case PopupItemType.Item:
			{
			
				AchievementManager.Instance.AddToAchievment(AchievementManager.Achievements.PlayMusic);
				//ShowText("I can't use this item");
				return false;
				break;
			}

			case PopupItemType.Medicine:
			{
			/*if(Health >= 95)
			{
				animationController.IsNo = true;
				return false;
			}
			else
			{*/
				//ShowText("I feel good");
				PersistentData.Singleton.Health += item.Points;
				Stop(true);
				animationController.IsTakingPill = true;
				AchievementManager.Instance.AddToAchievment(AchievementManager.Achievements.Heal);


			if(item.SpecialId == SpecialFunctionalityId.Injection)
				UIGlobalVariablesScript.Singleton.SoundEngine.Play(PersistentData.Singleton.PlayerAniminId, PersistentData.Singleton.AniminEvolutionId, CreatureSoundId.InjectionReact);
			else
				UIGlobalVariablesScript.Singleton.SoundEngine.Play(PersistentData.Singleton.PlayerAniminId, PersistentData.Singleton.AniminEvolutionId, CreatureSoundId.EatPill);

			//}
				break;
			}

		}

		return true;
	}

	public void GiveMedicine(ItemId id)
	{
		
	}


	public void MoveTo(Vector3 location, bool run)
	{
//		Debug.Log("Moving to point: " + location.ToString());
		IsMovingTowardsLocation = true;
	
		DestinationLocation = location;


		animationController.IsRunning = run;
		animationController.IsWalking = !run;

		GetComponent<CharacterControllerScript>().walkSpeed = 35;
		if(run) GetComponent<CharacterControllerScript>().walkSpeed = 120;

		Vector3 direction = Vector3.Normalize(location - this.transform.position);

//		Debug.Log(direction.ToString());
		this.gameObject.GetComponent<CharacterControllerScript>().MovementDirection = direction;
	}

	public void Stop(bool stopMovingAsWell)
	{
		IsGoingToPickUpObject = null;

		animationController.IsNotWell = false;
		animationController.IsHungry = false;
		InteractWithItemOnPickup = false;
		ShouldThrowObjectAfterPickup = false;

		/*sadUnwellLoopState = HungrySadUnwellLoopId.OnCooldown;

		if(TimeForNextHungryUnwellSadAnimation <= 1)
		{
			TimeForNextHungryUnwellSadAnimation += 1;
		}*/

		if(stopMovingAsWell)
		{
			animationController.IsWalking = false;
			animationController.IsRunning = false;
			this.gameObject.GetComponent<CharacterControllerScript>().MovementDirection = Vector3.zero;
			IsMovingTowardsLocation = false;
		}
	}
}

public enum ActionId
{
	None = 0,
	EnterSleep,
	Sleep,
	DetectMouseMoveAndDrag,
	DropItem,
	MoveToRequestedLocation,
	DragItemAround,
	DetectFlickAndThrow,
	EnterPortalToAR,
	EnterPortalToNonAR,
	SmallCooldownPeriod,
	EatItem,
	WaitEatingFinish,
	ThrowItemAfterPickup,
	ExitPortalMainStage,
}

public enum HungrySadUnwellLoopId
{
	DetectAnimation = 0,
	PlayHungry,
	PlaySad,
	PlayUnwell,
	OnCooldown,
}

//public enum AniminStateId
//{
//	Idle = 0,
//	GoingToTheToilet,
//	ToiletTime,
//	MovingToTapedLocation,
//	MovingToToiletLocation,
//}


public enum AchievementId
{
	None = 0,
	TestAchievent1,
	TestAchievent2,
	TestAchievent3,
	TestAchievent4,
	Count,
}

public enum ItemId
{
	None = 0,
	Strawberry,
}

