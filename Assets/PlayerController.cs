using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {


	//Movement//

	private Vector3 PlayerPos;
	protected Vector3 PlayerVel;
	protected Vector3 PlayerRot;





	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{	
		HandleInput();
		HandlePlayerMovement();

	}

	
	void HandleInput()
	{
		if(Input.GetKeyDown(KeyCode.W))
			//Go forward
		if(Input.GetKeyDown(KeyCode.A))
			//Go left
		if(Input.GetKeyDown(KeyCode.S))
			//Go backward
		if(Input.GetKeyDown(KeyCode.D));
			//Go right

		
	}


	void HandlePlayerMovement()
	{
		transform.position = PlayerPos + PlayerVel;
		transform.rotation = PlayerRot;

	}




}

