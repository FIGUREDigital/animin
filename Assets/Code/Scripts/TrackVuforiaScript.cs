using UnityEngine;
using System.Collections;

public class ValueSmoother
{
	public float ValueNow;
	public float ValueNext;

	public void Update()
	{
		if(Mathf.Abs(ValueNow - ValueNext) >= 0.15f)
			ValueNow = Mathf.Lerp(ValueNow, ValueNext, Time.deltaTime * 6);
	}
}

public class ValueSmootherVector3
{
	public Vector3 ValueNow;
	public Vector3 ValueNext;
	
	public void Update()
	{
		if(Vector3.Distance(ValueNow , ValueNext) >= 0.05f)
			ValueNow = Vector3.Lerp(ValueNow, ValueNext, Time.deltaTime * 6);
	}
}


public class TrackVuforiaScript : MonoBehaviour, ITrackableEventHandler
{
	private TrackableBehaviour mTrackableBehaviour;

	Vector3 SavedARPosition;

	//public GameObject ARSceneRef;
	//public GameObject UIGlobalVariablesScript.Singleton.NonSceneRef;
	//public GameObject UIGlobalVariablesScript.Singleton.ARSceneRef;
	//public GameObject UIGlobalVariablesScript.Singleton.MainCharacterRef;

	public static bool IsTracking;


	private ValueSmoother SmootherAxisX = new ValueSmoother();
	private ValueSmoother SmootherAxisY = new ValueSmoother();
	private ValueSmootherVector3 CameraPositionSmoother = new ValueSmootherVector3();
	private ValueSmootherVector3 CameraRotationSmoother = new ValueSmootherVector3();

	void Start()
	{
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}




