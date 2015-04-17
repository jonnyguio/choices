using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Message : MonoBehaviour {

	public static List<string> Messages = new List<string> {
		"'The one who never knew how to choose correctly.'\nYou must escape this maze.\n\nHow will you escape it? The letters shall guide your path.",
		"He had three children. Two flats, one round.\nWhy he didn't notice the special needs of the round one?",
		"So many regrets, that he's still trying to find a way to change it all.\nBut he simply can't. And while he didn't notice that, he just kept on making mistakes.",
		"He found the love of his life at the age of 22. She was perfectly round.\n\nThat shapes.",
		"The first one was the proud of the father. PhD in Physics, from Flatland University.\nSince he was a little square, he was good with numbers.",
		"After 30 years together, she died. Or, at least, disappeared.\n\nHe was never the same.",
		"'Why, why, why? Why i'm trapped here? What's is this all about?'",
		"'I can't remember how i ended up in this place?'\nThese first questions are the most dangerous ones.",
		"Running around in circles, he started to think too much about his life.\n\nDangerous zone.",
		"'DAD, I WANT A CAR!'\n'Sure son, you deserve it!'\nThe other two kids just stared from behind their bedrooms' doors.",
		"Always watches. No eyes.",
		"How could have I trusted Kevin?",
		"'It's just a resistance test. I can do it.'",
		"'Son, just follow your dreams. Pierce the heavens.'",
		"'Honey, I'm home.'",
		"Exhausted, he needed to find a way. He needed to escape of this endless hell.\nWhat was that he wasn't seeing?"};
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
				guiText.text = "He was never able to choose.\n\nNeither escape.";
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
