using UnityEngine;
using System.Collections;

public class GatewayButton : MonoBehaviour {


	[SerializeField]
	private bool mActive;
	private ParentalGateway gateway;

	void Start()
	{
		gateway = gameObject.transform.parent.parent.parent.GetComponent<ParentalGateway>();
	}

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
