using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

	public float CameraSmooth = 10f;

	private Transform player;


	void Awake() {
		//player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	

	void Update () {
		//Debug.Log (Player.position);
	}
}
