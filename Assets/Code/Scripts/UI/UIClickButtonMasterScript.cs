using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;


public class UIClickButtonMasterScript : MonoBehaviour 
{
	[DllImport("__Internal")]
	private static extern void _saveScreenshotToCameraRoll();

	public UIFunctionalityId FunctionalityId;
	private static float SavedRadius;
	//private Vector3 SavedScale;

	public static void PopulateInterfaceItems(PopupItemType typeToLoad, List<GameObject> allSprites)
	{
		List<GameObject> subItems = new List<GameObject>();
		GameObject itemsPanel = UIGlobalVariablesScript.Singleton.PanelItems;
		for(int i=0;i<itemsPanel.transform.childCount;++i)
		{
			GameObject lister = itemsPanel.transform.GetChild(i).gameObject;
			lister.SetActive(false);
			subItems.Add(lister);
		}
		
		List<InventoryItemData> inventoryItems = new List<InventoryItemData>();
		for(int i=0;i<PersistentData.Singleton.Inventory.Count;++i)
		{
//			Debug.Log(PersistentData.Singleton.Inventory[i].Id.ToString());
			if(InventoryItemData.Items[(int)PersistentData.Singleton.Inventory[i].Id].ItemType != typeToLoad) continue;
			inventoryItems.Add(PersistentData.Singleton.Inventory[i]);
		}
		
		
		
		int panelCount = 0;
		for(int i=0;i<inventoryItems.Count;i+=2)
		{
			subItems[panelCount].SetActive(true);
			subItems[panelCount].transform.GetChild(0).gameObject.SetActive(false);
			subItems[panelCount].transform.GetChild(1).gameObject.SetActive(false);

			GameObject sprite0 = subItems[panelCount].transform.GetChild(0).gameObject;
			Debug.Log(sprite0.name);

			subItems[panelCount].transform.GetChild(0).gameObject.GetComponent<UIButton>().normalSprite = InventoryItemData.Items[(int)inventoryItems[i + 0].Id].SpriteName;
			subItems[panelCount].transform.GetChild(0).gameObject.GetComponent<InterfaceItemLinkToModelScript>().Item3DPrefab = InventoryItemData.Items[(int)inventoryItems[i + 0].Id].PrefabId;
			subItems[panelCount].transform.GetChild(0).gameObject.GetComponent<InterfaceItemLinkToModelScript>().ItemID = InventoryItemData.Items[(int)inventoryItems[i + 0].Id].Id;
			subItems[panelCount].transform.GetChild(0).gameObject.SetActive(true);
			sprite0.transform.GetChild(0).GetChild(0).GetComponent<UILabel>().text = inventoryItems[i + 0].Count.ToString();

			if(inventoryItems[i + 0].Count == 1)
				sprite0.transform.GetChild(0).gameObject.SetActive(false);
			else
				sprite0.transform.GetChild(0).gameObject.SetActive(true);


			if(allSprites != null)
			{
				allSprites.Add(subItems[panelCount].transform.GetChild(0).gameObject);
			}

			if(inventoryItems.Count > i+1)
			{
				GameObject sprite1 = subItems[panelCount].transform.GetChild(1).gameObject;

				subItems[panelCount].transform.GetChild(1).gameObject.GetComponent<UIButton>().normalSprite = InventoryItemData.Items[(int)inventoryItems[i + 1].Id].SpriteName;
				subItems[panelCount].transform.GetChild(1).gameObject.GetComponent<InterfaceItemLinkToModelScript>().Item3DPrefab = InventoryItemData.Items[(int)inventoryItems[i + 1].Id].PrefabId;
				subItems[panelCount].transform.GetChild(1).gameObject.GetComponent<InterfaceItemLinkToModelScript>().ItemID = InventoryItemData.Items[(int)inventoryItems[i + 1].Id].Id;
				subItems[panelCount].transform.GetChild(1).gameObject.SetActive(true);
				sprite1.transform.GetChild(0).GetChild(0).GetComponent<UILabel>().text = inventoryItems[i + 1].Count.ToString();

				
				if(inventoryItems[i + 1].Count == 1)
					sprite1.transform.GetChild(0).gameObject.SetActive(false);
				else
					sprite1.transform.GetChild(0).gameObject.SetActive(true);

				if(allSprites != null)
				{
					allSprites.Add(subItems[panelCount].transform.GetChild(1).gameObject);
				}
			}
			
			panelCount++;
		}
	}


