using UnityEngine;
using System.Collections;

public class ThrowAnimationScript : MonoBehaviour 
{
	private enum StateEnum
	{
		None = 0,
		Begin,
		Throw,
		End,
	}

	StateEnum State;
	Vector3 Direction;

	float VerticalSpeed;

	public void Begin(Vector3 direction)
	{
		Direction = direction;
		State = StateEnum.Begin;

		//Rigidbody body = this.gameObject.AddComponent<Rigidbody>();
		//body.AddForce(direction * 100);

		//body.velocity = new Vector3(1 * direction.x, -1000, 1 * direction.z);
		//Physics.gravity = new Vector3(0, -10.0F, 0);
		VerticalSpeed = -220;

	}

	// Update is called once per frame
	void Update () 
	{

		VerticalSpeed = VerticalSpeed + (Time.deltaTime * 600);

		switch(State)
		{
			case StateEnum.Begin:
			{
				this.transform.position += Direction * Time.deltaTime * 100;
				this.transform.position -= new Vector3(0, VerticalSpeed * Time.deltaTime, 0);
				if(this.transform.localPosition.y <=0)
			{
				//Destroy(this.rigidbody);
				Destroy(this);
				this.gameObject.layer = LayerMask.NameToLayer("Default");
			}
				break;
			}

			case StateEnum.Throw:
			{
				break;
			}

			case StateEnum.End:
			{
				break;
			}
		}
	
	}
}
