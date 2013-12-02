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
			//reset rotation and assing new one
			playerNewRot = Vector3.zero;
			playerNewRot = new Vector3(Input.GetAxis("Mouse Y"),-Input.GetAxis("Mouse X"),0);
		}


		//Check if input LeftRiht or ForwardBackard
		if(Mathf.Abs(Input.GetAxis("LeftRight")) > 0 || Mathf.Abs(Input.GetAxis("ForwardBackward")) > 0  )
		{
			//reset velocity and assing new one
			playerPosition = Vector3.zero;
			playerVelocity = Vector3.zero;

			//Assing velocity with the input values
			playerVelocity = new Vector3(Input.GetAxis("LeftRight"),0,Input.GetAxis("ForwardBackward"));
		
		} 


		//Check if player is jumping
		if(Input.GetButton("Jump") && charController.isGrounded)
		{
			playerIsJumping = true;
			// playerIsInAir = true;

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
		
		
		//Move Character
		charController.Move(playerPosition * Time.deltaTime);


		//Rotate player
		transform.eulerAngles -= playerNewRot * playerTurnSpeed;

	}


	void Update()
	{
		playerInput();
		playerMovement();


	}

}

