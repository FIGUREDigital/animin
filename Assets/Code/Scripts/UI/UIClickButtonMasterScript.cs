using UnityEngine;
using System.Collections;

public class UIClickButtonMasterScript : MonoBehaviour 
{
	public UIFunctionalityId FunctionalityId;
	private float SavedRadius;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDoubleClick()
	{
		HandleDoubleClick(FunctionalityId);
	}

	void OnClick()
	{
		HandleClick(FunctionalityId);
	}

	void HandleDoubleClick(UIFunctionalityId id)
	{
		GameObject mainMenuPopupRef = UIGlobalVariablesScript.Singleton.MainMenuPopupObjectRef;
		Debug.Log("BUTTON: " + id.ToString());
		switch(id)
		{
		case UIFunctionalityId.ClearAllGroundItems:
		{
			CharacterProgressScript script = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>();
			
			for(int i=0;i<script.GroundItems.Count;++i)
			{
				Destroy(script.GroundItems[i]);
			}
			
			script.GroundItems.Clear();
			UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.CleanPooPiss);
			
			break;
		}
		}
	}

	void HandleClick(UIFunctionalityId id)
	{
		GameObject mainMenuPopupRef = UIGlobalVariablesScript.Singleton.MainMenuPopupObjectRef;
		Debug.Log("BUTTON: " + id.ToString());
		switch(id)
		{
		case UIFunctionalityId.None:
		{
			Debug.Log("You clicked on a button that does nothing. ");
			break;
		}
			
		case UIFunctionalityId.OpenCloseFoods:
		case UIFunctionalityId.OpenCloseItems:
		case UIFunctionalityId.OpenCloseMedicine:
		{
			if(mainMenuPopupRef.activeInHierarchy && UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef != this.gameObject)
			{

			}
			else
			{
				mainMenuPopupRef.SetActive(!mainMenuPopupRef.activeInHierarchy);
			}
			UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef = this.gameObject;

			UIGlobalVariablesScript.Singleton.PanelFoods.SetActive(false);
			UIGlobalVariablesScript.Singleton.PanelMedicine.SetActive(false);
			UIGlobalVariablesScript.Singleton.PanelItems.SetActive(false);

			if(id == UIFunctionalityId.OpenCloseFoods) 
			{
				UIGlobalVariablesScript.Singleton.PanelFoods.SetActive(true);
				UIGlobalVariablesScript.Singleton.PopupIndicator.transform.localPosition = new Vector3(-266, UIGlobalVariablesScript.Singleton.PopupIndicator.transform.localPosition.y, 0);
			}
			else if(id == UIFunctionalityId.OpenCloseMedicine) 
			{
				UIGlobalVariablesScript.Singleton.PanelMedicine.SetActive(true);
				UIGlobalVariablesScript.Singleton.PopupIndicator.transform.localPosition = new Vector3(266, UIGlobalVariablesScript.Singleton.PopupIndicator.transform.localPosition.y, 0);
			}
			else if(id == UIFunctionalityId.OpenCloseItems) 
			{
				UIGlobalVariablesScript.Singleton.PanelItems.SetActive(true);
				UIGlobalVariablesScript.Singleton.PopupIndicator.transform.localPosition = new Vector3(0, UIGlobalVariablesScript.Singleton.PopupIndicator.transform.localPosition.y, 0);
			}

			break;
		}
			
		case UIFunctionalityId.SetActiveItemOnBottomBarAndClose:
		{
			mainMenuPopupRef.SetActive(false);

			Debug.Log("BUTTON CLICL!!!");

			UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef.GetComponent<ReferencedObjectScript>().Reference = this.gameObject;
			
			//string spriteName = UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef.GetComponent<UISprite>().spriteName;
			UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef.GetComponent<UISprite>().spriteName = this.GetComponent<UISprite>().spriteName;
			UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef.GetComponent<UIButton>().normalSprite = this.GetComponent<UISprite>().spriteName;
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

			if(TrackVuforiaScript.IsTracking)
			{
				UIGlobalVariablesScript.Singleton.MinigamesMenuMasterScreenRef.SetActive(true);
				UIGlobalVariablesScript.Singleton.MinigameMenuScreeRef.SetActive(true);
			}
			else
			{
				UIGlobalVariablesScript.Singleton.RequiresGamecardScreenRef.SetActive(true);

			}



			
			break;
		}
			
		case UIFunctionalityId.BackFromMinigames:
		{
			UIGlobalVariablesScript.Singleton.MinigamesMenuMasterScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.CaringScreenRef.SetActive(true);
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().enabled = true;
			UIGlobalVariablesScript.Singleton.MainCharacterAnimationControllerRef.gameObject.GetComponent<ObjectLookAtDeviceScript>().enabled = true;
	
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<JoystiqScript>().DisableJoystick();
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<JoystiqScript>().enabled = false;
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterController>().radius = SavedRadius;
			UIGlobalVariablesScript.Singleton.ARSceneRef.SetActive(true);

			UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localRotation = Quaternion.Euler(0, 180, 0);
			
			break;
		}
			
		case UIFunctionalityId.PlayMinigameSpaceship:
		case UIFunctionalityId.PlayMinigameCubeRunners:
		case UIFunctionalityId.PlayMinigameGunFighters:
		case UIFunctionalityId.PlayMinigameUnknown:
		{
			UIGlobalVariablesScript.SelectedMinigameToPlay = FunctionalityId;
			UIGlobalVariablesScript.Singleton.StartMinigameScreenRef.SetActive(true);
			UIGlobalVariablesScript.Singleton.MinigameMenuScreeRef.SetActive(false);
			break;
		}
			
		case UIFunctionalityId.OpenStatsScreen:
		{
			UIGlobalVariablesScript.Singleton.CaringScreenRef.SetActive(false);
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

			UIGlobalVariablesScript.Singleton.InsideMinigamesMasterScreenRef.SetActive(true);
			UIGlobalVariablesScript.Singleton.MinigamesMenuMasterScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.SpaceshipGameScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.CuberunnerGamesScreenRef.SetActive(false);

			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().enabled = false;
			UIGlobalVariablesScript.Singleton.MainCharacterAnimationControllerRef.gameObject.GetComponent<ObjectLookAtDeviceScript>().enabled = false;

			UIGlobalVariablesScript.Singleton.ARSceneRef.SetActive(false);

			switch(UIGlobalVariablesScript.SelectedMinigameToPlay)
			{
				case UIFunctionalityId.PlayMinigameSpaceship:
				{
					UIGlobalVariablesScript.Singleton.SpaceshipGameScreenRef.SetActive(true);
					UIGlobalVariablesScript.Singleton.SpaceshipMinigameSceneRef.SetActive(true);
					
					break;
				}

				case UIFunctionalityId.PlayMinigameCubeRunners:
				{
					UIGlobalVariablesScript.Singleton.CuberunnerGamesScreenRef.SetActive(true);
					UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.SetActive(true);
				UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.GetComponent<MinigameCollectorScript>().HardcoreReset();

				SavedRadius = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterController>().radius;
				UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterController>().radius = 0.51f;
				UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<JoystiqScript>().enabled = true;
				UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<JoystiqScript>().EnableJoystick();

				
				break;
				}
			}
			
			break;
		}
			
		case UIFunctionalityId.CloseCurrentMinigame:
		{
			// Disable UI
			UIGlobalVariablesScript.Singleton.InsideMinigamesMasterScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.SpaceshipGameScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.CuberunnerGamesScreenRef.SetActive(false);

			// Disable Scenes
			UIGlobalVariablesScript.Singleton.SpaceshipMinigameSceneRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.SetActive(false);

			HandleClick(UIFunctionalityId.BackFromMinigames);

			
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
			UIGlobalVariablesScript.Singleton.CaringScreenRef.SetActive(false);
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
			UIGlobalVariablesScript.Singleton.PicturesScreenRef.GetComponent<DeviceCameraScript>().SavePicture();
			break;
		}

		

		case UIFunctionalityId.ClearDragedItem:
		{

			
			break;
		}
			
		}
	}

	private void PopulatePopupItems()
	{

	}
}
