using UnityEngine;
using System.Collections;

using UnityEngine;



public class JoystiqScript : MonoBehaviour {

	public UISprite ThumbpadFront;
	public UISprite ThumbpadBack;

	private float LeftAnchorStartPosition;
	private float RightAnchorStartPosition;
	private float TopAnchorStartPosition;
	private float BottomAnchorPosition;

	public static Vector2 VJRvector;    // Joystick's controls in Screen-Space.
	
	public static Vector2 VJRnormals;   // Joystick's normalized controls.
	
	public static bool VJRdoubleTap;    // Player double tapped this Joystick.
	
	//public Color activeColor;           // Joystick's color when active.
	
	//public Color inactiveColor;         // Joystick's color when inactive.
	
	//public Texture2D joystick2D;        // Joystick's Image.
	
	//public Texture2D background2D;      // Background's Image.
	//public Texture2D jumb2D;
	
	//private GameObject backOBJ;         // Background's Object.

	//private GameObject jumbOBJ;

	//private GUITexture jumbTexture;
	
	//private GUITexture joystick;        // Joystick's GUI.
	
	//private GUITexture background;      // Background's GUI.
	
	private Vector2 origin;             // Touch's Origin in Screen-Space.
	
	private Vector2 position;           // Pixel Position in Screen-Space.
	
	private int size;                   // Screen's smaller side.
	
	private float length;               // The maximum distance the Joystick can be pushed.
	
	private bool gotPosition;           // Joystick has a position.
	
	private int fingerID;               // ID of finger touching this Joystick.
	
	private int lastID;                 // ID of last finger touching this Joystick.
	
	private float tapTimer;             // Double-tap's timer.
	
	private bool enable;                // VJR external control.
	
	
	public CharacterControllerScript CharacterControllerRef;
	//
	
	
	
	/*public void DisableJoystick() 
	{

		joystick.enabled = false;
		background.enabled = false;
		backOBJ.SetActive(false);
		enable = false; 
		ResetJoystick();
	}
	
	public void EnableJoystick() 
	{
		//joystick.enabled = true;
		background.enabled = true;
		backOBJ.SetActive(true);
		enable = true;
		ResetJoystick();
	}*/
	
	
	
	//
	/*
	public void DisableJoystick() 
	{
		enable = false; 
		ResetJoystick();
	}
	public void EnableJoystick() 
	{
		enable = true; 
		ResetJoystick();
	}
	
	public void ResetJoystick() 
	{
		
		VJRvector = new Vector2(0,0); VJRnormals = VJRvector;
		lastID = fingerID; fingerID = -1; tapTimer = 0.150f;
		//joystick.color = inactiveColor; position = origin; gotPosition = false;
		
	}
	*/
	
	
	/*private Vector2 GetRadius(Vector2 midPoint, Vector2 endPoint, float maxDistance) {
		
		Vector2 distance = endPoint;
		
		if (Vector2.Distance(midPoint,endPoint) > maxDistance) {
			
			distance = endPoint-midPoint; distance.Normalize();
			
			return (distance*maxDistance)+midPoint;
			
		}
		
		return distance;
		
	}
	
	
	
	private void GetPosition() {
		
		foreach (Touch touch in Input.touches) 
		{	
			fingerID = touch.fingerId;
			
			if (fingerID >= 0 && fingerID < Input.touchCount) 
			{

				if(Input.GetTouch(fingerID).position.x < Screen.width/3 && Input.GetTouch(fingerID).position.y < Screen.height/3 && Input.GetTouch(fingerID).phase == TouchPhase.Began)
				{
					
					position = Input.GetTouch(fingerID).position;

					origin = position;

					//Debug.Log("GET POSITION@:" + position.ToString());
					
//					joystick.texture = joystick2D; 
//					joystick.color = activeColor;
//					
//					background.texture = background2D; 
//					background.color = activeColor;
					
					if (fingerID == lastID && tapTimer > 0) 
					{
						VJRdoubleTap = true;
					} 

					gotPosition = true;
					
				}
			}
		}
	}*/
	
	
	/*
	private void GetConstraints() {
		
		if (origin.x < (ThumbpadBack.transform.position.x)) 
		{
			origin.x = ThumbpadBack.transform.position.x;
		}
		
		if (origin.y < (ThumbpadBack.transform.position.y)) 
		{
			origin.y = (ThumbpadBack.transform.position.y);
		}
		
		if (origin.x > (ThumbpadBack.transform.position.x + ThumbpadBack.width)) 
		{
			origin.x = (ThumbpadBack.transform.position.x + ThumbpadBack.width);
		}
		
		if (origin.y > (ThumbpadBack.transform.position.y + ThumbpadBack.height)) 
		{
			origin.y = (ThumbpadBack.transform.position.y + ThumbpadBack.height);
		}
		
	}
	
	
	
	private Vector2 GetControls(Vector2 pos, Vector2 ori) {
		
		Vector2 vector = new Vector2();
		
		if (Vector2.Distance(pos,ori) > 0)
		{
			vector = new Vector2(pos.x-ori.x,pos.y-ori.y);
		}
		
		return vector;
	}
	
	
	*/
	//
	
	
	
