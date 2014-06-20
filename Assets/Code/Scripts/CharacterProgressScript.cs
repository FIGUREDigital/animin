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

public class CharacterProgressScript : MonoBehaviour 
{
	public float Happy;
	public float Hungry;
	public float Fitness;
	public float Evolution;
	public float Health;
	public int EvolutionStage;
	public int ZefTokens;
	public List<AchievementId> Achievements = new List<AchievementId>();
	public DateTime NextHappynBonusTimeAt;
	public DateTime LastSavePerformed;

	public List<GameObject> GroundItems = new List<GameObject>();
	private Vector3 TravelLocation;
	private float IdleCooldown;
	public TextMesh TextTest;
	public AnimationControllerScript animationController;
	private bool IsMovingTowardsLocation;
	public GameObject ObjectCarryAttachmentBone;
	private GameObject DragableObject;
	private GameObject ObjectHolding;
	private float LastTapClick;

	private int RequestedToMoveToCounter;
	private float RequestedTime;

	private bool RequestedToMoveTo;
	private bool ShouldDragIfMouseMoves;
	private Vector3 MousePositionAtDragIfMouseMoves;
	private GameObject IsGoingToPickUpObject;
	public bool DragedObjectedFromUIToWorld;

	public GameObject SleepBoundingBox;

	ItemPickupSavedData pickupItemSavedData = new ItemPickupSavedData();

	// Use this for initialization
	void Start () 
	{
		Happy = 100;
		Hungry = 100;
		Fitness = 100;
		Health = 100;
		LastSavePerformed = DateTime.Now;

		Load();

		IdleCooldown = UnityEngine.Random.Range(1.0f, 3.0f);

		TextTest.color = new Color(1, 1, 1, 0.0f);

		Physics.IgnoreLayerCollision(
			LayerMask.NameToLayer( "IgnoreCollisionWithCharacter"),  
			LayerMask.NameToLayer( "Character"));


	}

