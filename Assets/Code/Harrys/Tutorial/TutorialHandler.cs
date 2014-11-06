
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialHandler : MonoBehaviour {

	private Tutorial[] Tutorials{
		get { return TutorialReader.Instance.Tutorials;}
	}

    //-Gameobject Stuff -------
	[SerializeField]
	private GameObject TutorialUIParent;
	[SerializeField]
	private Animator WormAnimator;
	[SerializeField]
	private UILabel TutorialText;
	[SerializeField]
    private UIButton NextButton;
    [SerializeField]
    private GameObject StatsButton;


    //-Start Conditions
    public bool[] TutorialConditions;

    public void SetTutorialCondition(string name, bool value){
        for (int i = 0; i < Tutorials.Length; i++)
        {
            if (name == Tutorials[i].Name)
            {
                TutorialConditions[Tutorials[i].id_num] = value;
                return;
            }
        }
        Debug.Log("ERROR: Tutorial Name [" + name + "] not found!");
    }



	private bool m_PlayingTutorial, m_EndingTutorial;
	private int m_CurTutorial_i;
	private int m_Letter_i, m_Lesson_i, m_Entry_i;

	private GameObject m_CurrentListening;
	public GameObject CurrentListeningGO{ get { return m_CurrentListening; } }

	private string m_CurrentExitCond;
	public string CurrentExitCond{ get { return m_CurrentExitCond; } }
	private bool m_WaitingForInput;

	private const string TutorialPlayerPrefID = "TUTORIALS_COMPLETED";
    private bool CheckPref(int id){
        return PlayerPrefs.GetString(TutorialPlayerPrefID + id) == "true";
    }






	private float m_Timer;
	private int m_TutorialCountingDown= -1;
    private bool /*the secret to comedy*/ m_IsTiming;

	private void SetTimerOnTutorial(int TutId, float time){
		Debug.Log ("Setting Timer. Tut : ["+TutId+"]; Time : ["+time+"];");
		m_TutorialCountingDown = TutId;
		m_Timer = time;
        m_IsTiming = true;
	}
	private void TurnOffTimer(){
		m_TutorialCountingDown = -1;
		m_Timer = 0;
        m_IsTiming = false;
	}

	public bool IsPlaying
	{
		get
		{
			return (m_PlayingTutorial || m_EndingTutorial);
		}
	}








	// Use this for initialization
	void Start () {
		//Blocker.gameObject.SetActive (false);
		WormAnimator.gameObject.SetActive(false);
		TutorialUIParent.SetActive (false);

		TutorialReader.Instance.Deserialize ();
		for (int i = 0; i < Tutorials.Length; i++) {
			if (PlayerPrefs.GetString(TutorialPlayerPrefID + i) == null)
				PlayerPrefs.SetString(TutorialPlayerPrefID + i,"false");
		}
        TutorialConditions = new bool[Tutorials.Length];
        SetTutorialCondition("Initial", true);
	}
	


	// Update is called once per frame
	void Update () {
		if (!m_PlayingTutorial) {
			//I HATE MONODEVELOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
//																								OOOOOOOOOOOOOOOOOOOOOOOOOP!
			
			//TutorialReader.Instance.test();
			for (int i = 0; i < Tutorials.Length; i++) {
				if (StartConditions (i)) {
					if (TutorialUIParent == null) return;
					TutorialUIParent.SetActive (true);
					WormAnimator.gameObject.SetActive(true);
					//Blocker.gameObject.SetActive(true);
					Block(true);
					WormAnimator.SetTrigger ("worm_GoOut");

					m_CurTutorial_i = i;
					m_Letter_i = 0;
					m_Lesson_i = 0;
					m_Entry_i = 0;
					
					MakeScreensVisible(new GameObject[]{UIGlobalVariablesScript.Singleton.CaringScreenRef});

					m_PlayingTutorial = true;
					break;
				}
			}
		} else if (!m_EndingTutorial){
			if (m_Entry_i >= Tutorials[m_CurTutorial_i].Lessons[m_Lesson_i].TutEntries.Length) NextLesson();
			string text = Tutorials[m_CurTutorial_i].Lessons[m_Lesson_i].TutEntries[m_Entry_i].text;
			if (text.Length >= m_Letter_i){

				TutorialText.text = text.Substring(0,m_Letter_i++);
				NextButton.gameObject.SetActive(false);
			} else {
				if (!m_WaitingForInput)
					NextButton.gameObject.SetActive(true);
			}
		} else {
			if (WormAnimator.GetCurrentAnimatorStateInfo(0).IsName("worm_hidden")){
				
				TutorialUIParent.SetActive (false);
				WormAnimator.gameObject.SetActive(false);
				m_PlayingTutorial = false;
				m_EndingTutorial = false;
				TutorialUIParent.SetActive (false);
			}
		}
        if (m_IsTiming)
        {
            if (m_Timer > 0)
                m_Timer -= Time.deltaTime;
            else
            {
                TutorialConditions[m_TutorialCountingDown] = true;
                TurnOffTimer();
            }
        }
	}















    //- ENTRY CONDITIONS ----------------------------------------------------------------
    public bool CheckCharacterProgress(CharacterProgressScript script, RaycastHit hitInfo){
        bool cont = false;
        switch (m_CurrentExitCond) {
            case ("WakeUp"):
                if (hitInfo.collider.gameObject == script.SleepBoundingBox)
                    cont = true;
                break;
        }
        return cont;
    }

    //This method test whether or not to start the tutorial.
    private bool StartConditions(int id){

        if (CheckPref(id))
            return false;

        return TutorialConditions[id];

        /*switch (id) {
        case (0):
            return true;
        case (1):
            return (m_Timer<0);
        case (2):
            return (m_Timer<0);
        case (3):
            return (m_Timer<0);
        case (4):
            return (m_Timer<0);
        case (5):
            return (m_Timer<0);
        case (6):
            return (m_Timer<0);
        case (7):
            return (m_Timer<0);
            
        default:
            return false;
        }
        */      
    }


    //- EXIT STAMPS ----------------------------------------------------------------
	public void OnTutorialClick(GameObject go){
		if (go == m_CurrentListening) {
			UIEventListener.Get (m_CurrentListening).onClick -= OnTutorialClick;
			m_WaitingForInput = false;
			//go.GetComponent<UIWidget>().depth = m_SavedDepth;
			go.GetComponent<BoxCollider>().enabled = false;
			//NextLesson();
			NextButtonPress(true);
		}
	}
    public void TriggerExitCond(string TutorialName, string StampName){
        Debug.Log("Current Tutorial Name : ["+Tutorials[m_CurTutorial_i].Name+"]; Name to Change : ["+TutorialName+"];");
        if (Tutorials[m_CurTutorial_i].Name == TutorialName)
        {
            TriggerExitCond(Tutorials[m_CurTutorial_i].id_num, StampName);
        }
    }
    public void TriggerExitCond(int id, string StampName){
        if (Tutorials[m_CurTutorial_i].id_num == id && 
            Tutorials[m_CurTutorial_i].Lessons[m_Lesson_i].ExitStr == StampName && 
            m_WaitingForInput)
        {
            m_WaitingForInput = false;
            NextButtonPress(true);
        }
    }

	
	//- End of Lesson Processing ----------------------------------------------------------------
	public void NextButtonPress(bool ignoreCheck = false){

		int maxEntries = Tutorials [m_CurTutorial_i].Lessons [m_Lesson_i].TutEntries.Length;


		//Debug.Log ("Testing Entry : ["+m_Entry_i+":"+maxEntries+"]; Lesson : ["+m_Lesson_i+":"+maxLessons+"];");
		
		Debug.Log ("Testing : ["+ (m_Entry_i + 1) + ":" + (maxEntries) + "];");
		
		m_Letter_i = 0;
		m_Entry_i += 1;
		m_CurrentExitCond = Tutorials [m_CurTutorial_i].Lessons [m_Lesson_i].ExitStr;

		bool noNext = (m_CurrentExitCond != "None" && !ignoreCheck);
		if (noNext) maxEntries -= 1;
		
		if (m_Entry_i >= maxEntries) {

			// - When at the end of a lesson, this code here fires
			if (noNext) {

				//Exit stamp handling

				switch(m_CurrentExitCond){
				case ("Stats"):
					m_CurrentListening = UIGlobalVariablesScript.Singleton.StatsButton;
					UIWidget widget = m_CurrentListening.GetComponent<UIWidget> ();
					m_CurrentListening.GetComponent<BoxCollider>().enabled = true;

					UIEventListener.Get (m_CurrentListening).onClick += OnTutorialClick;
					
					NextButton.gameObject.SetActive(false);
					m_WaitingForInput = true;
					break;
				case("EatStrawberry"):
					
					PersistentData.Singleton.AddItemToInventory(InventoryItemId.Strawberry, 1);

					UIGlobalVariablesScript.Singleton.FoodButton.GetComponent<BoxCollider>().enabled = true;
					UIGlobalVariablesScript.Singleton.FoodButton.GetComponent<InterfaceItemLinkToModelScript>().ItemID = InventoryItemId.Strawberry;
					UIGlobalVariablesScript.Singleton.FoodButton.GetComponent<UISprite>().spriteName = "strawberry";
					UIGlobalVariablesScript.Singleton.FoodButton.GetComponent<UIButton>().normalSprite = "strawberry";
					UIGlobalVariablesScript.Singleton.FoodButton.GetComponent<UIClickButtonMasterScript>().enabled = false;


					ProfilesManagementScript.Singleton.CurrentProfile.Characters[(int)ProfilesManagementScript.Singleton.CurrentProfile.ActiveAnimin].Hungry = 0;
					PersistentData.Singleton.Hungry = 0;
					m_WaitingForInput = true;
					break;
				default:
					m_WaitingForInput = true;
					break;
				}
			} else {
				NextLesson ();
			}
		}
	}

    //----Load next lesson------------------------------------------------
	public void NextLesson(){
		if (m_WaitingForInput) return;
		
		m_Letter_i = 0;
		m_Entry_i =0;
		int maxLessons = Tutorials [m_CurTutorial_i].Lessons.Length;
		
		Debug.Log ("maxLessons : [" + maxLessons + "];");
		
		if (++m_Lesson_i >= maxLessons) {

            //----This code is fired at the end of a tutorial----------------

			WormAnimator.SetTrigger ("worm_GoIn");
			
			PlayerPrefs.SetString(TutorialPlayerPrefID + m_CurTutorial_i,"true");

			//TutorialReader.Instance.TutorialFinished[m_CurTutorial_i] = true;
			
			//Blocker.gameObject.SetActive(false);
			Block(false);
			
			m_EndingTutorial = true;
			NextButton.gameObject.SetActive(false);

            /*
            for (int i = 0; i < Tutorials.Length; i++)
            {
                if (Tutorials[i].Timer != null)
                {
                    int trig = Tutorials[i].Timer.trigi;
                    if (trig == m_CurTutorial_i && !CheckPref(trig))
                    {
                        SetTimerOnTutorial(trig,Tutorials[i].Timer.secf);
                    }
                }
            }
            */

			switch (m_CurTutorial_i){
			case (0):
				SetTimerOnTutorial(1,10f);
				break;
			case (1):
				SetTimerOnTutorial(2,20f);
				break;
			case (2):
				SetTimerOnTutorial(3,20f);
				break;
			case (3):
				SetTimerOnTutorial(4,60f);
				break;
			case (4):
				SetTimerOnTutorial(5,60f);
				break;
			case (5):
				SetTimerOnTutorial(6,20f);
				break;
			case (6):
				SetTimerOnTutorial(7,20f);
				break;
			}

		}
	}








	private UIButton[] m_Buttons;
	private bool[] m_EnabledButtons;
	private bool m_BoolArraySet;

	private void Block (bool on){
		if (on && !m_BoolArraySet) {
			m_Buttons = UIGlobalVariablesScript.Singleton.UIRoot.GetComponentsInChildren<UIButton>(true);
			m_EnabledButtons = new bool[m_Buttons.Length];
			for (int i = 0; i < m_Buttons.Length; i++){
				m_EnabledButtons[i] = m_Buttons[i].gameObject.GetComponent<BoxCollider>().enabled;
				m_Buttons[i].gameObject.GetComponent<BoxCollider>().enabled = false;
			}
			m_BoolArraySet = true;
		} else {
			for (int i = 0; i < m_Buttons.Length; i++){
				m_Buttons[i].gameObject.GetComponent<BoxCollider>().enabled = m_EnabledButtons[i];
			}
			m_Buttons = null;
			m_BoolArraySet = false;
		}
		NextButton.GetComponent<BoxCollider> ().enabled = true;
	}

	
	public void ResetTutorials(){
		for (int i = 0; i < Tutorials.Length; i++) {
			PlayerPrefs.SetString(TutorialPlayerPrefID + i,"false");
			TurnOffTimer();
		}
	}


	public void MakeScreensVisible(GameObject[] turnons){
		GameObject[] turnoffs = new GameObject[]{
			UIGlobalVariablesScript.Singleton.CaringScreenRef,
			UIGlobalVariablesScript.Singleton.AlarmUI,
			UIGlobalVariablesScript.Singleton.StereoUI,
			UIGlobalVariablesScript.Singleton.MainMenuPopupObjectRef,
			UIGlobalVariablesScript.Singleton.LightbulbUI,
			UIGlobalVariablesScript.Singleton.AchievementsScreenRef,
			UIGlobalVariablesScript.Singleton.EDMBoxUI,
			UIGlobalVariablesScript.Singleton.PianoUI,
			UIGlobalVariablesScript.Singleton.JunoUI,
			UIGlobalVariablesScript.Singleton.MinigamesMenuMasterScreenRef,
			UIGlobalVariablesScript.Singleton.StatsScreenRef,
			UIGlobalVariablesScript.Singleton.PicturesScreenRef,
			UIGlobalVariablesScript.Singleton.SettingsScreenRef,
			UIGlobalVariablesScript.Singleton.CreditsScreenRef,
			UIGlobalVariablesScript.Singleton.UIRoot.transform.FindChild ("ParentalControlsUI").gameObject,
			UIGlobalVariablesScript.Singleton.UIRoot.transform.FindChild ("UI - Set Parental Password").gameObject
		};
		for (int i = 0; i < turnoffs.Length; i++){
			turnoffs[i].SetActive(false);
		}
		for (int i = 0; i < turnons.Length; i++){
			turnons[i].SetActive(true);
		}
	}
}
















