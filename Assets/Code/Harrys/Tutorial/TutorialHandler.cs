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
	private UIWidget Blocker;
	[SerializeField]
	private Animator WormAnimator;
	[SerializeField]
	private UILabel TutorialText;
	[SerializeField]
	private UIButton NextButton;

	[SerializeField]
	private GameObject StatsButton;

	private bool m_PlayingTutorial, m_EndingTutorial;
	private int m_CurTutorial_i;
	private int m_Letter_i, m_Lesson_i, m_Entry_i;
	
	private int m_SavedDepth;
	private GameObject m_CurrentListening;
	private bool m_WaitingForInput;

	// Use this for initialization
	void Start () {
		Blocker.gameObject.SetActive (false);
		WormAnimator.gameObject.SetActive(false);
		TutorialUIParent.SetActive (false);

		TutorialReader.Instance.Deserialize ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!m_PlayingTutorial) {
			//I HATE MONODEVELOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
//																								OOOOOOOOOOOOOOOOOOOOOOOOOP!
			
			//TutorialReader.Instance.test();
			for (int i = 0; i < Tutorials.Length; i++) {
				if (StartConditions (i)) {
					TutorialUIParent.SetActive (true);
					WormAnimator.gameObject.SetActive(true);
					Blocker.gameObject.SetActive(true);
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
			if (!m_WaitingForInput){
				string text = Tutorials[m_CurTutorial_i].Lessons[m_Lesson_i].TutEntries[m_Entry_i].text;
				if (text.Length >= m_Letter_i){

					TutorialText.text = text.Substring(0,m_Letter_i++);
					NextButton.gameObject.SetActive(false);
				} else {
					NextButton.gameObject.SetActive(true);
				}
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
	}

	public void NextButtonPress(){

		int maxEntries = Tutorials [m_CurTutorial_i].Lessons [m_Lesson_i].TutEntries.Length;


		//Debug.Log ("Testing Entry : ["+m_Entry_i+":"+maxEntries+"]; Lesson : ["+m_Lesson_i+":"+maxLessons+"];");

		
		m_Letter_i = 0;
		
		if (++m_Entry_i >= maxEntries) {

			string exitstr = Tutorials [m_CurTutorial_i].Lessons [m_Lesson_i].ExitStr;
			if (exitstr != "None") {

				m_CurrentListening = GOFromString (exitstr);
				UIWidget widget = m_CurrentListening.GetComponent<UIWidget> ();
				m_SavedDepth = widget.depth;
				widget.depth = Blocker.depth + 1;

				UIEventListener.Get (m_CurrentListening).onClick += OnTutorialClick;
				
				NextButton.gameObject.SetActive(false);
				m_WaitingForInput = true;
			} else {
				NextLesson ();
			}
		}
	}

	public void OnTutorialClick(GameObject go){
		if (go == m_CurrentListening) {
			UIEventListener.Get (m_CurrentListening).onClick -= OnTutorialClick;
			m_WaitingForInput = false;
			NextLesson();
		}
	}

	public void NextLesson(){
		if (m_WaitingForInput) return;

		int maxLessons = Tutorials [m_CurTutorial_i].Lessons.Length;
		m_Entry_i =0;

		if (++m_Lesson_i >= maxLessons) {
			WormAnimator.SetTrigger ("worm_GoIn");
			TutorialReader.Instance.TutorialFinished[m_CurTutorial_i] = true;
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
		if (TutorialReader.Instance.TutorialFinished[id] == true) return false;

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
}
