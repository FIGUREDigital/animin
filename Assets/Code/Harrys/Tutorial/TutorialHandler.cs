
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialHandler : MonoBehaviour {

	private Tutorial[] Tutorials{
		get { return TutorialReader.Instance.Tutorials;}
	}

	[SerializeField]
	private GameObject TutorialUIParent;
	[SerializeField]
	private Animator WormAnimator;
	[SerializeField]
	private UILabel TutorialText;
	[SerializeField]
	private UIButton NextButton;

//	[SerializeField]
//	private UIWidget Blocker;

	[SerializeField]
	private GameObject StatsButton;

	private bool m_PlayingTutorial, m_EndingTutorial;
	private int m_CurTutorial_i;
	private int m_Letter_i, m_Lesson_i, m_Entry_i;
	
	private int m_SavedDepth;
	private GameObject m_CurrentListening;
	private bool m_WaitingForInput;

	private const string TutorialPlayerPrefID = "TUTORIALS_COMPLETED";
	
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
		
		if (Debug.isDebugBuild && Input.GetKey (KeyCode.R)) {
			ResetTutorials();
		}

		/*
		Debug.Log ("Argh : [" + UIGlobalVariablesScript.Singleton.UIRoot.GetComponent<UIPanel>().height+":"+UIGlobalVariablesScript.Singleton.UIRoot.GetComponent<UIPanel>().width+ "];");
		Blocker.GetComponent<BoxCollider> ().size = new Vector3 (
			UIGlobalVariablesScript.Singleton.UIRoot.GetComponent<UIPanel>().width, 
			UIGlobalVariablesScript.Singleton.UIRoot.GetComponent<UIPanel>().height, 1);
		*/

	}



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
	public void NextButtonPress(bool ignoreCheck = false){

		int maxEntries = Tutorials [m_CurTutorial_i].Lessons [m_Lesson_i].TutEntries.Length;


		//Debug.Log ("Testing Entry : ["+m_Entry_i+":"+maxEntries+"]; Lesson : ["+m_Lesson_i+":"+maxLessons+"];");
		
		Debug.Log ("Testing : ["+ (m_Entry_i + 1) + ":" + (maxEntries) + "];");
		
		m_Letter_i = 0;
		m_Entry_i += 1;
		string exitstr = Tutorials [m_CurTutorial_i].Lessons [m_Lesson_i].ExitStr;
		bool noNext = (exitstr != "None" && !ignoreCheck);
		if (noNext) maxEntries -= 1;
		
		if (m_Entry_i >= maxEntries) {

			if (noNext) {

				m_CurrentListening = GOFromString (exitstr);
				UIWidget widget = m_CurrentListening.GetComponent<UIWidget> ();
				m_SavedDepth = widget.depth;
				//widget.depth = Blocker.depth + 1;
				m_CurrentListening.GetComponent<BoxCollider>().enabled = true;

				UIEventListener.Get (m_CurrentListening).onClick += OnTutorialClick;
				
				NextButton.gameObject.SetActive(false);
				m_WaitingForInput = true;
			} else {
				NextLesson ();
			}
		}
	}

	public void NextLesson(){
		if (m_WaitingForInput) return;
		
		m_Letter_i = 0;
		m_Entry_i =0;
		int maxLessons = Tutorials [m_CurTutorial_i].Lessons.Length;

		Debug.Log ("maxLessons : [" + maxLessons + "];");

		if (++m_Lesson_i >= maxLessons) {
			WormAnimator.SetTrigger ("worm_GoIn");

			PlayerPrefs.SetString(TutorialPlayerPrefID + m_CurTutorial_i,"true");
			//TutorialReader.Instance.TutorialFinished[m_CurTutorial_i] = true;
			
			//Blocker.gameObject.SetActive(false);
			Block(false);

			m_EndingTutorial = true;
			NextButton.gameObject.SetActive(false);
		}
	}

	private GameObject GOFromString(string str){
		switch (str) {
		case ("Stats"):
			return StatsButton;
			break;
		default:
			return null;
		}
	}


	//This method test whether or not to start the tutorial.
	private bool StartConditions(int id){
		//if (TutorialReader.Instance.TutorialFinished[id] == true) return false;
		if (PlayerPrefs.GetString (TutorialPlayerPrefID + id) == "true")
						return false;
		
		//Maybe consider replacing int with an enum? So that they're easier to identify.
		switch (id) {
		case (0):
			return true;
			break;
		default:
			return false;
			break;
		}
	}

	public void ResetTutorials(){
		for (int i = 0; i < Tutorials.Length; i++) {
			PlayerPrefs.SetString(TutorialPlayerPrefID + i,"false");
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
}
















