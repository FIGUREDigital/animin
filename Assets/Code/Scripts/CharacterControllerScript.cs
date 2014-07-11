﻿using UnityEngine;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour 
{
	public Camera CameraRef;

	enum CharacterState {
		
		Idle = 0,
		
		Walking = 1,
		
		Trotting = 2,
		
		Running = 3,
		
		Jumping = 4,    
		
	}
	
	private CharacterState _characterState;
	
	// The speed when walking
	
	public float walkSpeed = 2.0F;
	
	// after trotAfterSeconds of walking we trot with trotSpeed
	
	public float trotSpeed = 4.0F;
	
	// when pressing "Fire3" button (cmd) we start running
	
	//public float runSpeed = 6.0F;
	
	
	
	public float inAirControlAcceleration = 3.0F;
	
	
	
	// How high do we jump when pressing jump and letting go immediately
	
	public float jumpHeight = 0.5F;
	
	public AnimationControllerScript animationController;
	
	// The gravity for the character
	
	public float gravity = 200.0F;
	
	// The gravity in controlled descent mode
	
	public float speedSmoothing = 100.0F;
	
	public float rotateSpeed = 500.0F;
	
	public float trotAfterSeconds = 3.0F;
	
	
	
	public bool canJump= true;
	
	
	
	private float jumpRepeatTime = 0.05F;
	
	private float jumpTimeout = 0.15F;
	
	private float groundedTimeout = 0.25F;
	
	
	
	// The camera doesnt start following the target immediately but waits for a split second to avoid too much waving around.
	
	private float lockCameraTimer = 0.0F;
	
	
	
	// The current move direction in x-z
	
	//public Vector3 moveDirection = Vector3.zero;
	
	// The current vertical speed
	
	private float verticalSpeed = 0.0F;
	
	// The current x-z move speed
	
	private float moveSpeed = 0.0F;
	
	
	
	// The last collision flags returned from controller.Move
	
	private CollisionFlags collisionFlags ; 
	
	
	
	// Are we jumping? (Initiated with jump button and not grounded yet)
	
	private bool jumping = false;
	
	private bool jumpingReachedApex = false;
	
	
	
	// Are we moving backwards (This locks the camera to not do a 180 degree spin)
	
	private bool movingBack = false;
	
	// Is the user pressing any keys?
	
	private bool isMoving = false;
	
	// When did the user start walking (Used for going into trot after a while)
	
	private float walkTimeStart = 0.0F;
	
	// Last time the jump button was clicked down
	
	private float lastJumpButtonTime = -10.0F;
	
	// Last time we performed a jump
	
	private float lastJumpTime = -1.0F;
	
	
	
	
	
	// the height we jumped from (Used to determine for how long to apply extra jump power after jumping.)
	
	private float lastJumpStartHeight = 0.0F;
	
	
	
	
	
	private Vector3 inAirVelocity = Vector3.zero;
	
	
	
	private float lastGroundedTime = 0.0F;
	
	
	
	
	
	private bool isControllable = true;

	public Vector3 MovementDirection;
	//public float HorizontalDirection;
	//public float VerticalDirection;
	public bool IsResetFalling;
	
	public MinigameCollectorScript MinigameRef;
	
	// Use this for initialization
	
	void  Awake (){
		
		//moveDirection = transform.TransformDirection(Vector3.forward);
		
	
	}


	public void  UpdateSmoothedMovementDirection ()
	{
		

		bool grounded = IsGrounded();

			
		movingBack = false;
		
		
		
		bool wasMoving= isMoving;
		
		isMoving = Mathf.Abs (MovementDirection.x) > 0.0f || Mathf.Abs (MovementDirection.z) > 0.0f;
		
		
		
		// Target direction relative to the camera
		
		Vector3 targetDirection= MovementDirection;//h * right + v * forward;

		
		
		// Grounded controls
		
		if (grounded)
		{
			IsResetFalling = false;
			
			// Lock camera for short period when transitioning moving & standing still
			
			lockCameraTimer += Time.deltaTime;
			
			if (isMoving != wasMoving)
				
				lockCameraTimer = 0.0f;
			
			
			
			// We store speed and direction seperately,
			
			// so that when the character stands still we still have a valid forward direction
			
			// moveDirection is always normalized, and we only update it if there is user input.
			
			if (targetDirection != Vector3.zero)
				
			{
				
				// If we are really slow, just snap to the target direction
				
				//if (moveSpeed < walkSpeed * 0.9f && grounded)
					
				{
					
					//moveDirection = targetDirection.normalized;
					
				}
				
				// Otherwise smoothly turn towards it
				
				//else
					
				{
					
					//moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime * 0.1f, 1000);
					
					
					
					//moveDirection = moveDirection.normalized;
					
				}
				
			}
			
			
			
			// Smooth the speed based on the current target direction
			
			float curSmooth= speedSmoothing * Time.deltaTime;
			
			
			
			// Choose target speed
			
			//* We want to support analog input but make sure you cant walk faster diagonally than just forward or sideways
			
			float targetSpeed= Mathf.Min(targetDirection.magnitude, 1.0f);
			
			
			
			//_characterState = CharacterState.Idle;
			
			
			
			// Pick speed modifier
			
			/*if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
				
			{
				
				targetSpeed *= runSpeed;
				
				_characterState = CharacterState.Running;
				
			}
			
			else if (Time.time - trotAfterSeconds > walkTimeStart)
				
			{
				
				targetSpeed *= trotSpeed;
				
				_characterState = CharacterState.Trotting;
				
			}
			
			else
				
			{*/
				
				targetSpeed *= walkSpeed;
				
				//_characterState = CharacterState.Walking;
				
			//}
			
			
			
			moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, curSmooth);
					
			
			// Reset walk time start when we slow down
			
			if (moveSpeed < walkSpeed * 0.3f)
				
				walkTimeStart = Time.time;
			
		}
		
		// In air controls
		
		else
			
		{
			
			// Lock camera while in air
			
			if (jumping)
				
				lockCameraTimer = 0.0f;
			
			
			
			if (isMoving)
				
				inAirVelocity += targetDirection.normalized * Time.deltaTime * inAirControlAcceleration;
			
		}
		
		
		
		
		
		
		
	}
	
	void  ApplyJumping ()
	{	
		// Prevent jumping too fast after each other	
		if (lastJumpTime + jumpRepeatTime > Time.time)
			return;

		if (IsGrounded()) 
		{
			// Jump
			
			// - Only when pressing the button down
			
			// - With a timeout so you can press the button slightly before landing     
			
			if (canJump && Time.time < lastJumpButtonTime + jumpTimeout) {
				
				verticalSpeed = CalculateJumpVerticalSpeed (jumpHeight);
				
				SendMessage("DidJump", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public bool PressedJumb;

	
	void  ApplyGravity ()
	{
		if (isControllable) // don't move player at all if not controllable.
		{
			// Apply gravity
			bool jumpButton = PressedJumb;//Input.GetButton("Jump");

			// When we reach the apex of the jump we send out a message
			if (jumping && !jumpingReachedApex && verticalSpeed <= 0.0f)
			{
				jumpingReachedApex = true;
				SendMessage("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
			}
			
			if (IsGrounded ())
				verticalSpeed = 0.0f;
			else
				verticalSpeed -= gravity * Time.deltaTime;
		}
	}
	
	float  CalculateJumpVerticalSpeed ( float targetJumpHeight  ){
		
		// From the jump height and gravity we deduce the upwards speed 
		
		// for the character to reach at the apex.
		
		return Mathf.Sqrt(2 * targetJumpHeight * gravity);
		
	}   
	
	void  DidJump (){
		
		jumping = true;
		
		jumpingReachedApex = false;
		
		lastJumpTime = Time.time;
		
		lastJumpStartHeight = transform.position.y;
		
		lastJumpButtonTime = -10;
		
		
		
		_characterState = CharacterState.Jumping;
		
	}
	
	void  Update (){
		
		
		
		if (!isControllable)
			
		{
			
			// kill all inputs if not controllable.
			
			Input.ResetInputAxes();
			
		}
		
		
		
		if (PressedJumb)
			
		{
			
			lastJumpButtonTime = Time.time;
			
		}
		
		
		
		UpdateSmoothedMovementDirection();
		
		
		
		// Apply gravity
		
		// - extra power jump modifies gravity
		
		// - controlledDescent mode modifies gravity
		
		ApplyGravity ();
		
		
		
		// Apply jumping logic
		ApplyJumping ();
		

		// Calculate actual motion
		
		Vector3 movement= MovementDirection * moveSpeed + new Vector3 (0, verticalSpeed, 0) + inAirVelocity;
		
		movement *= Time.deltaTime;


		if(IsResetFalling)
		{
			movement.x = 0;
			movement.z = 0;
			speedSmoothing = 10000;
		}
		else
		{
			speedSmoothing = 90;
		}
		
		
		// Move the controller
		CharacterController controller = GetComponent<CharacterController>();
		collisionFlags = controller.Move(movement);

		
		// Set rotation to the move direction
		
		/*if (IsGrounded())
		{

			//transform.rotation = Quaternion.LookRotation(moveDirection);
		}   
		
		else
			
		{
			
			Vector3 xzMove= movement;
			
			xzMove.y = 0;
			
			if (xzMove.sqrMagnitude > 0.001f)
				
			{
				
				transform.rotation = Quaternion.LookRotation(xzMove);
				
			}
			
		}   */
		
		
		
		// We are in jump mode but just became grounded
		
		if (IsGrounded())
			
		{
			
			lastGroundedTime = Time.time;
			
			inAirVelocity = Vector3.zero;
			
			if (jumping)
				
			{
				
				jumping = false;
				
				SendMessage("DidLand", SendMessageOptions.DontRequireReceiver);
				
			}
			
		}

		UpdateRotationLookAt();
	
		PressedJumb = false;
	}
	
	void  OnControllerColliderHit ( ControllerColliderHit hit   ){
		
		//  Debug.DrawRay(hit.point, hit.normal);

		//Debug.Log("COLLISION: " + hit.gameObject.name);

		if(hit.gameObject.tag == "Items" && this.GetComponent<CharacterProgressScript>().IsGoingToPickUpObject == hit.gameObject)
		{

			if(hit.gameObject.GetComponent<ReferencedObjectScript>().Reference.GetComponent<UIPopupItemScript>().Type == PopupItemType.Food)
			{
				if(this.GetComponent<CharacterProgressScript>().OnInteractWithPopupItem(hit.gameObject.GetComponent<ReferencedObjectScript>().Reference.GetComponent<UIPopupItemScript>()))
				{
					this.GetComponent<CharacterProgressScript>().GroundItems.Remove(hit.gameObject);
					Destroy(hit.gameObject);

				}
			}
			else
			{
				this.GetComponent<CharacterProgressScript>().PickupItem(hit.gameObject/*.GetComponent<ReferencedObjectScript>().Reference.GetComponent<UIPopupItemScript>()*/);
			}
				//Destroy(hit.gameObject);
		}

		if(hit.gameObject.tag == "EnemyJumbTop")
		{
			// hit from above
			if(hit.normal.y >= 0.5f)
			{
				UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.GetComponent<MinigameCollectorScript>().OnEvilCharacterHitFromTop(hit.gameObject);
			}
			else
			{
				//Debug.Log("HIT FROM SIDES");

				UIGlobalVariablesScript.Singleton.CubeRunnerMinigameSceneRef.GetComponent<MinigameCollectorScript>().OnEvilCharacterHit(hit.gameObject);


				//CharacterController controller = GetComponent<CharacterController>();

			 	//Vector3 movement =	-hit.moveDirection * moveSpeed + new Vector3 (0, verticalSpeed, 0) + inAirVelocity;
				//
				//collisionFlags = controller.Move(movement);
			}
		}

		if (hit.moveDirection.y > 0.01f) 
			
			return;
		
	}   
	
	float  GetSpeed (){
		
		return moveSpeed;
		
	}
	
	
	
	bool  IsJumping (){
		
		return jumping;
		
	}

	private void UpdateRotationLookAt()
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, RotateDirectionLookAt, Time.deltaTime * 6);
	}

	private Quaternion RotateDirectionLookAt = Quaternion.Euler(0, 180, 0);

	public void RotateToLookAtPoint(Vector3 worldPoint)
	{
		//float angle = Vector3.Angle(transform.forward, Vector3.Normalize(worldPoint - transform.position));
//		Debug.Log("ANGLE: " + angle.ToString());

		RotateDirectionLookAt = Quaternion.LookRotation(Vector3.Normalize(worldPoint - transform.position));
	//	transform.Rotate(new Vector3(0, angle, 0));
	}
	
	
	
	bool  IsGrounded (){
		
		return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
		
	}
	
	
	
//	Vector3  GetDirection (){
//		
//		return moveDirection;
//		
//	}
	
	
	
	bool  IsMovingBackwards (){
		
		return movingBack;
		
	}
	
	
	
	float  GetLockCameraTimer (){
		
		return lockCameraTimer;
		
	}
	
	
	
	bool IsMoving (){
		
		return MovementDirection != Vector3.zero;
		
	}
	
	
	
	bool  HasJumpReachedApex (){
		
		return jumpingReachedApex;
		
	}
	
	
	
	bool  IsGroundedWithTimeout (){
		
		return lastGroundedTime + groundedTimeout > Time.time;
		
	}
	
	
	
	void  Reset (){
		
		gameObject.tag = "Player";
		
	}
	
	
	
}