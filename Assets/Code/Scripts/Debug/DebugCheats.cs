﻿using UnityEngine;
using System.Collections;

public enum CheatButtons
{
	NotSet,
	Birthday,
	Tutorial,
	Evolution,
	EvolutionMarker1,
	EvolutionMarker2,
	Evolve,
	Devolve,
	GiveZeff,
	TakeZeff,
	DebugToggle,

}

public class DebugCheats : MonoBehaviour 
{
	public CheatButtons button = CheatButtons.NotSet;
	void OnClick()
	{
		switch(button)
		{

		case CheatButtons.Birthday:
			AchievementsScript.Singleton.Show(AchievementTypeId.Birthday, 0);
			break;
		case CheatButtons.Tutorial:
			AchievementsScript.Singleton.Show(AchievementTypeId.Tutorial, 0);
			break;
		case CheatButtons.Evolution:
			AchievementsScript.Singleton.Show(AchievementTypeId.Evolution, 0);
			break;
		case CheatButtons.EvolutionMarker1:
			AchievementsScript.Singleton.Show(AchievementTypeId.EvolutionMarkerOne, 0);
			break;
		case CheatButtons.EvolutionMarker2:
			AchievementsScript.Singleton.Show(AchievementTypeId.EvolutionMarkerTwo, 0);
			break;
		case CheatButtons.Evolve:
			switch(PersistentData.Singleton.AniminEvolutionId)
			{
			case AniminEvolutionStageId.Baby:
				PersistentData.Singleton.ZefTokens = EvolutionManager.Instance.BabyEvolutionThreshold + 50;
				break;
			case AniminEvolutionStageId.Kid:
				PersistentData.Singleton.ZefTokens = EvolutionManager.Instance.KidEvolutionThreshold + 50;
				break;
			case AniminEvolutionStageId.Adult:
			default:
				break;
			}
			break;
		case CheatButtons.Devolve:
			switch(PersistentData.Singleton.AniminEvolutionId)
			{
			case AniminEvolutionStageId.Baby:
			default:
				break;
			case AniminEvolutionStageId.Kid:
				PersistentData.Singleton.ZefTokens = EvolutionManager.Instance.BabyEvolutionThreshold - 50;
				break;
			case AniminEvolutionStageId.Adult:
				PersistentData.Singleton.ZefTokens = EvolutionManager.Instance.KidEvolutionThreshold - 50;
				break;
			}
			break;
		case CheatButtons.GiveZeff:
			EvolutionManager.Instance.AddZef();
			break;
		case CheatButtons.TakeZeff:
			PersistentData.Singleton.ZefTokens--;
			break;
		case CheatButtons.DebugToggle:
			GameObject go = gameObject.transform.parent.FindChild("Toggleable").gameObject;
			go.SetActive(!go.activeSelf);
			break;

		case CheatButtons.NotSet:
		default:
			break;
		}
	}
}