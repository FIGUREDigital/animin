using UnityEngine;
using System.Collections;

public class AniminNameSprite : MonoBehaviour {

	[SerializeField]
	private UIAtlas mAtlas;
	[SerializeField]
	private string mPiSprite;
	[SerializeField]
	private string mTboSprite;
	[SerializeField]
	private string mKelseySprite;
	[SerializeField]
	private string mMandiSprite;
	// Use this for initialization
	void Start () 
	{
		UISprite sprite = GetComponent<UISprite>();
		sprite.atlas = mAtlas;

		string name = "";
		switch(PlayerProfileData.ActiveProfile.ActiveAnimin)
		{
		case AniminId.Pi:
			name = mPiSprite;
			break;
		case AniminId.Tbo:
			name = mTboSprite;
			break;
		case AniminId.Kelsey:
			name = mKelseySprite;
			break;
		case AniminId.Mandi:
			name = mMandiSprite;
			break;
		default:
			break;
		}
		sprite.spriteName = name;

	}

}
