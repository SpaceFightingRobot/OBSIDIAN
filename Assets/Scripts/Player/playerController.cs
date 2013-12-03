///<summary>
/// PLAYER CONTROLLER
/// PROJECT EMBER
/// Made by: Burning
///</summary>


using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]

public class playerController : MonoBehaviour {

	public float playerSpeed = 10f;
	public float playerTurnSpeed = 5f;
	public float playerFriction = 5f;
	public float playerJumpStrenght = 10f;


	protected CharacterController charController;
	protected Vector3 playerVelocity = Vector3.zero;
	protected Vector3 playerPosition = Vector3.zero;
	protected Vector3 playerNewRot = Vector3.zero;
	protected Vector3 playerRot = Vector3.zero;
	protected Vector3 grapplePos = Vector3.zero;


	protected bool isGrappledLeft = false;
	protected bool isGrappledRight = false;
	protected bool playerIsJumping = false;





	private Vector3 playerForwardVelocity = Vector3.zero;




	void Awake()
	{
		charController = GetComponent<CharacterController>();
	}

	void Start()
	{
		// Lock the cursor
		Screen.lockCursor = true;
	}
	void playerInput()
	{
	
		
		//Check if Mouse Rotation
		if(Mathf.Abs(Input.GetAxis("Mouse X")) > 0 || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0)
		{
			//Adding input to rotation
			playerNewRot = new Vector3(Input.GetAxis("Mouse Y"),-Input.GetAxis("Mouse X"),0);
		}


		//Check if input LeftRiht or ForwardBackard
		if(Mathf.Abs(Input.GetAxis("LeftRight")) > 0 || Mathf.Abs(Input.GetAxis("ForwardBackward")) > 0  )
		{
			//Adding velocity with the input values
			playerVelocity = new Vector3(Input.GetAxis("LeftRight"),0,Input.GetAxis("ForwardBackward"));
		
		} 


		//Check if player is jumping
		if(Input.GetButton("Jump") && charController.isGrounded)
		{
			playerIsJumping = true;
			// playerIsInAir = true;

		}

		//Check if player is grappling
		if(Input.GetButton("GrappleRight"))
		{

			playerGrapple();
			Debug.Log("grapple INput");

		}




	}

	void playerMovement()
	{

	

	
		//Transform local of the velocity to world so we know which direction its going
		playerForwardVelocity = transform.TransformDirection(playerVelocity);

		//Add Velocity + speed
		playerPosition =  playerForwardVelocity * playerSpeed;

		//Add Gravity
		playerPosition.y += Physics.gravity.y;

		//Add Jump
		if(playerIsJumping)
		{
			playerPosition.y = playerJumpStrenght;
			playerIsJumping = false;
		}

		//Add Grapple
		if(isGrappledRight)
		{
			playerPosition -= transform.position - grapplePos;
			Debug.DrawLine(transform.position,grapplePos,Color.red);		
			//NOT RELATIVE TO PLAYER <----------------------------------------

		}
		Debug.Log(grapplePos);




	
		
		//Move Character
		charController.Move(playerPosition * Time.deltaTime);


		//Rotate player
		transform.eulerAngles -= playerNewRot * playerTurnSpeed;

		//Reseting Playerrotation to prevent sliding
		playerNewRot = Vector3.zero;

		//reset velocity 
		playerPosition = Vector3.zero;
		playerVelocity = Vector3.zero;

		
		

	}


	void playerGrapple()
	{
		Vector3 playerRayPos = transform.position;
		Vector3 playerRayForward = transform.TransformDirection(Vector3.forward);
	
		                                                       
		RaycastHit rayHit;
		if(Physics.Raycast(playerRayPos,playerRayForward,out rayHit))
		{
			grapplePos = rayHit.transform.position;
			//NOT RELATIVE TO PLAYER <----------------------------------------

		
		}

		isGrappledRight = true;
	


	}


	void Update()
	{
		playerInput();
		playerMovement();


	}

}

