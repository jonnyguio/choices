using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {
	
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
		if (!starting && name == "Play") {
			GameObject screenFader;
			
			screenFader = GameObject.Find("ScreenFader");
			screenFader.GetComponent<ScreenFader>().End (1);
			
			starting = true;
		}
		else {
			if (!starting && name == "Quit") {
			
				Debug.Log ("Teste");
				Application.Quit();
			}
		}
	}
}