	private void Awake() {
		
		/*gameObject.transform.localScale = new Vector3(0,0,0);
		gameObject.transform.position = new Vector3(0,0,999);

		if (Screen.width > Screen.height) 
		{
			size = Screen.height;
		} 
		else 
		{
			size = Screen.width;
		} 

		VJRvector = new Vector2(0,0);*/

		/*
		joystick = gameObject.AddComponent("GUITexture") as GUITexture;
		joystick.texture = joystick2D; joystick.color = inactiveColor;

		backOBJ = new GameObject("VJR-Joystick Back");
		backOBJ.transform.parent = this.transform;
		backOBJ.transform.localScale = new Vector3(0,0,0);
		background = backOBJ.AddComponent("GUITexture") as GUITexture;
		background.texture = background2D; background.color = inactiveColor;


		jumbOBJ = new GameObject("jumb button");
		jumbOBJ.transform.parent = this.transform;
		jumbOBJ.transform.localScale = new Vector3(0,0,0);
		jumbTexture = jumbOBJ.AddComponent("GUITexture") as GUITexture;

		jumbTexture.texture = jumb2D; 
		jumbTexture.color = Color.white;

		*/

		fingerID = -1; 
		lastID = -1; 
		VJRdoubleTap = false; 
		tapTimer = 0; 
		length = 140;

		//position = new Vector2(ThumbpadBack.transform.position.x + ThumbpadBack.width / 2, ThumbpadBack.transform.position.y + ThumbpadBack.height / 2); //new Vector2((Screen.width/3)/2,(Screen.height/3)/2);; 

		//origin = position;
		//gotPosition = false; EnableJoystick(); enable = true;
	}

	void Start()
	{
		LeftAnchorStartPosition = ThumbpadFront.leftAnchor.absolute;
		RightAnchorStartPosition = ThumbpadFront.rightAnchor.absolute;
		TopAnchorStartPosition = ThumbpadFront.topAnchor.absolute;
		BottomAnchorPosition = ThumbpadFront.bottomAnchor.absolute;
	}
	

	private Vector2 finalMovementDirection = Vector2.zero;
	
