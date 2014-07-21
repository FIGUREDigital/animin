﻿using UnityEngine;
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
	public CreatureTypeId CreaturePlayerId;
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
	public DateTime LastTimeToilet;
	public int StarsOwned = 10;
	private bool IsDetectingSwipeRight;
	private int SwipesDetectedCount;
	private bool AtLeastOneSwipeDetected;
	private List<GameObject> TouchesObjcesWhileSwiping = new List<GameObject>();

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

	public GameObject ActiveARScene
	{
		get
		{
			if(UIGlobalVariablesScript.Singleton.ARSceneRef.activeInHierarchy)
				return UIGlobalVariablesScript.Singleton.ARSceneRef;
			else
				return UIGlobalVariablesScript.Singleton.NonSceneRef;
		}
	}

	public GameObject PooPrefab;
	public GameObject PissPrefab;

	private Vector3 DestinationLocation;

	public TextMesh TextTest;
	public AnimationControllerScript animationController;
	public bool IsMovingTowardsLocation;
	public GameObject ObjectCarryAttachmentBone;
	private GameObject DragableObject;
	private GameObject ObjectHolding;
	private float LastTapClick;

	private int RequestedToMoveToCounter;
	private float RequestedTime;

	private bool RequestedToMoveTo;
	private bool ShouldDragIfMouseMoves;
	private Vector3 MousePositionAtDragIfMouseMoves;
	public GameObject IsGoingToPickUpObject;
	public bool DragedObjectedFromUIToWorld;
	public ActionId CurrentAction;

	public GameObject SleepBoundingBox;

	ItemPickupSavedData pickupItemSavedData = new ItemPickupSavedData();
	RaycastHit moveHitInfo;
	RaycastHit detectDragHit;
	ActionId lastActionId;
	bool hadButtonDownLastFrame;
	bool IsDetectingMouseMoveForDrag;
	bool IsDetectFlick;
	float FeedMyselfTimer;
	private bool HadUITouchLastFrame;
	private GameObject LastKnownObjectWithMenuUp;


	float TimeForNextHungryUnwellSadAnimation;
	float LengthOfHungryUnwellSadAnimation;
	HungrySadUnwellLoopId sadUnwellLoopState;
	float HoldingLeftButtonDownTimer;
	private bool TriggeredHoldAction;
	private List<Vector3> SwipeHistoryPositions = new List<Vector3>();
	public float PortalTimer;

	// Use this for initialization
	void Awake () 
	{
		CreaturePlayerId = CreatureTypeId.TBOBaby;
		Happy = 100;
		Hungry = 100;
		Fitness = 100;
		Health = 100;
		LastSavePerformed = DateTime.Now;
		LastTimeToilet = DateTime.Now;

		Load();

		TextTest.color = new Color(1, 1, 1, 0.0f);

		Physics.IgnoreLayerCollision(
			LayerMask.NameToLayer( "IgnoreCollisionWithCharacter"),  
			LayerMask.NameToLayer( "Character"));


		//CurrentAction = ActionId.EnterSleep;

		animationController.IsSleeping = true;
		CurrentAction = ActionId.Sleep;
		SleepBoundingBox.SetActive(true);


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
			Stop(true);
		}
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
	
	// Update is called once per frame
	void Update () 
	{
		Hungry -= Time.deltaTime * 0.2f;
		Fitness -= Time.deltaTime * 0.2f;
		Health -= Time.deltaTime * 0.2f;

		if(Health < 0 ) Health = 0;
		if(Hungry < 0 ) Hungry = 0;
		if(Fitness < 0) Fitness = 0;

	
		TextTest.color = new Color(1,1,1, TextTest.color.a - Time.deltaTime * 0.6f);
		if(TextTest.color.a < 0)
			TextTest.color = new Color(1,1,1, 0);


		Happy = (Hungry + Fitness + Health) / 3.0f;

		Evolution += (Happy / 100.0f) * Time.deltaTime * 0.1f;
		if(Evolution >= 100) Evolution = 100;

		UIGlobalVariablesScript.Singleton.EvolutionProgressSprite.width = (int)(1330.0f * (Evolution / 100.0f));

		if(NextHappynBonusTimeAt >= DateTime.Now)
		{
			// do bonus of happyness
		}

		if(ObjectHolding != null)
		{
			UIGlobalVariablesScript.Singleton.IndicatorAboveHead.SetActive(true);
		}
		else
		{
			UIGlobalVariablesScript.Singleton.IndicatorAboveHead.SetActive(false);
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
		}



		//Debug.Log(CurrentAction.ToString());
		switch(CurrentAction)
		{
		case ActionId.EnterPortalToAR:
			{
				//OnEnterARScene();
				PortalTimer += Time.deltaTime;

				if(PortalTimer >= 1.10f)
			   	{
					Debug.Log("EnterPortalToAR finished");
					CurrentAction = ActionId.None;
					UIGlobalVariablesScript.Singleton.Vuforia.OnEnterARScene();
					
					
			
					UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimateCharacterOutPortalScript>().Timer = 0;
					UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimateCharacterOutPortalScript>().JumbId = AnimateCharacterOutPortalScript.JumbStateId.Jumbout;

				UIGlobalVariablesScript.Singleton.ARPortal.GetComponent<PortalScript>().Show(true, false);
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
				break;
			}

			case ActionId.Sleep:
			{
				if( Input.GetButtonUp("Fire1") )
				{
					if (!hadUItouch && hadRayCollision && hitInfo.collider.gameObject == SleepBoundingBox)
					{
						LastTimeToilet = DateTime.Now;
						Debug.Log("exit sleep");
						animationController.IsSleeping = false;
						CurrentAction = ActionId.None;
						SleepBoundingBox.SetActive(false);
						UIGlobalVariablesScript.Singleton.SoundEngine.Play(CreaturePlayerId, CreatureSoundId.SleepToIdle);
						
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
					Debug.Log("if(lastActionId != ActionId.None)");
				}
			   
				else if(HadUITouchLastFrame || hadUItouch || DragedObjectedFromUIToWorld)
				{
					Debug.Log("UI TOUCH");

					break;
				}
					
				else if(Input.GetButtonDown("Fire1"))
				{
					Debug.Log("else if(Input.GetButtonDown(Fire1))");

					if(hadRayCollision)
					{
						if(hitInfo.collider.gameObject == ObjectHolding || hitInfo.collider.gameObject == this.gameObject)
						{
							IsDetectFlick = true;
							//CurrentAction = ActionId.DetectFlickAndThrow;
							MousePositionAtDragIfMouseMoves = Input.mousePosition;
						}
						else if(hitInfo.collider.tag == "Items")
						{
							IsDetectingMouseMoveForDrag = true;
							//CurrentAction = ActionId.DetectMouseMoveAndDrag;
							MousePositionAtDragIfMouseMoves = Input.mousePosition;
							detectDragHit = hitInfo;
							Debug.Log("DetectMouseMoveAndDrag");
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
					Debug.Log("IsDetectFlick");
					//DragableObject = ObjectHolding;
					animationController.IsHoldingItem = false;

					Ray raySecond = Camera.main.ScreenPointToRay(Input.mousePosition);

					Vector3 throwdirection = Vector3.Normalize(Input.mousePosition - MousePositionAtDragIfMouseMoves);
					throwdirection.z = throwdirection.y;
					throwdirection.y = 0;


					this.gameObject.GetComponent<CharacterControllerScript>().RotateToLookAtPoint(this.transform.position + throwdirection * 50);


					//pickupItemSavedData.Position = DragableObject.transform.position;
					//pickupItemSavedData.Rotation = DragableObject.transform.rotation.eulerAngles;
					
					ObjectHolding.transform.parent = ActiveARScene.transform;
					
					ThrowAnimationScript throwScript = ObjectHolding.AddComponent<ThrowAnimationScript>();
					float maxDistance = Vector3.Distance(Input.mousePosition, MousePositionAtDragIfMouseMoves) * 0.35f;
					if(maxDistance >= 160) maxDistance = 160;

					throwScript.Begin(throwdirection, maxDistance);
					
					UIGlobalVariablesScript.Singleton.SoundEngine.Play(CreaturePlayerId, CreatureSoundId.Throw);
					//ObjectHolding.transform.position = this.transform.position;
					



					GroundItems.Add(ObjectHolding);
					
					ObjectHolding.transform.rotation = Quaternion.Euler(0, ObjectHolding.transform.rotation.eulerAngles.y, 0);
					pickupItemSavedData.WasInHands = true;
					animationController.IsThrowing = true;
					IsDetectFlick = false;
					ObjectHolding = null;
					//CurrentAction = ActionId.None;
				}
				else if(IsDetectingMouseMoveForDrag && Vector3.Distance(Input.mousePosition, MousePositionAtDragIfMouseMoves) >= 5 && Input.GetButton("Fire1"))
				{
					Debug.Log("IsDetectingMouseMoveForDrag");
					pickupItemSavedData.WasInHands = false;
					
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
					{
						//Debug.Log("IT SHOULD GO AND DRAG NOW");

						DragableObject = detectDragHit.collider.gameObject;
						pickupItemSavedData.WasInHands = false;
						
						pickupItemSavedData.Position = DragableObject.transform.position;
						pickupItemSavedData.Rotation = DragableObject.transform.rotation.eulerAngles;
						//Physics.IgnoreCollision(DragableObject.collider, this.collider, true);
						//Debug.Log("DISABLING COLLISION");
						
						CurrentAction = ActionId.DragItemAround;
						IsDetectingMouseMoveForDrag = false;

						DragableObject.layer = LayerMask.NameToLayer("IgnoreCollisionWithCharacter");
					}
					
					
					
					Debug.Log("SELECTED OBJECT:" + detectDragHit.collider.name);
					
					DragableObject.GetComponent<BoxCollider>().enabled = false;
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
								Debug.Log("swipe moving right: " + SwipesDetectedCount.ToString());
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
								Debug.Log("swipe moving left: " + SwipesDetectedCount.ToString());
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
							Debug.Log("SWIPE DETECTED!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
							IsDetectingSwipeRight = !IsDetectingSwipeRight;
							SwipesDetectedCount = 0;
							

							bool cleanedShit = false;
							for(int i=0;i<TouchesObjcesWhileSwiping.Count;++i)
							{
								if(TouchesObjcesWhileSwiping[i].tag == "Shit" || TouchesObjcesWhileSwiping[i].tag == "Items")
								{
									if(TouchesObjcesWhileSwiping[i].tag == "Shit") cleanedShit = true;

									GroundItems.Remove(TouchesObjcesWhileSwiping[i]);
									Destroy(TouchesObjcesWhileSwiping[i]);
									TouchesObjcesWhileSwiping.RemoveAt(i);
									i--;
								}
							}

						if(cleanedShit) UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.CleanPooPiss);

												
						if(TouchesObjcesWhileSwiping.Contains(this.gameObject) && !cleanedShit)
						{
							int random = UnityEngine.Random.Range(0, 5);
							if(random == 0) animationController.IsIdleLook1 = true;
							else if(random == 1) animationController.IsIdleLook2 = true;
							else if(random == 2) animationController.IsIdleLook3 = true;
							else if(random == 3) animationController.IsIdleWave = true;
							
							else if(random == 4) animationController.IsTickled = true;
						}
					}

				
				SwipeHistoryPositions.Add(Input.mousePosition);
				if(SwipeHistoryPositions.Count >= 100)
				{
					SwipeHistoryPositions.RemoveAt(0);
					Debug.Log("TOO MANY!!");
				}
				//}
				

				

					/*
					List<GameObject> TouchedUnttouchedSwipeList = new List<GameObject>();
					// Consider this object for swiped
					if(hadRayCollision && (hitInfo.collider.tag == "Items" || hitInfo.collider.tag == "Shit"))
					{
						if(!TouchedUnttouchedSwipeList.Contains(hitInfo.collider.gameObject))
						{
							TouchedUnttouchedSwipeList.Remove(hitInfo.collider.gameObject);
						}
						else
						{
							if(!TouchedUnttouchedSwipeList.Contains(hitInfo.collider.gameObject))
								TouchedUnttouchedSwipeList.Add(hitInfo.collider.gameObject);
						}
					}
					else
					{
						for(int i=0;i<TouchedUnttouchedSwipeList.Count;++i)
						{
							SwipeHitsPerObject[TouchedUnttouchedSwipeList[i]]++;
						}

						TouchedUnttouchedSwipeList.Clear();
					}*/



				}
				else if (Input.GetButtonUp("Fire1")) 
				{
					Debug.Log("Input.GetButtonUp(Fire1)");
					IsDetectFlick = false;
					IsDetectingMouseMoveForDrag = false;

				if(UIGlobalVariablesScript.Singleton.DragableUI3DObject.transform.childCount == 1 && UIGlobalVariablesScript.Singleton.DragableUI3DObject.transform.GetChild(0).name == "Broom")
				{
					if(hadRayCollision != null && (hitInfo.collider.tag == "Items" || hitInfo.collider.tag == "Shit") && GroundItems.Contains(hitInfo.collider.gameObject))
					{
						GroundItems.Remove(hitInfo.collider.gameObject);
						Destroy(hitInfo.collider.gameObject);
						
					}
				}
				else if (!AtLeastOneSwipeDetected && hadRayCollision/* && !TriggeredHoldAction*/)
					{

						if(hitInfo.collider.name.StartsWith("MainCharacter") || hitInfo.collider.gameObject == ObjectHolding)
						{
							if(ObjectHolding != null && !ObjectHolding.GetComponent<ReferencedObjectScript>().Reference.GetComponent<UIPopupItemScript>().NonInteractable)
							{
								UIPopupItemScript item = ObjectHolding.GetComponent<ReferencedObjectScript>().Reference.GetComponent<UIPopupItemScript>();

								if(OnInteractWithPopupItem(item))
								{
									Destroy(ObjectHolding);
									ObjectHolding = null;
									animationController.IsHoldingItem = false;
									
								}
							}
							else if(ObjectHolding == null && animationController.IsAnyIdleAnimationPlaying())
							{
								animationController.IsTickled = true;
								UIGlobalVariablesScript.Singleton.SoundEngine.Play(CreaturePlayerId, CreatureSoundId.Tickle);
							}
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
								ObjectHolding.transform.parent = ActiveARScene.transform;
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
							if(UIGlobalVariablesScript.Singleton.Item3DPopupMenu.activeInHierarchy && (LastKnownObjectWithMenuUp == moveHitInfo.collider.gameObject))
							{
								isItemAlreadyOn = true;
							}

							if(RequestedToMoveToCounter == 1 && !isItemAlreadyOn  && (moveHitInfo.collider.GetComponent<ReferencedObjectScript>().Reference.GetComponent<UIPopupItemScript>().Menu != MenuFunctionalityUI.None))
							{
								if( moveHitInfo.collider.GetComponent<ReferencedObjectScript>().Reference.GetComponent<UIPopupItemScript>().Menu == MenuFunctionalityUI.Clock)
								{
									UIGlobalVariablesScript.Singleton.Item3DPopupMenu.GetComponent<UIWidget>().SetAnchor(hitInfo.collider.gameObject);
									//TriggeredHoldAction = true;
									UIGlobalVariablesScript.Singleton.Item3DPopupMenu.SetActive(true);
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
							UIGlobalVariablesScript.Singleton.Item3DPopupMenu.SetActive(false);
		
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
					if(!IsMovingTowardsLocation && !animationController.IsWakingUp && ObjectHolding == null && Hungry <= 70)
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
					DragableObject.transform.parent = ActiveARScene.transform;
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

				if(hadRayCollision && hitInfo.collider.name.StartsWith("Invisible Ground Plane"))
				{
					Debug.Log("DRAGGING");
					DragableObject.transform.position = hitInfo.point;
					//DragableObject.transform.parent = hit.transform;
				}

				if(!Input.GetButton("Fire1"))
				{
					CurrentAction = ActionId.DropItem;
				}
				

				break;
			}
		}



		if(UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimateCharacterOutPortalScript>().JumbId != AnimateCharacterOutPortalScript.JumbStateId.None 
		   || CurrentAction == ActionId.EnterPortalToAR 
		   || CurrentAction == ActionId.EnterPortalToNonAR 
		   || animationController.IsEnterPortal 
		   || animationController.IsExitPortal)
		{
			animationController.IsSad = false;
			animationController.IsNotWell = false;
			animationController.IsHungry = false;
			sadUnwellLoopState = HungrySadUnwellLoopId.DetectAnimation;
		}
		else
		{
			switch(sadUnwellLoopState)
			{
			case HungrySadUnwellLoopId.DetectAnimation:
			{
				if(ObjectHolding == null)
				{
					if(Hungry <= 50 && animationController.IsIdle)
					{
						animationController.IsHungry = true;
						TimeForNextHungryUnwellSadAnimation = UnityEngine.Random.Range(6.0f, 10.0f);
						LengthOfHungryUnwellSadAnimation = UnityEngine.Random.Range(3.0f, 5.0f);
						sadUnwellLoopState = HungrySadUnwellLoopId.PlayHungry;
						UIGlobalVariablesScript.Singleton.SoundEngine.Play(CreaturePlayerId, CreatureSoundId.Hungry);
						
					}
					else if(Health <= 50 && animationController.IsIdle)
					{
						animationController.IsNotWell = true;
						TimeForNextHungryUnwellSadAnimation = UnityEngine.Random.Range(6.0f, 10.0f);
						LengthOfHungryUnwellSadAnimation = UnityEngine.Random.Range(3.0f, 5.0f);
						sadUnwellLoopState = HungrySadUnwellLoopId.PlayUnwell;
						UIGlobalVariablesScript.Singleton.SoundEngine.Play(CreaturePlayerId, CreatureSoundId.Unwell);
						
					}
					/*else if(Happy <= 50 && animationController.IsIdle)
					{
						animationController.IsSad = true;
						TimeForNextHungryUnwellSadAnimation = UnityEngine.Random.Range(6.0f, 10.0f);
						LengthOfHungryUnwellSadAnimation = UnityEngine.Random.Range(3.5f, 5.6f);
						sadUnwellLoopState = HungrySadUnwellLoopId.PlaySad;
					UIGlobalVariablesScript.Singleton.SoundEngine.Play(CreaturePlayerId, CreatureSoundId.Happy1);
					}*/
					
				}
				
				break;
			}
				
			case HungrySadUnwellLoopId.PlayHungry:
			case HungrySadUnwellLoopId.PlaySad:
			case HungrySadUnwellLoopId.PlayUnwell:
			{
				
				LengthOfHungryUnwellSadAnimation -= Time.deltaTime;
				if(LengthOfHungryUnwellSadAnimation <= 0)
				{
					animationController.IsSad = false;
					animationController.IsNotWell = false;
					animationController.IsHungry = false;
					sadUnwellLoopState = HungrySadUnwellLoopId.OnCooldown;
				}
				
				break;
			}
				
			case HungrySadUnwellLoopId.OnCooldown:
			{
				TimeForNextHungryUnwellSadAnimation -= Time.deltaTime;
				
				if(TimeForNextHungryUnwellSadAnimation <= 0)
				{
					sadUnwellLoopState = HungrySadUnwellLoopId.DetectAnimation;
				}
				
				break;
			}
				
			}
		}





		if((DateTime.Now - LastTimeToilet).TotalSeconds >= 90 && !animationController.IsSleeping && animationController.IsIdle)
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

			newPoo.transform.parent = ActiveARScene.transform;
			newPoo.transform.position = this.transform.position;
			newPoo.transform.rotation = Quaternion.Euler(0, 180 + UnityEngine.Random.Range(-30.0f, 30.0f), 0);

			GroundItems.Add(newPoo);

			int sign = -1;
			if(UnityEngine.Random.Range(0, 2) == 0) sign = 1;

			MoveTo(this.transform.position + new Vector3(UnityEngine.Random.Range(-40, 40), 0, UnityEngine.Random.Range(30, 40) * sign), false);

			LastTimeToilet = DateTime.Now;
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

			/*float d1 = Vector3.Distance(TravelLocation, this.transform.position);
//			Debug.Log(d1.ToString() + "   _   " + TravelDistance.ToString());
			//Debug.Log(Vector3.Distance(TravelLocation, this.transform.position).ToString());

			if(d1 >= TravelDistance)
			{
				Stop();
			}*/
		}
		else
		{

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
	
	
		DragedObjectedFromUIToWorld = false;
		lastActionId = CurrentAction;
		HadUITouchLastFrame = hadUItouch;
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

		//if(PlayerPrefs.HasKey("Evolution"))
		//	Evolution = PlayerPrefs.GetFloat("Evolution");

		if(PlayerPrefs.HasKey("EvolutionStage"))
			EvolutionStage = PlayerPrefs.GetInt("EvolutionStage");

		if(PlayerPrefs.HasKey("ZefTokens"))
			ZefTokens = PlayerPrefs.GetInt("ZefTokens");

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

	public bool OnInteractWithPopupItem(UIPopupItemScript item)
	{
		switch(item.Type)
		{
			case PopupItemType.Food:
			{
			/*if(Hungry >= 95)
			{
				animationController.IsNo = true;
				return false;
			}
			else
			{*/
				ShowText("yum yum");
				Hungry += item.Points;
				Stop(true);
				animationController.IsEating = true;
			UIGlobalVariablesScript.Singleton.SoundEngine.Play(CreaturePlayerId, CreatureSoundId.Feed);
			//}
				break;
			}

			case PopupItemType.Item:
			{
				ShowText("I can't use this item");
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
				ShowText("I feel good");
				Health += item.Points;
				Stop(true);
				animationController.IsTakingPill = true;
			UIGlobalVariablesScript.Singleton.SoundEngine.Play(CreaturePlayerId, CreatureSoundId.EatPill);
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
		Debug.Log("Moving to point: " + location.ToString());
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
