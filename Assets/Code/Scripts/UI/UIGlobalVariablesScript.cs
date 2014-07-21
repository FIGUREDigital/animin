﻿using UnityEngine;
using System.Collections;

public class UIGlobalVariablesScript : MonoBehaviour 
{
	public static UIGlobalVariablesScript Singleton;

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

	public GameObject SpaceshipMinigameSceneRef;
	public GameObject CubeRunnerMinigameSceneRef;
	public GameObject GardenSceneRef;
	public GameObject ARSceneRef;
	public GameObject NonSceneRef;
	public GameObject DragableUI3DObject;

	// Interface Bars
	public GameObject FitnessControlBarRef;
	public GameObject HealthControlBarRef;
	public GameObject HungryControlBarRef;
	public GameObject HapynessControlBarRef;
	public GameObject EvolutionControlBarRef;
	public GameObject ARPortal;

	public AnimationControllerScript MainCharacterAnimationControllerRef;
	public GameObject MainCharacterRef;
	public GameObject PanelMedicine;
	public GameObject PanelFoods;
	public GameObject PanelItems;
	public UILabel TextForStarsInMiniCollector;
	public UISprite EvolutionProgressSprite;

	public GameObject PopupIndicator;
	public GameObject ImageTarget;
	public GameObject MinigameInterruptedMenu;

	public TrackVuforiaScript Vuforia;
	public GameObject IndicatorAboveHead;
	public GameObject Item3DPopupMenu;
	public SoundEngineScript SoundEngine;
	public JoystiqScript Joystick;

	// Set from the code, keep static
	public static GameObject ButtonTriggeredMainMenuPopupRef;
	public static UIFunctionalityId SelectedMinigameToPlay;

	

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
	
	}
}