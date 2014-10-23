using UnityEngine;
using System.Collections;

public class AchievementsScript : MonoBehaviour 
{
	public static AchievementsScript Singleton;

	public GameObject AchievementObject;
	public UISprite MedalIcon;
	public UILabel Title;
	public UILabel Description;
	public UISprite BackgroundGradient;

	private float Timer;
	private float Alpha = 0;
	private float VerticalMovement;

	void Awake()
	{
		Singleton = this;
		//Show(AchievementTypeId.Gold, 400);
	}

	public void Show(AchievementTypeId id, int points)
	{
		AchievementObject.SetActive(true);
		Timer = 5;
		VerticalMovement = 0;
		AchievementObject.GetComponent<UIWidget>().bottomAnchor.absolute = -509;

		switch(id)
		{
		case AchievementTypeId.Gold:
			{
				Title.text = "Gold Award!";
				BackgroundGradient.spriteName = "achievementYellow";
				Description.text = string.Format(@"Well done! you scored {0} points.", points);
				MedalIcon.spriteName = @"achievementBGBronze";
				break;
			}
		case AchievementTypeId.Bronze:
			{
				Title.text = "Bronze Award!";
				BackgroundGradient.spriteName = "achievementYellow";
				Description.text = string.Format(@"Well done! you scored {0} points.", points);
				MedalIcon.spriteName = @"achievementIconGold";
				break;
			}
		case AchievementTypeId.Silver:
			{
				Title.text = "Silver Award!!";
				BackgroundGradient.spriteName = "achievementYellow";
			Description.text = string.Format(@"Well done! you scored {0} points.", points);
				MedalIcon.spriteName = @"achievementIconSilver";
				break;
			}
		case AchievementTypeId.Achievement:
			{
				Title.text = "Achievement!";
				BackgroundGradient.spriteName = "achievementGreen";
				Description.text = "Congratulations, you got an achievement.";
				MedalIcon.spriteName = @"achievementIconStar";
				break;
			}

		case AchievementTypeId.Evolution:
			{
				Title.text = "Your Animin has evolved!";
				BackgroundGradient.spriteName = "achievementGreen";
				Description.text = "Well done! Keep taking care of your Animin and training them up.";
				MedalIcon.spriteName = @"achievementEvolution";
				break;
			}

		case AchievementTypeId.EvolutionExclamation:
			{
				Title.text = "Your animin has grown!";
				BackgroundGradient.spriteName = "achievementGreen";
				Description.text = "Well done! Keep taking care of your Animin and training them up.";
				MedalIcon.spriteName = @"achievementMarker2";
				break;
			}
		case AchievementTypeId.EvolutionStar:
			{
				Title.text = "You unlocked a surprise!";
				BackgroundGradient.spriteName = "achievementGreen";
				Description.text = "Well done! Keep taking care of your Animin and training them up.";
				MedalIcon.spriteName = @"achievementMarker3";
				break;
			}
		case AchievementTypeId.Tutorial:
			{
				Title.text = "Yo!";
				BackgroundGradient.spriteName = "achievementOrange";
				Description.text = "Well done! Keep taking care of your Animin and training them up.";
				MedalIcon.spriteName = @"achievementWorm";
				break;
			}
		case AchievementTypeId.Birthday:
			{
				Title.text = "Happy Birthday!";
				BackgroundGradient.spriteName = "achievementRed";
				Description.text = "Well done! Keep taking care of your Animin and training them up.";
				MedalIcon.spriteName = @"achievementBirthday";
				break;
			}
		}
	}
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Timer -= Time.deltaTime;
		if(Timer <= 0)
		{
			Alpha -= Time.deltaTime * 2.0f;
			if(Alpha <= 0)
			{
				Alpha = 0;
				AchievementObject.SetActive(false);
			}
		}
		else
		{
//			Alpha += Time.deltaTime * 0.8f;
//			if(Alpha >= 1) 
//			{
//				Alpha = 1;
//			}
		}

		if(Timer > 0)
		{
			Alpha = Mathf.Lerp(Alpha, 1, Time.deltaTime * 2.6f);
			float height = Mathf.Lerp(AchievementObject.GetComponent<UIWidget>().bottomAnchor.absolute, 245, Time.deltaTime * 2.6f);
			AchievementObject.GetComponent<UIWidget>().bottomAnchor.absolute = (int)height;
		}

		
		for(int i=0;i<AchievementObject.transform.childCount;++i)
		{
			GameObject transform = AchievementObject.transform.GetChild(i).gameObject;
			
			UISprite sprite = transform.GetComponent<UISprite>();
			if(sprite != null)
			{
				sprite.color = new Color(1,1,1, Alpha);
			}
			
			UILabel label = transform.GetComponent<UILabel>();
			if(label != null)
			{
				label.color = new Color(1,1,1, Alpha);
			}
			
		}
	}
}

public enum AchievementTypeId
{
	None = 0,
	Gold,
	Silver,
	Bronze,
	Evolution,
	Achievement,
	Tutorial,
	EvolutionExclamation,
	EvolutionStar,
	Birthday,
}
