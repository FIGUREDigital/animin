using UnityEngine;
using System.Collections;

public class GatewayButton : MonoBehaviour {


	[SerializeField]
	private bool mActive;
	private ParentalGateway gateway;

	void OnClick()
	{
		if(mActive)
		{
			gateway.Pass();
		}
		else
		{
			gateway.Fail();
		}
	}

}
