using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {


	//Movement//
	public float PlayerGravity = 10f;
	public float PlayerTurnSpeed = 10f;
	public float PlayerMaxMoveSpeed = 5f;
	public float PlayerAcceleration = 1f;
	public bool IsInAir = false;
	
	private Vector3 PlayerPos;
	private Quaternion PlayerNewRot;

	protected Quaternion PlayerRot;
	protected float PlayerSpeed;
	protected Vector3 PlayerVelDir;
	protected Vector3 PlayerVel;
		




 
	// Use this for initialization
	void Start () 
	{
	
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
		PlayerPos = transform.position;
		PlayerRot = transform.rotation;
		//Check player movement
		if(Mathf.Abs(Input.GetAxis("LeftRight")) > 0 || Mathf.Abs(Input.GetAxis("ForwardBackward")) > 0  )
			PlayerVelDir = new Vector3(Input.GetAxis("LeftRight"),0,Input.GetAxis("ForwardBackward"));

			if(PlayerSpeed != PlayerMaxMoveSpeed)
				PlayerSpeed += PlayerAcceleration;
		//if(Mathf.Abs(Input.GetAxis("Mouse X")) > 0 || Mathf.Abs(Input.GetAxis("Mouse Y")) > 0)
			//PlayerNewRot = new Quaternion(0,Input.GetAxis("Mouse Y"),0,0) * PlayerRot ;


		
	}


	void HandlePlayerMovement()
	{

		PlayerVel = PlayerVelDir * PlayerSpeed;


		//DEBUG
		Debug.DrawLine (PlayerPos, PlayerPos + PlayerVel , Color.red);	
		//Debug.Log("Mouse" + Input.GetAxis("Mouse X") + "   " + Input.GetAxis("Mouse X");



		//Set Player Position
		transform.position = PlayerPos + PlayerVel * Time.deltaTime;
		//transform.rotation = PlayerNewRot;


	}




}

