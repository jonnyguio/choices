using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Message : MonoBehaviour {

	public static List<string> Messages = new List<string> {
		"'The one who never knew how to chose correctly.\n But, why?",
		"He had three children. Two flats, one round.\n Why he didn't noticed the special needs of the round one?",
		"So many regrets, that he's still trying to find a way to change it all.\n But he simply can't. And while he didn't notice that, he continously made mistakes.",
		"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
		"Teste 4",
		"Teste 5",
		"Teste 6",
		"Teste 7",
		"Teste 8",
		"Teste 9",
		"Teste 10",
		"Teste 11",
		"Teste 12",
		"Teste 13",
		"Teste 14",
		"Teste 15"};
	public static string tip = "Press E to read the message.";
	public static bool know = false;	

	private float 
		initTime, endTime, endGame;

	private bool 
		time, started, ending;

	private Color alpha;
	// Use this for initialization
	void Start () {
		started = false; time = false; ending = false;
		alpha = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
		if (know) {
			if (guiText.text != "" && !Player.knowing) {
				if (!time) {
					initTime = Time.time;
					time = true;
				}
				else {
					endTime = Time.time;
					if (guiText.material.color.a > 0 && endTime - initTime > 0.05f * guiText.text.Length)
						alpha.a -= 0.01f * (endTime - 0.05f * guiText.text.Length) - initTime * 0.01f;
					else {
						if (guiText.material.color.a < 0) {
							guiText.text = "";
							time = false;
							if (started) {
								Player.knowing = true;
								started = false;
								alpha.a = 1.0f;
							}
						}
					}
				}
			}
			else
			{
				alpha.a = 1.0f;
				if (Player.started && !started) {
					guiText.text = "Suddenly, everything made sense.";
					started = true;
				}
			}
			guiText.material.color = alpha;
			if (Player.knowing) {
				endGame = Time.time;
				guiText.text = "He was never able to choose.";
				alpha.a -= 0.1f * (endTime - 0.05f * guiText.text.Length) - initTime * 0.01f;
				Debug.Log (endGame - initTime);
				if (endGame - initTime > 20.0f && !ending) {
					GameObject screenFader;
					
					screenFader = GameObject.Find("ScreenFader");
					screenFader.GetComponent<ScreenFader>().End (2);
					ending = true;
				}
			}
			//Debug.Log (endTime - initTime);
		}
	}
}
