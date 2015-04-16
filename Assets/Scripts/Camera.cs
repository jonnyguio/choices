using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	private GameObject player, mainCamera;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position;

		position = player.transform.position;

		mainCamera.transform.position = new Vector3(position.x, position.y,  position.z - 10.0f);
	}
}
