using UnityEngine;
using System.Collections;

public class Compass : MonoBehaviour {

	private GameObject
		player;

	private GameObject[]
		letters;

	private float
		smooth = 1.5f;

	// Use this for initialization
	void Start () {

		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		float nDistance, distance;
		GameObject closer;
		
		letters = GameObject.FindGameObjectsWithTag("Letter");

		transform.position = player.transform.position + new Vector3(0.0f, 6.8f, 0.0f);
		distance = Vector3.Distance(letters[0].transform.position, player.transform.position);
		closer = null;
		foreach (GameObject ele in letters) {
			nDistance = Vector3.Distance(ele.transform.position, player.transform.position);
			if (nDistance < distance) {
				distance = nDistance;
				closer = ele;
			}
		}

		if (closer) {
			Vector3 letterDistance = closer.transform.position - player.transform.position;
			
			float angle = Vector3.Angle(letterDistance, player.transform.up);

			Debug.Log (letterDistance);

			if (letterDistance.x > 0.0f)
				angle = -angle;

			transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
		}
	}
}