	void OnApplicationPause(bool pauseStatus)
	{
		//Debug.Log("pauseStatus:" + pauseStatus.ToString());
		if(pauseStatus)
		{
			Stop();
			//Debug.Log("pauseStatus:" + pauseStatus.ToString());
			animationController.SetAnimation(IdleStateId.Sleeping);
		

		
		}
		else
		{
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

		Hungry -= Time.deltaTime * 0.1f;
		Fitness -= Time.deltaTime * 0.1f;
		Health -= Time.deltaTime * 0.1f;

		if(Health < 0 ) Health = 0;
		if(Hungry < 0 ) Hungry = 0;
		if(Fitness < 0) Fitness = 0;

	
		TextTest.color = new Color(1,1,1, TextTest.color.a - Time.deltaTime * 0.6f);
		if(TextTest.color.a < 0)
			TextTest.color = new Color(1,1,1, 0);


		Happy = (Hungry + Fitness + Health) / 3.0f;

		if(NextHappynBonusTimeAt >= DateTime.Now)
		{
			// do bonus of happyness
		}

		bool hadUItouch = false;

		if(Input.GetButtonDown("Fire1"))
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
					//Debug.Log("normalUIRAY:" + hit.collider.gameObject);
					if(hit.collider.gameObject.layer == LayerMask.NameToLayer( "UI" ))
					{
						//Debug.Log("normalUIRAY +++++:" + hit.collider.gameObject);
						hadUItouch = true;
					}

				}
				
				/*if( Physics.Raycast( inputRay.origin, inputRay.direction, out hit, Mathf.Infinity, LayerMask.NameToLayer( "UI" ) ) )
				{
					hadUItouch = true;
					Debug.Log("uitouch:" + hit.collider.name);
				}*/
			}
		}
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		
		if (!hadUItouch && !DragedObjectedFromUIToWorld && Input.GetButtonUp("Fire1")) 
		{
			RaycastHit hitInfo;
			
			if (Physics.Raycast(ray, out hitInfo))
			{
				
				switch(animationController.IdleState)
				{
				case IdleStateId.Sleeping:
				{
					if(hitInfo.collider.gameObject == SleepBoundingBox)
					{
						animationController.Wakeup();
					

					}
					break;
				}
					
					
					
				default:
				{
					if(!animationController.IsEating && !animationController.IsTakingPill && !animationController.IsTickled)
					{
						if(DragableObject == null)
						{
							if(hitInfo.collider.name.StartsWith("MainCharacter"))
							{
								animationController.IsTickled = true;
							}

							else if((hitInfo.collider.tag == "Items"))
							{
								if(RequestedToMoveToCounter == 0) RequestedTime = Time.time;
								RequestedToMoveToCounter++;
							}
							else if(hitInfo.collider.name.StartsWith("Invisible Ground Plane") || (hitInfo.collider.tag == "Items"))
							{
								//float distane = Vector3.Distance(hitInfo.point, this.transform.position);
								//MoveTo(hitInfo.point, distane > 220.0f ? true : false);
								if(RequestedToMoveToCounter == 0) RequestedTime = Time.time;
								RequestedToMoveToCounter++;

							}
						}
					}
					
					break;
				}
				}
			}
		}



		if(hadUItouch || DragedObjectedFromUIToWorld)
		{
			ShouldDragIfMouseMoves = false;
			if(DragableObject != null) 
			{
				DragableObject.GetComponent<BoxCollider>().enabled = true;
				//if(!animationController.IsHoldingItem) Physics.IgnoreCollision(DragableObject.collider, this.collider, false);
				//GroundItems.Add(DragableObject);
			}
			
			DragableObject = null;
			Debug.Log("can't drag");
		}
		else 
		// GRAB
		if(Input.GetButtonDown("Fire1"))
		{
			ShouldDragIfMouseMoves = true;
			MousePositionAtDragIfMouseMoves = Input.mousePosition;
		}

		// DROP
		else if(Input.GetButtonUp("Fire1"))
		{
			if(DragableObject != null)
			{
				bool validDrop = false;

				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{   
					//Debug.Log(hit.collider.tag);
					// DRAG ITEM ON TO THE CHARACTER
					if(hit.collider.name.StartsWith("MainCharacter") && !animationController.IsHoldingItem)
					{
						//Physics.IgnoreCollision(DragableObject.collider, this.collider, true);
						//DragableObject.GetComponent<BoxCollider>().enabled = true;
						PutItemInHands(DragableObject);
						validDrop = true;
					}
					else if(hit.collider.tag == "Floor")
					{
						DragableObject.transform.parent = this.transform.parent.transform;
						validDrop = true;
						GroundItems.Add(DragableObject);
						DragableObject.layer = LayerMask.NameToLayer("IgnoreCollisionWithCharacter");
						//DragableObject.GetComponent<BoxCollider>().enabled = true;
						//Physics.IgnoreCollision(DragableObject.collider, this.collider, false);
						//Debug.Log("ACTIVAITING COLLISION");
					}

				}

				DragableObject.GetComponent<BoxCollider>().enabled = true;
				DragableObject = null;
			}

			ShouldDragIfMouseMoves = false;
		}




		// DRAG
		else if(Input.GetButton("Fire1"))
		{
			Debug.Log("BUTTON DOWN");

			if(ShouldDragIfMouseMoves && (MousePositionAtDragIfMouseMoves != Input.mousePosition))
			{
				Debug.Log("ShouldDragIfMouseMoves");
				ShouldDragIfMouseMoves = false;
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{ 
					pickupItemSavedData.WasInHands = false;

					// DRAG ITEM FROM CHARACTER AWAY FROM HIM
					if(hit.collider.name.StartsWith("MainCharacter") && DragableObject == null && animationController.IsHoldingItem)
					{
						pickupItemSavedData.Position = DragableObject.transform.position;
						pickupItemSavedData.Rotation = DragableObject.transform.rotation.eulerAngles;
						pickupItemSavedData.WasInHands = true;

						Debug.Log("SELECTED OBJECT:" + hit.collider.name);
						DragableObject = ObjectHolding;
						DragableObject.GetComponent<BoxCollider>().enabled = false;
						DragableObject.transform.parent = this.transform.parent;
						animationController.IsHoldingItem = false;
						//Physics.IgnoreCollision(DragableObject.collider, this.collider, true);


					}
					
					// GRAB ITEM ITSELF EITHER FROM HANDS OR FLOOR
					else if(hit.collider.tag == "Items")
					{

						if(animationController.IsHoldingItem)
						{

							//DragableObject = ObjectHolding;
							animationController.IsHoldingItem = false;
							pickupItemSavedData.Position = DragableObject.transform.position;
							pickupItemSavedData.Rotation = DragableObject.transform.rotation.eulerAngles;

							ObjectHolding.transform.parent = this.transform.parent;
							pickupItemSavedData.WasInHands = true;
						}
						else
						{
							DragableObject = hit.collider.gameObject;
							pickupItemSavedData.WasInHands = false;

							pickupItemSavedData.Position = DragableObject.transform.position;
							pickupItemSavedData.Rotation = DragableObject.transform.rotation.eulerAngles;
							//Physics.IgnoreCollision(DragableObject.collider, this.collider, true);
							//Debug.Log("DISABLING COLLISION");
						}


						
						Debug.Log("SELECTED OBJECT:" + hit.collider.name);
						
						DragableObject.GetComponent<BoxCollider>().enabled = false;
						
					}
				}
			}
			else
			{

				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{ 
					if(hit.collider.tag == "Floor" && DragableObject != null)
					{
						Debug.Log("DRAGGING");
						DragableObject.transform.position = hit.point;
						//DragableObject.transform.parent = hit.transform;
					}
				}
			}
		}
		else 
		{
			ShouldDragIfMouseMoves = false;
			if(DragableObject != null) 
			{
				DragableObject.GetComponent<BoxCollider>().enabled = true;
				//if(!animationController.IsHoldingItem) Physics.IgnoreCollision(DragableObject.collider, this.collider, false);
				//GroundItems.Add(DragableObject);
			}
			
			DragableObject = null;

			Debug.Log("no collision");
		}

		if(RequestedToMoveToCounter > 0)
		{
			if((Time.time - RequestedTime) >= 0.17f)
			{
				RaycastHit hitInfo;
				
				if (Physics.Raycast(ray, out hitInfo))
				{
					ResetActions();

					Vector3 point = hitInfo.point;
					if(hitInfo.collider.tag == "Items")
					{
						point = hitInfo.transform.position;

						if(!animationController.IsHoldingItem)
						{
							hitInfo.collider.gameObject.layer = LayerMask.NameToLayer("Default");
							IsGoingToPickUpObject = hitInfo.collider.gameObject;
							Debug.Log("going to pickup");
						}
						else
						{
							Debug.Log("will not pick up, i already have item");
						}
					}

					if(RequestedToMoveToCounter > 1)
						MoveTo(point, true);
					else
						MoveTo(point, false);
				}

				RequestedToMoveToCounter = 0;
			}
		}
		 
		/*
		switch(animationController.IdleState)
		{
			case IdleStateId.Default:
			{
				//MoveTo(new Vector3(UnityEngine.Random.Range(-100.0f, 100.0f), 0, UnityEngine.Random.Range(-100.0f, 100.0f)));

			if(GroundItems.Count > 0 && !animationController.IsEating && !animationController.IsTakingPill && !animationController.IsTickled && DragableObject == null && !animationController.IsHoldingItem)
				{
					IdleCooldown -= Time.deltaTime;
					if(IdleCooldown <= 0)
					{
						ShowText("FOOOD!");
						IdleCooldown = UnityEngine.Random.Range(4.0f, 7.0f);
						MoveTo(GroundItems[UnityEngine.Random.Range(0, GroundItems.Count - 1)].transform.position, false);
					}
				}

				break;
			}
		}*/

		if(IsMovingTowardsLocation)
		{
			if(Vector3.Distance(TravelLocation, this.transform.position) <= 5f)
			{
				Stop();
			}
		}


		UIGlobalVariablesScript.Singleton.HungryControlBarRef.transform.localPosition = new Vector3(Mathf.Lerp(-80.51972f, 617.2906f, Hungry / 100.0f), UIGlobalVariablesScript.Singleton.HungryControlBarRef.transform.localPosition.y, 0);
		UIGlobalVariablesScript.Singleton.HealthControlBarRef.transform.localPosition = new Vector3(Mathf.Lerp(-80.51972f, 617.2906f, Health / 100.0f), UIGlobalVariablesScript.Singleton.HealthControlBarRef.transform.localPosition.y, 0);
		UIGlobalVariablesScript.Singleton.HapynessControlBarRef.transform.localPosition = new Vector3(Mathf.Lerp(-80.51972f, 617.2906f, Happy / 100.0f), UIGlobalVariablesScript.Singleton.HapynessControlBarRef.transform.localPosition.y, 0);
		UIGlobalVariablesScript.Singleton.FitnessControlBarRef.transform.localPosition = new Vector3(Mathf.Lerp(-80.51972f, 617.2906f, Fitness / 100.0f), UIGlobalVariablesScript.Singleton.FitnessControlBarRef.transform.localPosition.y, 0);
		//UIGlobalVariablesScript.Singleton.EvolutionControlBarRef.GetComponent<UISlider>().value = Evolution / 100.0f;

		if((DateTime.Now - LastSavePerformed).TotalSeconds >= 1)
		{
			Save();
		}
	
	
		DragedObjectedFromUIToWorld = false;
	}

	public void ShowText(string text)
	{
		TextTest.text = text;
		TextTest.color = new Color(1,1,1,1);
	}

	public void Save()
	{
		PlayerPrefs.SetFloat("Happy", Happy);
		PlayerPrefs.SetFloat("Hungry", Hungry);
		PlayerPrefs.SetFloat("Fitness", Fitness);
		PlayerPrefs.SetFloat("Evolution", Evolution);
		PlayerPrefs.SetInt("EvolutionStage", EvolutionStage);
		PlayerPrefs.SetInt("ZefTokens", ZefTokens);

	}

	public void Load()
	{
		if(PlayerPrefs.HasKey("Happy"))
			Happy = PlayerPrefs.GetFloat("Happy");

		if(PlayerPrefs.HasKey("Hungry"))
			Hungry = PlayerPrefs.GetFloat("Hungry");

		if(PlayerPrefs.HasKey("Fitness"))
			Fitness = PlayerPrefs.GetFloat("Fitness");

		if(PlayerPrefs.HasKey("Evolution"))
			Evolution = PlayerPrefs.GetFloat("Evolution");

		if(PlayerPrefs.HasKey("EvolutionStage"))
			EvolutionStage = PlayerPrefs.GetInt("EvolutionStage");

		if(PlayerPrefs.HasKey("ZefTokens"))
			ZefTokens = PlayerPrefs.GetInt("ZefTokens");

	}

	public void PickupItem(GameObject item)
	{
		this.GetComponent<CharacterProgressScript>().GroundItems.Remove(item);
		Stop();
		//Physics.IgnoreCollision(item.collider, this.collider, true);
		PutItemInHands(item);
//		Debug.Log("DISABLING COLLISION");
	}



	private void PutItemInHands(GameObject item)
	{
		//item.GetComponent<BoxCollider>().enabled = false;
		//if(item.collider.gameObject.activeInHierarchy)
			
		item.layer = LayerMask.NameToLayer("IgnoreCollisionWithCharacter");
		item.transform.parent = ObjectCarryAttachmentBone.transform;
		animationController.IsHoldingItem = true;
		ObjectHolding = item;
		
		item.transform.localPosition = new Vector3(2.137475f, 
		                                           -1.834323f, 
		                                           0.3105991f);
		
		item.transform.localRotation = Quaternion.Euler(44.08633f,
		                                                159.2195f,
		                                                -100.7192f);
	}

	public void OnInteractWithPopupItem(UIPopupItemScript item)
	{
		switch(item.Type)
		{
			case PopupItemType.Food:
			{
				ShowText("yum yum");
				Hungry += item.Points;
				Stop();
				animationController.IsEating = true;
				break;
			}

			case PopupItemType.Item:
			{
				ShowText("I can't use this item");
				break;
			}

			case PopupItemType.Medicine:
			{
				ShowText("I feel good");
				Stop();
				animationController.IsTakingPill = true;
				break;
			}

		}
	}

	public void GiveMedicine(ItemId id)
	{
		
	}


	public void MoveTo(Vector3 location, bool run)
	{
		IsMovingTowardsLocation = true;
		TravelLocation = location;

		if(!run)
		{
			animationController.SetAnimation(IdleStateId.Moving);
		}
		else
		{
			animationController.SetAnimation(IdleStateId.Default);
		}

		animationController.IsRunning = run;

		GetComponent<CharacterControllerScript>().walkSpeed = 35;
		if(run) GetComponent<CharacterControllerScript>().walkSpeed = 120;

		Vector3 direction = Vector3.Normalize(TravelLocation - this.transform.position);
		this.gameObject.GetComponent<CharacterControllerScript>().MovementDirection = direction;
	}

	public void Stop()
	{
		animationController.IsRunning = false;
		this.gameObject.GetComponent<CharacterControllerScript>().MovementDirection = Vector3.zero;
		IsMovingTowardsLocation = false;
		if(animationController.IdleState != IdleStateId.Sleeping)
		{
			animationController.SetAnimation(IdleStateId.Default);
		}
		ResetActions();
	}

	private void ResetActions()
	{
		if(IsGoingToPickUpObject != null)
		{
			IsGoingToPickUpObject.layer = LayerMask.NameToLayer("IgnoreCollisionWithCharacter");
		}
		
		IsGoingToPickUpObject = null;
	}
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