		//this.renderer.enabled = false;
		SavedARPosition = new Vector3(0, 0.0f, 0);
		Input.gyro.enabled = true;
	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED ||
		    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			OnTrackingFound();
		}
		else
		{
			OnTrackingLost();
		}
	}
	


	public void FlipFrontBackCamera()
	{
		CameraDevice.Instance.Stop();

		// This assumes that the back facing camera was used before:
		if(CameraDevice.Instance.GetCameraDirection() == CameraDevice.CameraDirection.CAMERA_BACK || CameraDevice.Instance.GetCameraDirection() == CameraDevice.CameraDirection.CAMERA_DEFAULT)
		{
			CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_FRONT);


			//.reflection = QCARRenderer.VideoBackgroundReflection.OFF;

		}
		else
		{
			//QCARRenderer.Instance.GetVideoBackgroundConfig().reflection = QCARRenderer.VideoBackgroundReflection.ON;;
			CameraDevice.Instance.Init(CameraDevice.CameraDirection.CAMERA_BACK);

		}

		CameraDevice.Instance.Start();
	}

	void Update()
	{
		//Debug.Log("GYOR: " + Input.gyro.attitude.eulerAngles.ToString());

//		UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.transform.localPosition = CubeGamePosition.ValueNext;
		//UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.transform.rotation = Quaternion.Euler(CubeGameRotation.ValueNow);

//		if(UIGlobalVariablesScript.Singleton.NonSceneRef.activeInHierarchy)
//		{
//			Camera.main.transform.position = CameraPositionSmoother.ValueNow;
//			Camera.main.transform.rotation = Quaternion.Euler(CameraRotationSmoother.ValueNow);
//		}
//
//		CameraPositionSmoother.Update();
//		CameraRotationSmoother.Update();
	}


	void LateUpdate()
	{

		if(UIGlobalVariablesScript.Singleton.NonSceneRef.activeInHierarchy)
		{
//			if(UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.activeInHierarchy)
//			{
//				CameraPositionSmoother.ValueNext = new Vector3(0, 123.1f, -198.3f);
//				CameraRotationSmoother.ValueNext = new Vector3(14.73474f, 0.0f, 0.0f);
//			}
//			else
//			{
//				CameraPositionSmoother.ValueNext = new Vector3(0, 123.1f, -198.3f);
//				CameraRotationSmoother.ValueNext = new Vector3(14.73474f, 0.0f, 0.0f);
//			}


			/*float gyroAngle = 270 - Input.gyro.attitude.eulerAngles.z;
			if(gyroAngle >= 30) gyroAngle = 30;
			if(gyroAngle <= -30) gyroAngle = -30;

			gyroAngle += 30;
			gyroAngle /= 60;*/
			
			//float xAngle = Mathf.Lerp(15.5f, -15.5f, (Input.acceleration.x + 1) / 2);
			
			
			//Debug.Log("GYRO: " + Input.gyro.attitude.eulerAngles.ToString());
			
			//UIGlobalVariablesScript.Singleton.NonSceneRef.transform.localRotation = Quaternion.Euler(gyroAngleY, 0, 0);
			
			//Debug.Log("Acceleration: " + Input.acceleration.ToString() + "_" + ((Input.acceleration.x + 1) / 2).ToString());
			//Debug.Log("Gravity: " + Input.gyro.gravity.ToString());
			
			//0.0f to -0.5f to -1.0f
			//Debug.Log((Mathf.Lerp(-50, 50, (Input.acceleration.x + 1) / 2)).ToString());
			//Camera.main.transform.rotation = Quaternion.Euler(14.73474f + (Input.acceleration.y) * 10, -(Input.acceleration.x) * 10, 0);
			//Camera.main.transform.localPosition = new Vector3(Mathf.Lerp(200, -200, (Input.acceleration.x + 1) / 2), Camera.main.transform.localPosition.y, Camera.main.transform.localPosition.z);
			
			
			//Debug.Log(	GetQuadraticCoordinates(((Camera.main.transform.localPosition.x + 200) / 1)).ToString());
			
			
			float stableAccelerationX = (float)System.Math.Round(Input.acceleration.x, 2);
			float stableAccelerationY = (float)System.Math.Round(Input.acceleration.y, 2);
			
			float angle = Mathf.Lerp(180, 360, (stableAccelerationX + 1) / 2 /*(Mathf.Sin(Time.time) + 1) / 2*/);
			SmootherAxisX.ValueNext = (float)System.Math.Round(angle);
			
			Vector3 newPosition = new Vector3(
				Mathf.Cos(SmootherAxisX.ValueNow* Mathf.Deg2Rad ) * 220,
				0,
				(Mathf.Sin(SmootherAxisX.ValueNow* Mathf.Deg2Rad ) * 220) * 0.2f);
			
			
			
			
			float value = stableAccelerationY;
			if(value > 0) value = 0;
			if(value < -1) value = -1;
			value *= -1;
			//value = 1 - value;
			
			float angle2 = Mathf.Lerp(360, 180, value/*(Mathf.Sin(Time.time) + 1) / 2*/);
			SmootherAxisY.ValueNext = (float)System.Math.Round(angle2);
			
			Vector3 newPosition2 = new Vector3(
				0,
				Mathf.Cos(SmootherAxisY.ValueNow* Mathf.Deg2Rad ) * 90,
				(Mathf.Sin(SmootherAxisY.ValueNow* Mathf.Deg2Rad ) * 90) * 0.2f);

			Vector3 cameraPoint = new Vector3(0, 233.1f, -198.3f);
			Transform target = UIGlobalVariablesScript.Singleton.NonSceneRef.transform;
			
			if(UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.activeInHierarchy)
			{
				cameraPoint = new Vector3(0, 500.6f, -250.63f);
				target = UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.transform;
			}
			else
			{

			}

			Camera.main.transform.localPosition = cameraPoint + newPosition2 + newPosition;
			Camera.main.transform.LookAt(target);
		}
		
		
		SmootherAxisX.Update();
		SmootherAxisY.Update();

	}


	// CHANGES THAT HAVE TO HAPPEN WHEN AR CHANGES
	public void OnARChanged()
	{
		if(TrackVuforiaScript.IsTracking)
		{
			//UIGlobalVariablesScript.Singleton.NonSceneRef.SetActive (false);
			//UIGlobalVariablesScript.Singleton.ARSceneRef.SetActive(true);

			UIGlobalVariablesScript.Singleton.AROnIndicator.SetActive(true);
			UIGlobalVariablesScript.Singleton.AROffIndicator.SetActive(false);

			UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.transform.parent = UIGlobalVariablesScript.Singleton.ARSceneRef.transform;
			UIGlobalVariablesScript.Singleton.GunGameScene.transform.parent = UIGlobalVariablesScript.Singleton.ARSceneRef.transform;
			UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().ArenaStage.SetActive(false);
			//CubeGamePosition.ValueNext = Vector3.zero;
			//CubeGameRotation.ValueNext = UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.transform.parent.rotation.eulerAngles;
		}
		else
		{
			//UIGlobalVariablesScript.Singleton.NonSceneRef.SetActive (true);
			//UIGlobalVariablesScript.Singleton.ARSceneRef.SetActive(false);

			UIGlobalVariablesScript.Singleton.AROnIndicator.SetActive(false);
			UIGlobalVariablesScript.Singleton.AROffIndicator.SetActive(true);

			UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.transform.parent = UIGlobalVariablesScript.Singleton.NonSceneRef.transform;
			UIGlobalVariablesScript.Singleton.GunGameScene.transform.parent = UIGlobalVariablesScript.Singleton.NonSceneRef.transform;
			UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().ArenaStage.SetActive(true);
			//CubeGameRotation.ValueNext = new Vector3(0,0,0);
			//CubeGamePosition.ValueNext = new Vector3(0,-0.3f,-0.1f);
		}
	}

	public static void EnableDisableMinigamesBasedOnARStatus()
	{
		GameObject sprite = GameObject.Find("SpriteCubeWorld");

		if(sprite != null)
		{
			if(IsTracking)
			{
				sprite.GetComponent<UIWidget>().color = new Color(1, 1, 1, 1);
				sprite.GetComponent<UIClickButtonMasterScript>().FunctionalityId = UIFunctionalityId.PlayMinigameCubeRunners;

				sprite.GetComponent<UIButton>().defaultColor = new Color(
					sprite.GetComponent<UIButton>().defaultColor.r,
					sprite.GetComponent<UIButton>().defaultColor.g,
					sprite.GetComponent<UIButton>().defaultColor.b,
					1);

				sprite.GetComponent<UIButton>().hover = new Color(
					sprite.GetComponent<UIButton>().hover.r,
					sprite.GetComponent<UIButton>().hover.g,
					sprite.GetComponent<UIButton>().hover.b,
					1);
				
				//UIGlobalVariablesScript.Singleton.RequiresGamecardScreenRef.SetActive(false);
			}
			else
			{
				sprite.GetComponent<UIWidget>().color = new Color(1, 1, 1, 0.5f);
				sprite.GetComponent<UIClickButtonMasterScript>().FunctionalityId = UIFunctionalityId.None;

				sprite.GetComponent<UIButton>().defaultColor = new Color(
					sprite.GetComponent<UIButton>().defaultColor.r,
					sprite.GetComponent<UIButton>().defaultColor.g,
					sprite.GetComponent<UIButton>().defaultColor.b,
					0.5f);

				sprite.GetComponent<UIButton>().hover = new Color(
					sprite.GetComponent<UIButton>().hover.r,
					sprite.GetComponent<UIButton>().hover.g,
					sprite.GetComponent<UIButton>().hover.b,
					0.5f);
			}
		}
	}


	public void OnCharacterEnterARScene()
	{
		UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.parent = UIGlobalVariablesScript.Singleton.ARSceneRef.transform;
		SavedARPosition.y = 0;
		UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localPosition = new Vector3(0, 0.01f, 0);
		UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.rotation = Quaternion.Euler(0, 180, 0);
		UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localScale = new Vector3(0.035f, 0.035f, 0.035f);
		UIGlobalVariablesScript.Singleton.Shadow.transform.localScale = new Vector3(0.46f, 0.46f, 0.46f);

		if(UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().CurrentAction != ActionId.Sleep &&
		   UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().CurrentAction != ActionId.EnterSleep)
		{
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().Stop(true);
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterControllerScript>().ResetRotation();
		}
	}

	public void OnCharacterEnterNonARScene()
	{
		if(IsTracking)
			SavedARPosition = UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localPosition;
		
		Camera.main.transform.position = new Vector3(0, 123.1f, -198.3f);
		Camera.main.transform.rotation = Quaternion.Euler(14.73474f, 0.0f, 0.0f);

		UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.parent = UIGlobalVariablesScript.Singleton.NonSceneRef.transform;
		UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localPosition = new Vector3(0, 0.01f, 0);
		UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.rotation = Quaternion.Euler(0, 180, 0);
		UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.localScale = new Vector3(0.035f, 0.035f, 0.035f);
		UIGlobalVariablesScript.Singleton.Shadow.transform.localScale = new Vector3(0.46f, 0.46f, 0.46f);
	
		if(UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().CurrentAction != ActionId.Sleep &&
		   UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().CurrentAction != ActionId.EnterSleep)
		{
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterControllerScript>().ResetRotation();
			UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().Stop(true);
		}
	}


	private void OnTrackingFound()
	{
		IsTracking = true;
		bool isPlayingMinigame = false;

		if(UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.activeInHierarchy || 
		   UIGlobalVariablesScript.Singleton.GunGameScene.activeInHierarchy)
			isPlayingMinigame = true;


		OnARChanged();

		CharacterProgressScript progress = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>();

		if(isPlayingMinigame)
		{
			Debug.Log("OnTrackingFound: playing mini game");
			UIGlobalVariablesScript.Singleton.NonSceneRef.SetActive (false);
			UIGlobalVariablesScript.Singleton.ARSceneRef.SetActive(true);
		}
		else  if(UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().CurrentAction == ActionId.Sleep ||
		   UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().CurrentAction == ActionId.EnterSleep)
		{
			Debug.Log("OnTrackingFound: sleeping");
			UIGlobalVariablesScript.Singleton.NonSceneRef.SetActive (false);
			UIGlobalVariablesScript.Singleton.ARSceneRef.SetActive(true);
			OnCharacterEnterARScene();
		}
		else
		{
			Debug.Log("OnTrackingFound: caring screen");
//			if(UIGlobalVariablesScript.Singleton.InsideMinigamesMasterScreenRef.activeInHierarchy)
//			{
//				UIGlobalVariablesScript.Singleton.MinigameInterruptedMenu.SetActive(false);
//			}
			/*else if(UIGlobalVariablesScript.Singleton.MinigamesMenuMasterScreenRef.activeInHierarchy)
			{
				UIGlobalVariablesScript.Singleton.ARSceneRef.SetActive(true);
			}*/
			//else
			{
				//return;
				UIGlobalVariablesScript.Singleton.SoundEngine.Play(PersistentData.Singleton.PlayerAniminId, PersistentData.Singleton.AniminEvolutionId, CreatureSoundId.JumbInPortal);
				UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimationControllerScript>().IsEnterPortal = true;
				progress.CurrentAction = ActionId.EnterPortalToAR;

				UIGlobalVariablesScript.Singleton.ARPortal.GetComponent<PortalScript>().Show(PortalStageId.NonARScene, true);
				progress.Stop(true);
				progress.PortalTimer = 0;
				Debug.Log("ENTERING AR STATE ");
				//UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().CurrentAction = ActionId.EnterPortalToNonAR;


				UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimateCharacterOutPortalScript>().Timer = 0;
				UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimateCharacterOutPortalScript>().JumbId = AnimateCharacterOutPortalScript.JumbStateId.JumbIn;
			}
		}
	}

	
	public void OnTrackingLost()
	{
		IsTracking = false;

		bool isPlayingMinigame = false;

		if(UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.activeInHierarchy || 
		   UIGlobalVariablesScript.Singleton.GunGameScene.activeInHierarchy)
			isPlayingMinigame = true;

		OnARChanged();
		UIGlobalVariablesScript.Singleton.NonSceneRef.SetActive (true);
		UIGlobalVariablesScript.Singleton.ARSceneRef.SetActive(false);

		if(isPlayingMinigame)
		{
			Debug.Log("OnTrackingLost: playing mini game");
		}
		else
		{
			OnCharacterEnterNonARScene();

			//return;
			if(UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().CurrentAction == ActionId.Sleep ||
			   UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().CurrentAction == ActionId.EnterSleep)
			{

				Debug.Log("OnTrackingLost: sleeping");
			}
			else
			{
				CharacterProgressScript progressScript = UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>();

				Debug.Log("OnTrackingLost: caring screen");
				//UIGlobalVariablesScript.Singleton.MainUIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().PortalTimer = 0;
				//Debug.Log("ENTERING NON AR STATE ");
				//UIGlobalVariablesScript.Singleton.MainUIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().Stop(true);
				//UIGlobalVariablesScript.Singleton.MainCharacterAnimationControllerRef.IsEnterPortal = true;
				progressScript.CurrentAction = ActionId.None;
				//UIGlobalVariablesScript.Singleton.Vuforia.OnExitAR();
				UIGlobalVariablesScript.Singleton.ARPortal.GetComponent<PortalScript>().Show(PortalStageId.NonARScene, false);

				UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimationControllerScript>().IsExitPortal = true;
				UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.rotation = Quaternion.Euler(0, 180, 0);
				UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterControllerScript>().ResetRotation();
		
				UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimateCharacterOutPortalScript>().Timer = 0;
				UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimateCharacterOutPortalScript>().JumbId = AnimateCharacterOutPortalScript.JumbStateId.Jumbout;
				UIGlobalVariablesScript.Singleton.SoundEngine.Play(PersistentData.Singleton.PlayerAniminId, PersistentData.Singleton.AniminEvolutionId, CreatureSoundId.JumbOutPortal);
				//UIGlobalVariablesScript.Singleton.ARPortal.GetComponent<PortalScript>().Show(true);
				progressScript.CurrentAction = ActionId.SmallCooldownPeriod;
				progressScript.SmallCooldownTimer = 0.5f;

				//OnExitAR();
				//UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<CharacterProgressScript>().CurrentAction = ActionId.EnterPortalToNonAR;
			}
		}

	}
}