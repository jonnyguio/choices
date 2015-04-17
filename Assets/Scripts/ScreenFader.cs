using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour {

	public float 
		fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.

	private int
		level;

	private bool 
		sceneStarting = true,
		sceneEnding = false;      // Whether or not the scene is still fading in.

	
	void Awake () {
		// Set the texture so that it is the the size of the screen and covers it.
		transform.position = new Vector3(0.0f, 0.0f, 0.0f);
		guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}
	
	
	void Update () {
		if (sceneStarting)
			StartScene();
		if (sceneEnding)
			EndScene ();
	}

	public void End(int level)
	{
		sceneEnding = true;
		this.level = level;
	}
	
	void FadeToClear () {
		// Lerp the colour of the texture between itself and transparent.
		guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	void FadeToBlack() {
		guiTexture.color = Color.Lerp (guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
	}
	
	void StartScene () {
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(guiTexture.color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			guiTexture.color = Color.clear;
			guiTexture.enabled = false;
			
			// The scene is no longer starting.
			sceneStarting = false;
		}
	}

	void EndScene() {

		guiTexture.enabled = true;

		FadeToBlack ();

		if (guiTexture.color.a >= 0.95f) {
			guiTexture.color = Color.black;
			guiTexture.enabled = true;

			sceneStarting = true;
			sceneEnding = false;
			Application.LoadLevel (level);
		}
	}
}
