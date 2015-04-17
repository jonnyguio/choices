using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour {

	public bool
		starting;

	// Use this for initialization
	void Start () {
		starting = false;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject play, exit;
		play = GameObject.Find ("Play");
	}

	void OnMouseDown()
	{
		if (!starting) {
			GameObject screenFader;

			screenFader = GameObject.Find("ScreenFader");
			screenFader.GetComponent<ScreenFader>().End (0);
		
			starting = true;
		}
	}
}