	private void Update() {

		/*if(Input.GetKey(KeyCode.A))
		{
			Debug.Log("DOING A: " + ThumbpadBack.leftAnchor.relative.ToString() + "_" + ThumbpadBack.leftAnchor.absolute.ToString() + "_" + ThumbpadFront.width.ToString());


			ThumbpadFront.leftAnchor.Set(ThumbpadFront.leftAnchor.relative, ThumbpadFront.leftAnchor.absolute + 1);
			ThumbpadFront.rightAnchor.Set(ThumbpadFront.rightAnchor.relative, ThumbpadFront.rightAnchor.absolute + 1);
			ThumbpadFront.ResetAnchors();
			ThumbpadFront.UpdateAnchors();
		}*/

		bool isButtonDown = false;
		Vector3 mousePosition = Vector3.zero;

#if UNITY_EDITOR
		if(Input.GetMouseButton(0))
		{
			isButtonDown = true;
			mousePosition = Input.mousePosition;
		}
#endif

		for(int i=0;i<Input.touchCount;++i)
		{
			TouchPhase phase = Input.GetTouch(i).phase;

			if(fingerID == -1)
			{
				Vector3 bottomLeftWorld = UICamera.mainCamera.WorldToScreenPoint(ThumbpadBack.worldCorners[0]);
				Vector3 topRightWorld = UICamera.mainCamera.WorldToScreenPoint(ThumbpadBack.worldCorners[2]);

				if(Input.GetTouch(i).position.x < topRightWorld.x 
				   && Input.GetTouch(i).position.y < topRightWorld.y 
				   && Input.GetTouch(i).phase == TouchPhase.Began)
				{
					fingerID = Input.GetTouch(i).fingerId;
					isButtonDown = true;
					mousePosition = Input.GetTouch(i).position;
				}
			}
			else
			{
				if(Input.GetTouch(i).fingerId == fingerID)
				{
					isButtonDown = true;
					mousePosition = Input.GetTouch(i).position;
				}
			}
		}

		if(!isButtonDown) fingerID = -1;





		float movementSpeed = 0;

		bool fingerTouchValid = false;

		if(isButtonDown)
		{
			//float ffff = (ThumbpadBack.worldCorners[2].x - ThumbpadBack.worldCorners[0].x) * ThumbpadBack.width;
			fingerTouchValid = true;
			Vector3 bottomLeftWorld = UICamera.mainCamera.WorldToScreenPoint(ThumbpadBack.worldCorners[0]);
			Vector3 topRightWorld = UICamera.mainCamera.WorldToScreenPoint(ThumbpadBack.worldCorners[2]);
			//Debug.Log("bottomLeftWorld: " + bottomLeftWorld.ToString());
			//Debug.Log("topRightWorld: " + topRightWorld.ToString());


			Vector3 middle = bottomLeftWorld + (topRightWorld - bottomLeftWorld) / 2;

			//float invertedY = Screen.height - Input.mousePosition.y;

			//if(Input.mousePosition.x >= bottomLeftWorld.x && Input.mousePosition.x <= topRightWorld.x)
			{
				//if(Input.mousePosition.y >= bottomLeftWorld.y && Input.mousePosition.y <= topRightWorld.y)
				{




					float horizontalDistance = (mousePosition.x - middle.x);
					horizontalDistance /= ((topRightWorld.x - bottomLeftWorld.x) / 2);
					if(horizontalDistance < -1) horizontalDistance = -1;
					if(horizontalDistance > 1) horizontalDistance = 1;

					float verticalDistance = (mousePosition.y - middle.y);
					verticalDistance /= ((topRightWorld.y - bottomLeftWorld.y) / 2);
					if(verticalDistance < -1) verticalDistance = -1;
					if(verticalDistance > 1) verticalDistance = 1;

					float maxRadius = (topRightWorld.y - middle.y);// + (topRightWorld.y - middle.y) * 0.8f;
					Vector2 directionVector = (new Vector2(mousePosition.x, mousePosition.y) - new Vector2(middle.x, middle.y));
					directionVector.Normalize();

					float currentDistance = Vector2.Distance(new Vector2(mousePosition.x, mousePosition.y), new Vector2(middle.x, middle.y));
					if(currentDistance >= maxRadius) currentDistance = maxRadius;
					movementSpeed = currentDistance / maxRadius;
					//Debug.Log("speeD: " + movementSpeed.ToString());

					float lerpH = (directionVector.x * Mathf.Abs(horizontalDistance)) * ((ThumbpadFront.width * 0.9f) / 2);
					float lerpV = (directionVector.y * Mathf.Abs(verticalDistance)) * ((ThumbpadFront.height * 0.9f) / 2);

					ThumbpadFront.leftAnchor.Set(ThumbpadFront.leftAnchor.relative, LeftAnchorStartPosition + lerpH);
					ThumbpadFront.rightAnchor.Set(ThumbpadFront.rightAnchor.relative, RightAnchorStartPosition + lerpH);

					ThumbpadFront.topAnchor.Set(ThumbpadFront.topAnchor.relative, TopAnchorStartPosition + lerpV);
					ThumbpadFront.bottomAnchor.Set(ThumbpadFront.bottomAnchor.relative, BottomAnchorPosition + lerpV);
				
					finalMovementDirection.x = directionVector.x;
					finalMovementDirection.y = directionVector.y;
				}
			}
		}


		if(!fingerTouchValid)
		{
			finalMovementDirection = Vector2.zero;
			ThumbpadFront.leftAnchor.Set(ThumbpadFront.leftAnchor.relative, LeftAnchorStartPosition);
			ThumbpadFront.rightAnchor.Set(ThumbpadFront.rightAnchor.relative, RightAnchorStartPosition);
			
			ThumbpadFront.topAnchor.Set(ThumbpadFront.topAnchor.relative, TopAnchorStartPosition);
			ThumbpadFront.bottomAnchor.Set(ThumbpadFront.bottomAnchor.relative, BottomAnchorPosition);
		}

		//Debug.Log(finalMovementDirection.ToString());
		/*
		if (tapTimer > 0) 
		{
			tapTimer -= Time.deltaTime;
		}
		
		if (fingerID > -1 && fingerID >= Input.touchCount) 
		{
			ResetJoystick();
		}
		
		if (enable == true) 
		{
			
			if (Input.touchCount > 0 && gotPosition == false) 
			{
				GetPosition(); 
				GetConstraints();
			}
			
			if (Input.touchCount > 0 && fingerID > -1 && fingerID < Input.touchCount && gotPosition == true) {
				
				foreach (Touch touch in Input.touches) {
					
					if (touch.fingerId == fingerID) {
						
						position = touch.position; 
						//Debug.Log("position1:" + position.ToString());
						position = GetRadius(origin, position,length);
						//Debug.Log("position2:" + position.ToString());

						VJRvector = GetControls(position,origin); 
						//Debug.Log("VJRVECTOR:" + VJRvector.ToString());
						VJRnormals = new Vector2(VJRvector.x/length,VJRvector.y/length);
						
						if (Input.GetTouch(fingerID).position.x > (ThumbpadBack.transform.position.x + ThumbpadBack.width)
						    || Input.GetTouch(fingerID).position.y > (ThumbpadBack.transform.position.y + ThumbpadBack.height)) 
						{
							ResetJoystick();
						}
						//Debug.Log("Joystick Axis:: "+VJRnormals); //<-- Delete this line | (X,Y), from -1.0 to +1.0 | Use this value "VJRnormals" in your scripts.
					}
				}
				

				Debug.Log(VJRnormals.ToString());
				
			}
			
			if (gotPosition == true && Input.touchCount > 0 && fingerID > -1 && fingerID < Input.touchCount) {
				
				if (Input.GetTouch(fingerID).phase == TouchPhase.Ended || Input.GetTouch(fingerID).phase == TouchPhase.Canceled) 
				{
					ResetJoystick();
				}
				
			}
			
			/*if (gotPosition == false && fingerID == -1 && tapTimer <= 0) 
			{
				if (background.color != inactiveColor) 
				{
					background.color = inactiveColor;
				}
			}*/

			//background.pixelInset = new Rect(origin.x-(background.pixelInset.width/2),origin.y-(background.pixelInset.height/2),size/4,size/4);
			//joystick.pixelInset = new Rect(position.x-(joystick.pixelInset.width/2),position.y-(joystick.pixelInset.height/2),size/4,size/4);
		


		 

			//Vector2 jumbPosition = new Vector2((Screen.width/3)/2,(Screen.height/3)/2);;
			//jumbTexture.pixelInset = new Rect(Screen.width-(jumbTexture.pixelInset.width) * 1.3f, (jumbTexture.pixelInset.height/2),size/6,size/6);
			//Debug.Log("background.pixelInset: " + background.pixelInset.ToString());
			///Debug.Log("origin: " + origin.ToString());
			//Debug.Log("joystick.pixelInset:" + joystick.pixelInset.ToString());
			//*

			//Debug.Log(ThumbpadBack.rightAnchor.rect.ToString());
			//Debug.Log(ThumbpadBack.leftAnchor.rect.ToString());
		//}
		/*else if (background.pixelInset.width > 0) 
		{
			background.pixelInset = new Rect(0,0,0,0); 
			joystick.pixelInset = new Rect(0,0,0,0);

			//Debug.Log("pixel inset 0");
		}*/
	

		//Vector3 vectorA = new Vector3(UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.position.x, 0, UIGlobalVariablesScript.Singleton.MainCharacterRef.transform.position.z);
		//Vector3 vectorB = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);


		if(CharacterControllerRef != null)
		{
			CharacterControllerRef.MovementDirection = Camera.main.transform.right * finalMovementDirection.x;//new Vector3(VJRnormals.x, 0, VJRnormals.y);
			CharacterControllerRef.MovementDirection += Vector3.Normalize(new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.transform.forward.z)) * finalMovementDirection.y;
			CharacterControllerRef.MovementDirection.y = 0;

			if(finalMovementDirection != Vector2.zero)
			{
				if(movementSpeed < 0.5f)
				{
					UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimationControllerScript>().IsRunning = false;
					UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimationControllerScript>().IsWalking = true;
				}
				else
				{
					UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimationControllerScript>().IsRunning = true;
					UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimationControllerScript>().IsWalking = false;
				}
				CharacterControllerRef.walkSpeed =  movementSpeed * 150.0f;
				CharacterControllerRef.RotateToLookAtPoint(CharacterControllerRef.transform.position + CharacterControllerRef.MovementDirection * 6);
			}
			else
			{
				UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimationControllerScript>().IsRunning = false;
				UIGlobalVariablesScript.Singleton.MainCharacterRef.GetComponent<AnimationControllerScript>().IsWalking = false;
				CharacterControllerRef.MovementDirection = Vector3.zero;
			}
		}

	}
}
