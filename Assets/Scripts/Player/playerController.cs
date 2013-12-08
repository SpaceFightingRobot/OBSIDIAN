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
	public float grappleSpeed = 10f;


	protected CharacterController charController;
	protected Vector3 playerVelocity = Vector3.zero;
	protected Vector3 playerPosition = Vector3.zero;
	protected Vector3 playerNewPosition = Vector3.zero;

	protected Vector3 playerNewRot = Vector3.zero;
	protected Vector3 playerRot = Vector3.zero;


	protected Vector3 grappleRightPos = Vector3.zero;
	protected Vector3 grappleLeftPos = Vector3.zero;
	protected Vector3 grappleVelocity = Vector3.zero;

	protected bool isGrappled = false;
	protected bool playerIsJumping = false;
	protected bool isGrounded = false;
	protected bool isInAir = false;








	void Awake()
	{
		charController = GetComponent<CharacterController>();
	}

	void Start()
	{
		Screen.lockCursor = true;
	}

	void PlayerInput()
	{
	
		
		//Check if Mouse Rotation
		if(Mathf.Abs(Input.GetAxis("Mouse X")) > 0 || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0)
		{			//Adding input to rotation
			SetRotVelocity( new Vector3(Input.GetAxis("Mouse Y") , -Input.GetAxis("Mouse X"), 0) );
		}


		//Check if input LeftRiht or ForwardBackard
		if(Mathf.Abs(Input.GetAxis("LeftRight")) > 0 || Mathf.Abs(Input.GetAxis("ForwardBackward")) > 0  )
		{			//Adding velocity with the input values
			SetVelocity( new Vector3(Input.GetAxis("LeftRight" ), 0 , Input.GetAxis("ForwardBackward") ));		
		} 


		//Check if player is jumping
		if(Input.GetButton("Jump") && isGrounded)
		{	
			PlayerJump();
			// playerIsInAir = true;
		}

		//Check if player is grappling
		if(Input.GetButton("GrappleRight"))
		{
			PlayerGrappleRight();
			isGrappled = !isGrappled;

		}
	}

	void PlayerMovement()
	{
		isGrounded = charController.isGrounded;


		Vector3 playerMoveVelocity = Vector3.zero;
	

		if(isGrounded && !isGrappled)
		{
			Vector3 playerForwardDir = Vector3.zero;
			playerForwardDir = transform.TransformDirection(playerVelocity);
			playerMoveVelocity = playerForwardDir  * playerSpeed * Time.deltaTime;

		}
		else
		{		
			playerMoveVelocity = playerVelocity;
			Debug.Log("GRAPPLED");
	
		}




		playerMoveVelocity.y += Physics.gravity.y;


		playerNewPosition = playerMoveVelocity * playerSpeed;	
		
		//Move Character
		charController.Move(playerNewPosition * Time.deltaTime);


		//Rotate player
		transform.eulerAngles -= playerNewRot * playerTurnSpeed;

		//Reseting Playerrotation to prevent sliding
		playerNewRot = Vector3.zero;

		//reset velocity 
		playerPosition = transform.localPosition;



		//add friction
		if(playerVelocity != Vector3.zero && isGrounded)
		{
			playerVelocity /= playerFriction;
		}

	}


	void SetVelocity(Vector3 velocity)
	{
		playerVelocity = velocity ;
	}


	void SetRotVelocity(Vector3 velocity)
	{
		playerNewRot = velocity;
	}




	void PlayerGrappleRight()
	{
		Vector3 playerRayPos = transform.position;
		Vector3 playerRayForward = transform.TransformDirection(Vector3.forward);
	
		//Raycast for grappling position                                                    
		RaycastHit rayHit;
		if(Physics.Raycast(playerRayPos,playerRayForward,out rayHit))
		{
			//Set grapple hook position
			grappleRightPos = rayHit.point;		
		}

		//figure out velocity direction needed to get to grapplePos 
		Vector3 grappleVelDir;
		grappleVelDir = Vector3.Normalize( grappleRightPos - playerPosition );

		//Add grapplespeed to that velocity
		grappleVelocity = grappleVelDir * grappleSpeed;

		//Set velocity to player
		SetVelocity(grappleVelocity);

	}

	void PlayerJump()
	{
		Vector3 playerJumpVector = new Vector3(0,playerJumpStrenght,0);
		SetVelocity(playerJumpVector);
		isInAir = true;
	}

	void Update()
	{
		PlayerInput();
		PlayerMovement();


	}

	void OnDrawGizmosSelected() 
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(grappleRightPos,new Vector3(1,1,1));
	}

}

