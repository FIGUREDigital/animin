using UnityEngine;
using System.Collections;

public class TutorialHandler : MonoBehaviour {


	[SerializeField]
	private GameObject TutorialUIParent;
	[SerializeField]
	private GameObject Blocker;
	[SerializeField]
	private Animator WormAnimator;
	[SerializeField]
	private UILabel TutorialText;
	[SerializeField]
	private UIButton NextButton;

	//[SerializeField]
	//private GameObject[] ExitButtons;

	private bool m_PlayingTutorial, m_EndingTutorial;
	private int m_CurTutorial_i;
	private int m_Letter_i, m_Lesson_i, m_Entry_i;

	private Tutorial[] Tutorials{
		get { return TutorialReader.Instance.Tutorials;}
	}

	// Use this for initialization
	void Start () {
		Blocker.SetActive (false);
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
					Blocker.SetActive(true);
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
			string text = Tutorials[m_CurTutorial_i].Lessons[m_Lesson_i].TutEntries[m_Entry_i].text;
			if (text.Length >= m_Letter_i){

				TutorialText.text = text.Substring(0,m_Letter_i++);
				NextButton.gameObject.SetActive(false);
			} else {
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
	}

	public void NextButtonPress(){

		int maxEntries = Tutorials [m_CurTutorial_i].Lessons [m_Lesson_i].TutEntries.Length;
		int maxLessons = Tutorials [m_CurTutorial_i].Lessons.Length;


		//Debug.Log ("Testing Entry : ["+m_Entry_i+":"+maxEntries+"]; Lesson : ["+m_Lesson_i+":"+maxLessons+"];");

		
		m_Letter_i = 0;
		
		if (++m_Entry_i >= maxEntries) {
			m_Entry_i =0;
			if (++m_Lesson_i >= maxLessons) {
				WormAnimator.SetTrigger ("worm_GoIn");
				TutorialReader.Instance.TutorialFinished[m_CurTutorial_i] = true;
				m_EndingTutorial = true;
				NextButton.gameObject.SetActive(false);
			}
		}
	}

	//public void CorrectButton(){
	//}

	private bool StartConditions(int id){
		if (TutorialReader.Instance.TutorialFinished[id] == true) return false;

		//Maybe consider replacing int with an enum? So that they're easier to identify.
		switch (id) {
		case (0):
			return true;
			break;
		case (1):
			return false;
			break;
		default:
			return false;
			break;
		}
	}
}
