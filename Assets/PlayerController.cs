using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]

public class PlayerController : MonoBehaviour {


	//Movement//
	public float PlayerTurnSpeed = 10f;
	public float PlayerMaxMoveSpeed = 5f;
	public float PlayerAcceleration = 1f;
	public float PlayerJumpStrenght = 10f;
	public float PlayerFriction = 10f;


	
	private Vector3 PlayerPos;
	private Vector3 PlayerNewRot;


	protected float PlayerSpeed;
	protected bool PlayerIsJumping;
	protected bool PlayerIsInAir;

	protected Vector3 PlayerRot;
	protected Vector3 PlayerVelDir;
	protected Vector3 PlayerVel;
		

	CharacterController CharController;


 
	// Use this for initialization
	void Start () 
	{
		CharController = GetComponent<CharacterController>();
		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		HandleInput();
		HandlePlayerMovement();
		//HandleCamera ();

	}

	
	void HandleInput()
	{
		//Reset Vars
		PlayerVelDir = new Vector3();
		PlayerNewRot = new Vector3();
		PlayerRot = transform.rotation.eulerAngles;
		PlayerPos = transform.position;	

		//Check player movement
		if(Mathf.Abs(Input.GetAxis("LeftRight")) > 0.1 || Mathf.Abs(Input.GetAxis("ForwardBackward")) > 0.1  )
			PlayerVelDir = new Vector3(Input.GetAxis("LeftRight"),0,Input.GetAxis("ForwardBackward"));

				if(PlayerSpeed != PlayerMaxMoveSpeed)
					PlayerSpeed += PlayerAcceleration;

		//Check Mouse Rotation 
		if(Mathf.Abs(Input.GetAxis("Mouse X")) > 0.1 || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0.1)
			PlayerNewRot = new Vector3(Input.GetAxis("Mouse Y"),-Input.GetAxis("Mouse X"),0);

		if(Input.GetButton("Jump") && CharController.isGrounded)		
			PlayerIsJumping = true;



	}


	void HandlePlayerMovement()
	{

		PlayerVel = PlayerVelDir  * PlayerSpeed ;

		if(!CharController.isGrounded)	
			PlayerVel.y += Physics.gravity.y;
			PlayerIsInAir = true;

		if(PlayerIsJumping)		
			PlayerVel.y += PlayerJumpStrenght;
			PlayerIsJumping = false;
			


		//DEBUG
		Debug.DrawLine (PlayerPos, PlayerPos + PlayerVel , Color.red);	
		Debug.Log("     " + PlayerVel.normalized + PlayerVel);



		//Set Player Position
		CharController.Move( PlayerVel * Time.deltaTime);
		//transform.position +=  PlayerVel * Time.deltaTime;
		transform.Rotate(new Vector3(0,-PlayerNewRot.y * PlayerTurnSpeed,0));
		Camera.main.transform.Rotate(new Vector3(-PlayerNewRot.x * PlayerTurnSpeed,0,0));

	


	}




}