	void OnDoubleClick()
	{
		HandleDoubleClick(FunctionalityId);
	}

	void OnClick()
	{
		HandleClick(FunctionalityId, this.gameObject);
	}

	void OnPress (bool isPressed)
	{

		if(isPressed)
		{
			//Debug.Log("PRESS DETECTED");
			switch(FunctionalityId)
			{
				case UIFunctionalityId.JumbOnCubeRunner:
				{
					UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.Jump);

					UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterControllerScript> ().PressedJumb = true;
					
					if(UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.GetComponent<MinigameCollectorScript>().TutorialId == MinigameCollectorScript.TutorialStateId.ShowJumb)
						UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.GetComponent<MinigameCollectorScript>().AdvanceTutorial();


					break;
				}
			case UIFunctionalityId.ShootBullet:
			{
				UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.Jump);
				
				//UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().ShootBulletForward();
				break;
			}
			}
		}
	}

	void HandleDoubleClick(UIFunctionalityId id)
	{
		//GameObject mainMenuPopupRef = UIGlobalVariablesScript.Singleton.MainMenuPopupObjectRef;
		//Debug.Log("BUTTON: " + id.ToString());
		switch(id)
		{
		case UIFunctionalityId.ClearAllGroundItems:
		{
			CharacterProgressScript script = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>();
			
			for(int i=0;i<script.GroundItems.Count;++i)
			{
				if(script.GroundItems[i].GetComponent<UIPopupItemScript>() != null)
				{
					PersistentData.Singleton.AddItemToInventory(script.GroundItems[i].GetComponent<UIPopupItemScript>().Id, 1);
				}
				Destroy(script.GroundItems[i]);
			}
			
			script.GroundItems.Clear();
			UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.CleanPooPiss);
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().HidePopupMenus();

			for(int i=0;i<EDMMixerScript.Singleton.KeysOn.Length;++i)
			{
				EDMMixerScript.Singleton.KeysOn[i] = false;
			}

			break;
		}
		}
	}

	public static void SetSoundSprite()
	{
		if(PlayerProfileData.ActiveProfile.Settings.AudioEnabled)
			UIGlobalVariablesScript.Singleton.SoundSprite.GetComponent<UISprite>().spriteName = "pauseScreenSound";
		else
			UIGlobalVariablesScript.Singleton.SoundSprite.GetComponent<UISprite>().spriteName = "soundOff";
	}

	public static void HandleClick(UIFunctionalityId id, GameObject sender)
	{

		Debug.Log("BUTTON: " + id.ToString());
		switch(id)
		{
		case UIFunctionalityId.None:
		{
			Debug.Log("You clicked on a button that does nothing. ");
			break;
		}

		case UIFunctionalityId.GoToMainMenuFromGame:
		{
			Application.LoadLevel("Menu");

			break;
		}

		case UIFunctionalityId.AudioOnOffGame:
		{
			PlayerProfileData.ActiveProfile.Settings.AudioEnabled = !PlayerProfileData.ActiveProfile.Settings.AudioEnabled;

			SetSoundSprite();

			PlayerProfileData.ActiveProfile.Save();

			break;
		}
		case UIFunctionalityId.ResetAnimin:
		{
			PersistentData.Singleton.SetDefault(PersistentData.Singleton.PlayerAniminId);
			Application.LoadLevel("VuforiaTest");

			break;
		}
			
		case UIFunctionalityId.OpenCloseFoods:
		case UIFunctionalityId.OpenCloseItems:
		case UIFunctionalityId.OpenCloseMedicine:
		{
			GameObject mainMenuPopupRef = UIGlobalVariablesScript.Singleton.MainMenuPopupObjectRef;

			if(mainMenuPopupRef.activeInHierarchy && UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef != sender)
			{

			}
			else
			{
				mainMenuPopupRef.SetActive(!mainMenuPopupRef.activeInHierarchy);
			}
			UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef = sender;

			//UIGlobalVariablesScript.Singleton.PanelFoods.SetActive(false);
			//UIGlobalVariablesScript.Singleton.PanelMedicine.SetActive(false);
			UIGlobalVariablesScript.Singleton.PanelItems.SetActive(true);
			PopupItemType typeToLoad = PopupItemType.Food;

			if(id == UIFunctionalityId.OpenCloseFoods) 
			{
				typeToLoad = PopupItemType.Food;
				UIGlobalVariablesScript.Singleton.ItemsFoodMedicineLabel.text = "Foods";
				//UIGlobalVariablesScript.Singleton.PanelFoods.SetActive(true);
				UIGlobalVariablesScript.Singleton.PopupIndicator.transform.localPosition = new Vector3(-266, UIGlobalVariablesScript.Singleton.PopupIndicator.transform.localPosition.y, 0);
			}
			else if(id == UIFunctionalityId.OpenCloseMedicine) 
			{
				typeToLoad = PopupItemType.Medicine;
				UIGlobalVariablesScript.Singleton.ItemsFoodMedicineLabel.text = "Medicine";
				//UIGlobalVariablesScript.Singleton.PanelMedicine.SetActive(true);
				UIGlobalVariablesScript.Singleton.PopupIndicator.transform.localPosition = new Vector3(266, UIGlobalVariablesScript.Singleton.PopupIndicator.transform.localPosition.y, 0);
			}
			else if(id == UIFunctionalityId.OpenCloseItems) 
			{
				typeToLoad = PopupItemType.Item;
				UIGlobalVariablesScript.Singleton.ItemsFoodMedicineLabel.text = "Items";
				//UIGlobalVariablesScript.Singleton.PanelItems.SetActive(true);
				UIGlobalVariablesScript.Singleton.PopupIndicator.transform.localPosition = new Vector3(0, UIGlobalVariablesScript.Singleton.PopupIndicator.transform.localPosition.y, 0);
			}

			UIGlobalVariablesScript.Singleton.ItemScrollView.GetComponent<UIScrollView>().ResetPosition();


			PopulateInterfaceItems(typeToLoad, null);

			break;
		}
			
		case UIFunctionalityId.SetActiveItemOnBottomBarAndClose:
		{
			GameObject mainMenuPopupRef = UIGlobalVariablesScript.Singleton.MainMenuPopupObjectRef;
			mainMenuPopupRef.SetActive(false);

		//	Debug.Log("BUTTON CLICL!!!");

			//InventoryItemId itemId = UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef.GetComponent<InterfaceItemLinkToModelScript>().ItemID;



			UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef.GetComponent<InterfaceItemLinkToModelScript>().ItemID = 
				sender.GetComponent<InterfaceItemLinkToModelScript>().ItemID;
			
			//string spriteName = UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef.GetComponent<UISprite>().spriteName;
			UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef.GetComponent<UISprite>().atlas = sender.GetComponent<UISprite>().atlas;
			UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef.GetComponent<UISprite>().spriteName = sender.GetComponent<UISprite>().spriteName;
			UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef.GetComponent<UIButton>().normalSprite = sender.GetComponent<UISprite>().spriteName;
			//UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef.GetComponent<UISprite>().MakePixelPerfect();
			//UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef.GetComponent<UISprite>().MarkAsChanged();
			//this.GetComponent<UISprite>().spriteName = spriteName;
			//this.GetComponent<UISprite>().MarkAsChanged();

			//NGUITools.MarkParentAsChanged(UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef);
			
			break;
		}
			
		case UIFunctionalityId.OpenMinigamesScreen:
		{
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().Stop(true);

			UIGlobalVariablesScript.Singleton.CaringScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.StartMinigameScreenRef.SetActive(false);

			UIGlobalVariablesScript.Singleton.MinigamesMenuMasterScreenRef.SetActive(true);
			UIGlobalVariablesScript.Singleton.MinigameMenuScreeRef.SetActive(true);


			//TrackVuforiaScript.EnableDisableMinigamesBasedOnARStatus();

			
			break;
		}
			
		case UIFunctionalityId.BackFromMinigames:
		{
			UIGlobalVariablesScript.Singleton.MinigamesMenuMasterScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.CaringScreenRef.SetActive(true);


			break;
		}

		case UIFunctionalityId.CloseCurrentMinigame:
		{
			Application.LoadLevel("VuforiaTest");
			return;


			// Disable UI
			UIGlobalVariablesScript.Singleton.InsideMinigamesMasterScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.SpaceshipGameScreenRef.SetActive(false);         //HOME BUTTON ERROR
			UIGlobalVariablesScript.Singleton.CuberunnerGamesScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.GunGameInterface.SetActive(false);
			
			// Disable Scenes
			UIGlobalVariablesScript.Singleton.SpaceshipMinigameSceneRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.SetActive(false);

			if(UIGlobalVariablesScript.SelectedMinigameToPlay == UIFunctionalityId.PlayMinigameGunFighters)
				UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().CloseGame();

			//Debug.Log("SavedScale: " + SavedScale.ToString());
//			UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localScale = new Vector3(0.035f, 0.035f, 0.035f);
//			UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.position = Vector3.zero;
//			UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localPosition = new Vector3(0, 0.01f, 0);
//			UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.rotation = Quaternion.Euler(0, 180, 0);


			UIGlobalVariablesScript.Singleton.NonARWorldRef.SetActive(true);
			UIGlobalVariablesScript.Singleton.ARWorldRef.SetActive(true);

			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().enabled = true;
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<ObjectLookAtDeviceScript>().enabled = true;
			UIGlobalVariablesScript.Singleton.MainCharacterRef.SetActive(true);

			//UIGlobalVariablesScript.Singleton.Joystick.ResetJoystick();
			UIGlobalVariablesScript.Singleton.Joystick.gameObject.SetActive(false);	
			UIGlobalVariablesScript.Singleton.JoystickArt.SetActive(false);	

			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterController>().radius = UIClickButtonMasterScript.SavedRadius;
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterControllerScript>().FreezeCollisionDetection = false;
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterControllerScript>().Forces.Clear();			

			Debug.Log ("CloseCurrentMinigame");
			GameController.instance.StopGame();

			HandleClick(UIFunctionalityId.BackFromMinigames, sender);
			Camera.main.GetComponent<MusicScript>().Stop();

			if(TrackVuforiaScript.IsTracking)
			{
				UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.parent = UIGlobalVariablesScript.Singleton.ARSceneRef.transform;
				UIGlobalVariablesScript.Singleton.Vuforia.OnCharacterEnterARScene();
			}
			else
			{
				UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.parent = UIGlobalVariablesScript.Singleton.NonSceneRef.transform;
				UIGlobalVariablesScript.Singleton.Vuforia.OnCharacterEnterNonARScene();
			}


	
			break;
		}
			
		case UIFunctionalityId.PlayMinigameSpaceship:
		case UIFunctionalityId.PlayMinigameCubeRunners:
		case UIFunctionalityId.PlayMinigameGunFighters:
		case UIFunctionalityId.PlayMinigameUnknown:
		{
			UIGlobalVariablesScript.SelectedMinigameToPlay = sender.GetComponent<UIClickButtonMasterScript>().FunctionalityId;

			GameObject.Find("MultiplayerObject").GetComponent<GameController>().SetSinglePlayer();
			HandleClick(UIFunctionalityId.StartSelectedMinigame, sender);

			//UIGlobalVariablesScript.Singleton.StartMinigameScreenRef.SetActive(true);
			//UIGlobalVariablesScript.Singleton.MinigameMenuScreeRef.SetActive(false);


			break;
		}

		case UIFunctionalityId.PlaySinglePlayer:
		{
			GameObject.Find("MultiplayerObject").GetComponent<GameController>().SetSinglePlayer();
			HandleClick(UIFunctionalityId.StartSelectedMinigame, sender);


			break;
		}
		case UIFunctionalityId.PlayMultiplayer:
		{
			UIGlobalVariablesScript.Singleton.MultiplayerOptionsScreen.SetActive(true);
			UIGlobalVariablesScript.Singleton.StartMinigameScreenRef.SetActive(false);

			//HandleClick(UIFunctionalityId.StartSelectedMinigame, sender);
			break;
		}
		case UIFunctionalityId.BackFromStartGameOptions:
		{
			UIGlobalVariablesScript.Singleton.MultiplayerOptionsScreen.SetActive(false);
			UIGlobalVariablesScript.Singleton.StartMinigameScreenRef.SetActive(true);
			
			//HandleClick(UIFunctionalityId.StartSelectedMinigame, sender);
			break;
		}
		case UIFunctionalityId.JoinRandomGame:
		{
			GameObject.Find("MultiplayerObject").GetComponent<GameController>().SetMultiplayerJoinRandom();
			break;
		}
		case UIFunctionalityId.StartFriendGame:
		{
			GameObject.Find("MultiplayerObject").GetComponent<GameController>().SetMultiplayerStartFriendGame();
			break;
		}
		case UIFunctionalityId.JoinFriendGame:
		{
			UIGlobalVariablesScript.Singleton.EnterFriendsNameChat.SetActive(true);
			break;
		}

		case UIFunctionalityId.ShowLeaderboards:
		{
			break;
		}
			
		case UIFunctionalityId.OpenStatsScreen:
		{
			UIGlobalVariablesScript.Singleton.CaringScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.StatsScreenRef.SetActive(true);
			
			break;
		}

		case UIFunctionalityId.BackToStatsFromAchievement:
		{
			UIGlobalVariablesScript.Singleton.AchievementsScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.StatsScreenRef.SetActive(true);
			break;
		}
			
		case UIFunctionalityId.BackToCaringFromStats:
		{
			UIGlobalVariablesScript.Singleton.StatsScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.CaringScreenRef.SetActive(true);
			
			break;
		}
			
		case UIFunctionalityId.OpenPictureScreen:
		{
			UIGlobalVariablesScript.Singleton.PicturesScreenRef.SetActive(true);
			UIGlobalVariablesScript.Singleton.CaringScreenRef.SetActive(false);

			UIGlobalVariablesScript.Singleton.PicturesScreenRef.GetComponent<DeviceCameraScript>().ResetCamera();

			break;
		}
			
		case UIFunctionalityId.ClosePictureScreen:
		{
			UIGlobalVariablesScript.Singleton.PicturesScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.CaringScreenRef.SetActive(true);
			UIGlobalVariablesScript.Singleton.PicturesScreenRef.GetComponent<DeviceCameraScript>().Stop();
			break;
		}
			
		case UIFunctionalityId.CloseStartMinigameScreen:
		{
			UIGlobalVariablesScript.Singleton.StartMinigameScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.MinigameMenuScreeRef.SetActive(true);
			
			break;
		}
			
		case UIFunctionalityId.StartSelectedMinigame:
		{
			//SavedScale = UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localScale;
			//Debug.Log("SavedScale 1:" + SavedScale.ToString());

			Application.LoadLevel("VuforiaTestMinigame1");
			break;

			UIGlobalVariablesScript.Singleton.ARWorldRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.NonARWorldRef.SetActive(false);

			UIGlobalVariablesScript.Singleton.PauseGameButton.SetActive(true);
			UIGlobalVariablesScript.Singleton.PausedScreen.SetActive(false);

			UIGlobalVariablesScript.Singleton.InsideMinigamesMasterScreenRef.SetActive(true);
			UIGlobalVariablesScript.Singleton.MinigamesMenuMasterScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.SpaceshipGameScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.CuberunnerGamesScreenRef.SetActive(false);

			//UIGlobalVariablesScript.Singleton.MainCharacterRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().enabled = false;
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<ObjectLookAtDeviceScript>().enabled = false;
//
//			UIGlobalVariablesScript.Singleton.Joystick.CharacterAnimationRef = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimationControllerScript>();
//			UIGlobalVariablesScript.Singleton.Joystick.CharacterControllerRef = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterControllerScript>();


			//UIGlobalVariablesScript.Singleton.ARSceneRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.JumbButton.SetActive(false);
			UIGlobalVariablesScript.Singleton.ShootButton.SetActive(false);

			UIGlobalVariablesScript.Singleton.Shadow.transform.localScale = new Vector3(0.79f, 0.79f, 0.79f);

			switch(UIGlobalVariablesScript.SelectedMinigameToPlay)
			{
				case UIFunctionalityId.PlayMinigameSpaceship:
				{
					UIGlobalVariablesScript.Singleton.SpaceshipGameScreenRef.SetActive(true);
					UIGlobalVariablesScript.Singleton.SpaceshipMinigameSceneRef.SetActive(true);
					
					break;
				}

			case UIFunctionalityId.PlayMinigameGunFighters:
			{
				UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().Reset();
				//UIGlobalVariablesScript.Singleton.CuberunnerGamesScreenRef.SetActive(true);
				UIGlobalVariablesScript.Singleton.GunGameScene.SetActive(true);
				UIGlobalVariablesScript.Singleton.GunGameInterface.SetActive(true);
				//UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.GetComponent<MinigameCollectorScript>().HardcoreReset();
				UIGlobalVariablesScript.Singleton.GunGameScene.transform.localPosition = Vector3.zero;
				UIClickButtonMasterScript.SavedRadius = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterController>().radius;
				//UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterController>().radius = 0.51f;
				
				UIGlobalVariablesScript.Singleton.Joystick.gameObject.SetActive(true);	
				UIGlobalVariablesScript.Singleton.JoystickArt.SetActive(true);	

				
				UIGlobalVariablesScript.Singleton.Joystick.CharacterAnimationRef = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<MinigameAnimationControllerScript>();
				UIGlobalVariablesScript.Singleton.Joystick.CharacterControllerRef = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterControllerScript>();

				
				//UIGlobalVariablesScript.Singleton.ShootButton.SetActive(true);

				UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.parent = UIGlobalVariablesScript.Singleton.GunGameScene.transform;
				
				//UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localScale = new Vector3(0.026f, 0.026f, 0.025f);
//				Camera.main.GetComponent<MusicScript>().PlayGun();

				break;
			}

				case UIFunctionalityId.PlayMinigameCubeRunners:
				{
					UIGlobalVariablesScript.Singleton.CuberunnerGamesScreenRef.SetActive(true);
					UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.SetActive(true);
					UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.GetComponent<MinigameCollectorScript>().HardcoreReset();

					UIClickButtonMasterScript.SavedRadius = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterController>().radius;
					UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterController>().radius = 0.51f;

					UIGlobalVariablesScript.Singleton.Joystick.gameObject.SetActive(true);	
					UIGlobalVariablesScript.Singleton.JoystickArt.SetActive(true);	


					UIGlobalVariablesScript.Singleton.Joystick.CharacterAnimationRef = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<MinigameAnimationControllerScript>();
					UIGlobalVariablesScript.Singleton.Joystick.CharacterControllerRef = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterControllerScript>();


					//UIGlobalVariablesScript.Singleton.Joystick.ResetJoystick();

					UIGlobalVariablesScript.Singleton.JumbButton.SetActive(true);

					UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.parent = UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.transform;
					
					UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localScale = new Vector3(0.026f, 0.026f, 0.025f);
//					Camera.main.GetComponent<MusicScript>().PlayCube();
				
					break;
				}
			}


			
			break;
		}


		case UIFunctionalityId.PauseGame:
		{
			UIGlobalVariablesScript.Singleton.PauseGameButton.SetActive(false);
			UIGlobalVariablesScript.Singleton.PausedScreen.SetActive(true);
			break;
		}

		case UIFunctionalityId.ResumeGame:
		{
			UIGlobalVariablesScript.Singleton.PauseGameButton.SetActive(true);
			UIGlobalVariablesScript.Singleton.PausedScreen.SetActive(false);
			break;
		}

		case UIFunctionalityId.CloseSettings:
		{
			UIGlobalVariablesScript.Singleton.SettingsScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.StatsScreenRef.SetActive(true);

			break;
		}
		case UIFunctionalityId.OpenSettings:
		{
			UIGlobalVariablesScript.Singleton.AchievementsScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.SettingsScreenRef.SetActive(true);
			UIGlobalVariablesScript.Singleton.StatsScreenRef.SetActive(false);
			break;
		}

		case UIFunctionalityId.CloseGamecard:
		{
			UIGlobalVariablesScript.Singleton.RequiresGamecardScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.CaringScreenRef.SetActive(true);
			break;
		}

		case UIFunctionalityId.ShowAchievements:
		{
			UIGlobalVariablesScript.Singleton.StatsScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.AchievementsScreenRef.SetActive(true);

			break;
		}

		case UIFunctionalityId.CloseAchivements:
		{
			UIGlobalVariablesScript.Singleton.CaringScreenRef.SetActive(true);
			UIGlobalVariablesScript.Singleton.AchievementsScreenRef.SetActive(false);
			break;
		}

		case UIFunctionalityId.ChangeCameraFacingMode:
		{
			UIGlobalVariablesScript.Singleton.ImageTarget.GetComponent<TrackVuforiaScript>().FlipFrontBackCamera();

			break;
		}

		case UIFunctionalityId.TakePicture:
		{
//			if (Application.platform == RuntimePlatform.IPhonePlayer) 
//			{
//				GameObject.Find("Aperture").GetComponent<Aperture>().Photo();
//			}

			break;
		}

		case UIFunctionalityId.LighbulbSwitch:
		{
			UIWidget widget = UIGlobalVariablesScript.Singleton.LightbulbUI.GetComponent<UIWidget>();

			Debug.Log(widget.leftAnchor.target.gameObject.name);
			LighbulbSwitchOnOffScript script = widget.leftAnchor.target.gameObject.GetComponent<LighbulbSwitchOnOffScript>();


			if(UIGlobalVariablesScript.Singleton.LightbulbGreenButton.activeInHierarchy)
			{
				UIGlobalVariablesScript.Singleton.LightbulbGreenButton.SetActive(false);
				UIGlobalVariablesScript.Singleton.LightbulbRedButton.SetActive(true);

				UIGlobalVariablesScript.Singleton.LightbulbSwitchButton.transform.localPosition = new Vector3(-42, 198, 0);
				script.SetOff();
			}
			else
			{
				UIGlobalVariablesScript.Singleton.LightbulbGreenButton.SetActive(true);
				UIGlobalVariablesScript.Singleton.LightbulbRedButton.SetActive(false);

				UIGlobalVariablesScript.Singleton.LightbulbSwitchButton.transform.localPosition = new Vector3(27, 198, 0);
				script.SetOn();
			}



			break;
		}

		case UIFunctionalityId.ShowAlbum:
		{
			//GameObject.Find("KamcordPrefab").GetComponent<RecordingGUI>().ShowVideos();
			break;
		}

		case UIFunctionalityId.RecordVideo:
		{
//			GameObject.Find("KamcordPrefab").GetComponent<RecordingGUI>().StartRecording();
			sender.GetComponent<UIClickButtonMasterScript>().FunctionalityId = UIFunctionalityId.StopRecordVideo;
			sender.GetComponent<UISprite>().spriteName = "stopvideo";
			sender.GetComponent<UIButton>().normalSprite = "stopvideo";
			break;
		}

		case UIFunctionalityId.StopRecordVideo:
		{
			//GameObject.Find("KamcordPrefab").GetComponent<RecordingGUI>().StopRecording();
			sender.GetComponent<UIClickButtonMasterScript>().FunctionalityId = UIFunctionalityId.RecordVideo;
			sender.GetComponent<UISprite>().spriteName = "video 1";
			sender.GetComponent<UIButton>().normalSprite = "video 1";
			break;
		}


		case UIFunctionalityId.ClearDragedItem:
		{

			
			break;
		}

		case UIFunctionalityId.ResumeInterruptedMinigame:
		{
			UIGlobalVariablesScript.Singleton.MinigameInterruptedMenu.SetActive(false);

			break;
		}

		case UIFunctionalityId.ExitInterruptedMinigame:
		{
			UIGlobalVariablesScript.Singleton.MinigameInterruptedMenu.SetActive(false);
			HandleClick(UIFunctionalityId.CloseCurrentMinigame, sender);
			UIGlobalVariablesScript.Singleton.Vuforia.OnTrackingLost();
			break;
		}


		case UIFunctionalityId.PlayPauseSong:
		{
			UIGlobalVariablesScript.Singleton.UIRoot.GetComponent<MediaPlayerPluginScript>().PlayPause();
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimationControllerScript>().IsDance = true;

			break;
		}

		case UIFunctionalityId.NextSong:
		{
			UIGlobalVariablesScript.Singleton.UIRoot.GetComponent<MediaPlayerPluginScript>().NextSong();
			break;
		}

		case UIFunctionalityId.PreviousSong:
		{
			UIGlobalVariablesScript.Singleton.UIRoot.GetComponent<MediaPlayerPluginScript>().PreviousSong();
			break;
		}

		case UIFunctionalityId.ViewTracklist:
		{

			UIGlobalVariablesScript.Singleton.TracklistPanel.SetActive(true);
			UIGlobalVariablesScript.Singleton.PlaySongPanel.SetActive(false);

			break;
		}

		case UIFunctionalityId.ReturnFromTracklist:
		{
			UIGlobalVariablesScript.Singleton.TracklistPanel.SetActive(false);
			UIGlobalVariablesScript.Singleton.PlaySongPanel.SetActive(true);
			
			break;
		}

		case UIFunctionalityId.SelectedTrack:
		{
			UIGlobalVariablesScript.Singleton.TracklistPanel.SetActive(false);
			UIGlobalVariablesScript.Singleton.PlaySongPanel.SetActive(true);

			UIGlobalVariablesScript.Singleton.UIRoot.GetComponent<MediaPlayerPluginScript>().PlaySongAtIndex(
				sender.GetComponent<TrackSongIndexScript>().TrackIndex);


			break;
		}

		}
	}

	private void PopulatePopupItems()
	{

	}
}
