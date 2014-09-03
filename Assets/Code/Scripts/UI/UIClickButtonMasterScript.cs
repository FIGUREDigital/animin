using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Linq;


public class UIClickButtonMasterScript : MonoBehaviour 
{
	[DllImport("__Internal")]
	private static extern void _saveScreenshotToCameraRoll();

	public UIFunctionalityId FunctionalityId;
	private static float SavedRadius;
	//private Vector3 SavedScale;


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
					break;
				}
			case UIFunctionalityId.ShootBullet:
			{
				UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.Jump);
				
				UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().ShootBulletForward();
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
				Destroy(script.GroundItems[i]);
			}
			
			script.GroundItems.Clear();
			UIGlobalVariablesScript.Singleton.SoundEngine.Play(GenericSoundId.CleanPooPiss);
			UIGlobalVariablesScript.Singleton.Item3DPopupMenu.SetActive(false);
			UIGlobalVariablesScript.Singleton.StereoUI.SetActive(false);
			UIGlobalVariablesScript.Singleton.LightbulbUI.SetActive(false);

			break;
		}
		}
	}

	public static void HandleClick(UIFunctionalityId id, GameObject sender)
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
			if(mainMenuPopupRef.activeInHierarchy && UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef != sender)
			{

			}
			else
			{
				mainMenuPopupRef.SetActive(!mainMenuPopupRef.activeInHierarchy);
			}
			UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef = sender;

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

			UIGlobalVariablesScript.ButtonTriggeredMainMenuPopupRef.GetComponent<ReferencedObjectScript>().Reference = sender;
			
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
			// Disable UI
			UIGlobalVariablesScript.Singleton.InsideMinigamesMasterScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.SpaceshipGameScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.CuberunnerGamesScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.GunGameInterface.SetActive(false);
			
			// Disable Scenes
			UIGlobalVariablesScript.Singleton.SpaceshipMinigameSceneRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.SetActive(false);
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
			
			//UIGlobalVariablesScript.Singleton.Joystick.ResetJoystick();
			UIGlobalVariablesScript.Singleton.Joystick.gameObject.SetActive(false);	
			UIGlobalVariablesScript.Singleton.JoystickArt.SetActive(false);	

			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterController>().radius = UIClickButtonMasterScript.SavedRadius;
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterControllerScript>().FreezeCollisionDetection = false;
						
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
			UIGlobalVariablesScript.Singleton.StartMinigameScreenRef.SetActive(true);
			UIGlobalVariablesScript.Singleton.MinigameMenuScreeRef.SetActive(false);

			HandleClick(UIFunctionalityId.StartSelectedMinigame, sender);
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
			//SavedScale = UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localScale;
			//Debug.Log("SavedScale 1:" + SavedScale.ToString());

			UIGlobalVariablesScript.Singleton.ARWorldRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.NonARWorldRef.SetActive(false);

			UIGlobalVariablesScript.Singleton.InsideMinigamesMasterScreenRef.SetActive(true);
			UIGlobalVariablesScript.Singleton.MinigamesMenuMasterScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.SpaceshipGameScreenRef.SetActive(false);
			UIGlobalVariablesScript.Singleton.CuberunnerGamesScreenRef.SetActive(false);

			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().enabled = false;
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<ObjectLookAtDeviceScript>().enabled = false;

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
				UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterController>().radius = 0.51f;
				
				UIGlobalVariablesScript.Singleton.Joystick.gameObject.SetActive(true);	
				UIGlobalVariablesScript.Singleton.JoystickArt.SetActive(true);	
				
				//UIGlobalVariablesScript.Singleton.ShootButton.SetActive(true);

				UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.parent = UIGlobalVariablesScript.Singleton.GunGameScene.transform;
				
				UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localScale = new Vector3(0.026f, 0.026f, 0.025f);
				Camera.main.GetComponent<MusicScript>().PlayGun();

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
					//UIGlobalVariablesScript.Singleton.Joystick.ResetJoystick();

					UIGlobalVariablesScript.Singleton.JumbButton.SetActive(true);

					UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.parent = UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.transform;
					
					UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localScale = new Vector3(0.026f, 0.026f, 0.025f);
				Camera.main.GetComponent<MusicScript>().PlayCube();
				
					break;
				}
			}


			
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
			if (Application.platform == RuntimePlatform.IPhonePlayer) 
			{
				GameObject.Find("Aperture").GetComponent<Aperture>().Photo();
			}

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
			GameObject.Find("KamcordPrefab").GetComponent<RecordingGUI>().ShowVideos();
			break;
		}

		case UIFunctionalityId.RecordVideo:
		{
			GameObject.Find("KamcordPrefab").GetComponent<RecordingGUI>().StartRecording();
			sender.GetComponent<UIClickButtonMasterScript>().FunctionalityId = UIFunctionalityId.StopRecordVideo;
			sender.GetComponent<UISprite>().spriteName = "stopvideo";
			sender.GetComponent<UIButton>().normalSprite = "stopvideo";
			break;
		}

		case UIFunctionalityId.StopRecordVideo:
		{
			GameObject.Find("KamcordPrefab").GetComponent<RecordingGUI>().StopRecording();
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
