using UnityEngine;
using System.Collections;

public class FlashObjectScript : MonoBehaviour 
{
	private enum StateId
	{
		Intro,
		FlashUp1,
		FlashUp2,
		FlashDown1,
		FlashDown2,
		Outro,
	}

	private Shader FlashShader;
	private Shader savedShader;
	private StateId State;
	private float Lerp;

	void Update() 
	{
		switch(State)
		{
		case StateId.Intro:
			FlashShader = Shader.Find("Custom/Flash Select");
			savedShader = renderer.material.shader;
			renderer.material.shader = FlashShader;
			Lerp = 0;
			State = StateId.FlashUp1;
			renderer.material.SetFloat("_BlendFactor", Lerp);
			break;

		case StateId.FlashUp1:
		case StateId.FlashUp2:

			Lerp += Time.deltaTime * 5;
			if(Lerp >= 1) 
			{
				Lerp = 1;
				State = StateId.FlashDown1;
			}

			renderer.material.SetFloat("_BlendFactor", Lerp);
			break;

		case StateId.FlashDown1:
		case StateId.FlashDown2:
			Lerp -= Time.deltaTime * 5;
			if(Lerp <= 0) 
			{
				Lerp = 0;
				State = StateId.Outro;
			}
			
			renderer.material.SetFloat("_BlendFactor", Lerp);
			break;

		case StateId.Outro:
			renderer.material.shader = savedShader;
			Destroy(this);
			break;
		}

		
	}
}