using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	private GameObject mainCamera;

	public float smooth = 1.5f, smoothPerspective = 0.8f;         // The relative speed at which the camera will catch up.

	private Transform player;           // Reference to the player's transform.
	private Vector3 relCameraPos;       // The relative position of the camera from the player.
	private float relCameraPosMag;      // The distance of the camera from the player.
	private Vector3 newPos;             // The position the camera is trying to reach.
	private bool change;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		relCameraPos = transform.position - player.position;
		relCameraPosMag = relCameraPos.magnitude - 0.5f; 
		//ChangePerspective();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		/*
		// OLD CODE TO CHANGE CAMERA POSITION - Too stiff
		Vector3 position;

		position = player.transform.position;

		mainCamera.transform.position = new Vector3(position.x, position.y,  position.z - 10.0f);*/


		// SMOOTH CODE TO CHANGE CAMERA POSITION
		// The standard position of the camera is the relative position of the camera from the player.

		Vector3 standardPos = player.position + relCameraPos;
		
		// The abovePos is directly above the player at the same distance as the standard position.
		Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;
		
		// An array of 5 points to check if the camera can see the player.
		Vector3[] checkPoints = new Vector3[5];
		
		// The first is the standard position of the camera.
		checkPoints[0] = standardPos;
		
		// The next three are 25%, 50% and 75% of the distance between the standard position and abovePos.
		checkPoints[1] = Vector3.Lerp(standardPos, abovePos, 0.25f);
		checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 0.5f);
		checkPoints[3] = Vector3.Lerp(standardPos, abovePos, 0.75f);
		
		// The last is the abovePos.
		checkPoints[4] = abovePos;
		
		// Run through the check points...
		for(int i = 0; i < checkPoints.Length; i++)
		{
			if(ViewingPosCheck(checkPoints[i]))
				break;
		}
		
		// Lerp the camera's position between it's current position and it's new position.
		transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);


		// Make sure the camera is looking at the player.
		//SmoothLookAt();

		if (Player.knowing)
			ChangePerspective();

	}

	bool ViewingPosCheck (Vector3 checkPos)
	{
		RaycastHit hit;
		
		// If a raycast from the check position to the player hits something...
		if(Physics.Raycast(checkPos, player.position - checkPos, out hit, relCameraPosMag))
			// ... if it is not the player...
			if(hit.transform != player)
				// This position isn't appropriate.
				return false;
		
		// If we haven't hit anything or we've hit the player, this is an appropriate position.
		newPos = checkPos;
		return true;
	}
	
	
	void SmoothLookAt ()
	{
		// Create a vector from the camera towards the player.
		Vector3 relPlayerPosition = player.position - transform.position;
		
		// Create a rotation based on the relative position of the player being the forward vector.
		Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);
		
		// Lerp the camera's rotation between it's current rotation and the rotation that looks at the player.
		transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
	}

	private void ChangePerspective() {
		camera.orthographic = false;
		
		camera.transform.position = Vector3.Lerp (transform.position, new Vector3(player.position.x - 30.0f, player.position.y, player.position.z),  smoothPerspective * Time.deltaTime);

		camera.transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler(new Vector3(0.0f, 70.0f, 270.0f)), smoothPerspective * Time.deltaTime);

	}
}
	