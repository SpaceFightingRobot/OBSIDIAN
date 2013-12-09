///<summary>
/// PLAYER CONTROLLER
/// OBSIDIAN
/// Made by: Burning
///</summary>


using UnityEngine;
using System.Collections;





[RequireComponent (typeof (CharacterController))]



public class playerController : MonoBehaviour 
{

	public float playerRotSpeed = 2f;
	public float playerSpeed = 10f;
	public float grappleSpeed = 2f;


	protected CharacterController charController;

	protected Vector3 vMoveDireciton = Vector3.zero;
	protected Vector3 vRotDirection = Vector3.zero;
	protected Vector3 vPlayerVelocity = Vector3.zero;
	protected Vector3 vMoveVelocity = Vector3.zero;

	protected bool isGrounded = true;
	protected bool isGrappledRight = false;

	protected Vector3 grappleRightPos = Vector3.zero;
	protected Vector3 grappleVelocity = Vector3.zero;




	void Awake()
	{
		charController = GetComponent<CharacterController>();
		Screen.lockCursor = true;
	}

	void PlayerInput()
	{
		//Check if LeftRight or ForwardBackward Axis are bigger than 0
		if( Mathf.Abs( Input.GetAxis("LeftRight") ) > 0 || Mathf.Abs( Input.GetAxis("ForwardBackward") ) > 0  )
		{
			//Assing vMoveDirection a new vector with the axises
			vMoveDireciton = new Vector3(Input.GetAxis("LeftRight"),0,Input.GetAxis("ForwardBackward"));
	
		}
		//Check if Mouse X or Mouse Y Axis are bigger than 0
		if( Mathf.Abs( Input.GetAxis("Mouse X") ) > 0 || Mathf.Abs( Input.GetAxis("Mouse Y") ) > 0  )
		{
			//Assing vRotDirection a new vector with the axises
			vRotDirection = new Vector3( Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"),0 );

		}

		if(Input.GetButton("Jump"))
		{
			SetVelocity(new Vector3(0,50,0));

		}

		if(Input.GetButton("GrappleRight"))
		{
			PlayerGrapple();
			
		}



	}




	void SetVelocity(Vector3 velocity)
	{
		vPlayerVelocity = velocity;
	}



	void PlayerGrapple()
	{
		// IMPLEMENT SECOND GRAPPLE <-----------------

		//Raycast variables
		Vector3 playerRayPos = transform.position;
		Vector3 playerRayForward = transform.TransformDirection(Vector3.forward);

		
		//Raicast players forward
		RaycastHit rayHit;
		if(Physics.Raycast(playerRayPos,playerRayForward,out rayHit))
		{	
			//Set grapple hook position
			grappleRightPos = rayHit.point;		
		}
				
		isGrappledRight = true;		
		isGrounded = false;


	}


	void PlayerUpdateGrapplePos()
	{
		//figure out velocity direction needed to get to grapplePos 
		Vector3 grappleVelDir;

		//Get Distance from grapple to player
		float distance =  Vector3.Distance(grappleRightPos, transform.position);

		//normalize the direction to the grapple
		grappleVelDir = Vector3.Normalize( grappleRightPos - transform.position );
		
		//Add grapplespeed to that velocity
		grappleVelocity = grappleVelDir * grappleSpeed * distance;
		
		//Set velocity to player
		SetVelocity(grappleVelocity);
	}





	void FixedUpdate()
	{
		PlayerInput();

		if(isGrounded)
		{
			//make it vMoveDireciton relative to player direction
			vMoveVelocity = transform.TransformDirection(vMoveDireciton);

			//Add speed
			vMoveVelocity *= playerSpeed;

			//Reset Y because relative also gives us the Y position
			vMoveVelocity.y = 0;

			//Add external forces(external velocity)
			vMoveVelocity += vPlayerVelocity * Time.deltaTime;	

			//Reset to prevent sliding <-------------------- **FIND A BETTER WAY**
			vMoveDireciton = Vector3.zero;

			//Add Gravity
			vMoveVelocity.y += Physics.gravity.y;

		}
		else if(isGrappledRight)
		{
			
			//Check for grapple velocity again
			PlayerUpdateGrapplePos();

			//Add velocity of the grapple
			vMoveVelocity += vPlayerVelocity * Time.deltaTime;

			//Apply Gravity
			vMoveVelocity.y += Physics.gravity.y;



		}


		////DEBUG
		Debug.DrawLine(transform.position,transform.position + vPlayerVelocity,Color.green);
		Debug.DrawLine(transform.position,grappleRightPos,Color.red);
		Debug.Log(grappleVelocity);


		//Move to velocity
		charController.Move(vMoveVelocity * Time.deltaTime);

		//Rotate player
		transform.eulerAngles -= vRotDirection * playerRotSpeed * Time.deltaTime;


		//Reset to prevent sliding <------------------------ **FIND A BETTER WAY**
		vRotDirection *= 0.7f;



	}
	
	

	
	

}

