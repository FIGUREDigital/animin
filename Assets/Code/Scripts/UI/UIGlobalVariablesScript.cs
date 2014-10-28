using UnityEngine;
using System.Collections;

public class UIGlobalVariablesScript : MonoBehaviour 
{
	public static UIGlobalVariablesScript Singleton;

	public GameObject ARCamera;
	// Set from the Editor

	public GameObject MainMenuPopupObjectRef;
	public GameObject MinigamesMenuMasterScreenRef;
	public GameObject CaringScreenRef;
	public GameObject StatsScreenRef;
	public GameObject PicturesScreenRef;
	public GameObject StartMinigameScreenRef;
	public GameObject MinigameMenuScreeRef;
	public GameObject InsideMinigamesMasterScreenRef;
	public GameObject SpaceshipGameScreenRef;
	public GameObject CuberunnerGamesScreenRef;
	public GameObject AROnIndicator;
	public GameObject AROffIndicator;
	public GameObject SettingsScreenRef;    
	public GameObject RequiresGamecardScreenRef;
    public GameObject AchievementsScreenRef;
    public GameObject LoadingScreenRef;
	public GameObject CreditsScreenRef;

	public GameObject SpaceshipMinigameSceneRef;
	public GameObject CubeRunnerMinigameSceneRef;
	public GameObject GardenSceneRef;
	public GameObject ARSceneRef;
	public GameObject NonSceneRef;
	public GameObject NonARWorldRef;
	public GameObject ARWorldRef;
	public GameObject DragableUI3DObject;
	//public GameObject ARSceneContainer;


	// Interface Bars
	public GameObject FitnessControlBarRef;
	public GameObject HealthControlBarRef;
	public GameObject HungryControlBarRef;
	public GameObject HapynessControlBarRef;
	public GameObject EvolutionControlBarRef;
	public GameObject ARPortal;
	public GameObject Shadow;
	public GameObject SoundSprite;

	//public AnimationControllerScript MainCharacterAnimationControllerRef;
	public GameObject MainCharacterRef;
	public GameObject PanelMedicine;
	public GameObject PanelFoods;
	public GameObject PanelItems;
	public UILabel TextForStarsInMiniCollector;
	public UITexture EvolutionProgressSprite;

	public GameObject PopupIndicator;
	public GameObject ImageTarget;
	public GameObject MinigameInterruptedMenu;

	public TrackVuforiaScript Vuforia;
	//public GameObject IndicatorAboveHead;
	public GameObject AlarmUI;
	public SoundEngineScript SoundEngine;
	public JoystiqScript Joystick;
	public GameObject JoystickArt;
	public GameObject UIRoot;
	public GameObject StereoUI;
	public UILabel CurrentArtistLabel;
	public UILabel CurrentSongLabel;
	public UISprite PlayPauseButton;
	public UISprite ProgressSongBar;
	public GameObject LightbulbUI;
	public GameObject LightbulbSwitchButton;
	public GameObject LightbulbRedButton;
	public GameObject LightbulbGreenButton;

	public GameObject TracklistPanel;
	public GameObject PlaySongPanel;
	public GameObject PanelWithAllSongs;

	// Set from the code, keep static
	public static GameObject ButtonTriggeredMainMenuPopupRef;
	public static UIFunctionalityId SelectedMinigameToPlay;

	public GameObject GunGameScene;
	public GameObject GunGameInterface;

	public GameObject ShootButton;
	public GameObject JumbButton;
	public GameObject PauseGameButton;
	public GameObject PausedScreen;
	public GameObject MultiplayerOptionsScreen;
	public GameObject EnterFriendsNameChat;
	public UILabel ZefTokensUI;
	public UILabel ItemsFoodMedicineLabel;
	public GameObject EDMBoxUI;
	public GameObject JunoUI;
	public GameObject PianoUI;
	public GameObject ItemScrollView;
	public GameObject FoodButton;
	public GameObject MedicineButton;
	public GameObject ItemsButton;
	public GameObject StatsButton;
	public GameObject MinigamesButton;


	public enum ActiveState {Caring, Collecting,Gun, None};
	public ActiveState CurrentlyActive{
		get
		{
			if (CubeRunnerMinigameSceneRef != null)
				return ActiveState.Collecting;
			else if (GunGameScene != null)
				return ActiveState.Gun;
			else
				return ActiveState.Caring;
		}
	}

	public System.Type CurrentVFType{
		get
		{
			switch(UIGlobalVariablesScript.Singleton.CurrentlyActive){
			case UIGlobalVariablesScript.ActiveState.Caring:
				return typeof(TrackVuforiaScript);
				break;
			case UIGlobalVariablesScript.ActiveState.Collecting:
				return typeof(TrackVFMG1);
				break;
			case UIGlobalVariablesScript.ActiveState.Gun:
				return typeof(TrackVFMG2);
				break;
			}
			return null;
		}
	}

	public GameObject CurrentMiniGameSceneRef{
		get
		{
			switch(UIGlobalVariablesScript.Singleton.CurrentlyActive){
			case UIGlobalVariablesScript.ActiveState.Caring:
				return GardenSceneRef;
				break;
			case UIGlobalVariablesScript.ActiveState.Collecting:
				return CubeRunnerMinigameSceneRef;
				break;
			case UIGlobalVariablesScript.ActiveState.Gun:
				return GunGameScene;
				break;
			}
			return null;
		}
	}
	public GameObject CurrentUIScreenRef{
		get
		{
			
			switch(UIGlobalVariablesScript.Singleton.CurrentlyActive){
			case UIGlobalVariablesScript.ActiveState.Caring:
				return CaringScreenRef;
				break;
			case UIGlobalVariablesScript.ActiveState.Collecting:
				return CuberunnerGamesScreenRef;
				break;
			case UIGlobalVariablesScript.ActiveState.Gun:
				return GunGameInterface;
				break;
			}
			return null;
		}
	}

	public void OpenParentalGateway(GameObject prev, GameObject next)
	{
		GameObject go = (GameObject)Instantiate(Resources.Load("Prefabs/Parental Gateway"));
		UIRoot root = GameObject.FindObjectOfType<UIRoot>();
		go.transform.parent = root.gameObject.transform;
		go.transform.localScale = Vector3.one;
		go.GetComponent<ParentalGateway>().Open(prev, next);
	}

	void Awake()
	{
		Singleton = this;
	}

	// Use this for initialization
	void Start () 
	{

	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("According to the scientific method, we're are in the state: "+CurrentlyActive+";");
	}

	void OnApplicationPause()
	{

	}

	void OnApplicationResume()
	{
	}
}
