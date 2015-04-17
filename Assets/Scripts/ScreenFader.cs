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

	private Color
		alpha;

	private GameObject 
		obj, obj2, obj3;

	void Awake () {
		// Set the texture so that it is the the size of the screen and covers it.
		transform.position = new Vector3(0.0f, 0.0f, 0.0f);
		guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
		alpha = Color.white;
		alpha.a = 0.0f;
		obj = GameObject.Find ("Play");
		obj2 = GameObject.Find ("Quit");
		obj3 = GameObject.Find ("GUI Text");
	}
	
	
	void Update () {
		if (sceneStarting)
			StartScene();
		if (sceneEnding)
			EndScene ();
	}
	
	void FadeToClear () {
		// Lerp the colour of the texture between itself and transparent.
		guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
		alpha.a += fadeSpeed * Time.deltaTime * 0.45f;
		if (obj)
			obj.guiText.color = alpha;
		if (obj2)
			obj2.guiText.color = alpha;
		if (obj3)
			obj3.guiText.color = alpha;
	}

	void FadeToBlack() {
		guiTexture.color = Color.Lerp (guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
		alpha.a -= fadeSpeed * Time.deltaTime * 0.75f;

		if (obj)
			obj.guiText.color = alpha;
		if (obj2)
			obj2.guiText.color = alpha;
		if (obj3)
			obj3.guiText.color = alpha;
	}
	
	void StartScene () {
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(guiTexture.color.a <= 0.05f)
		{

			if (obj)
				obj.guiText.color = Color.white;
			if (obj2)
				obj2.guiText.color = Color.white;
			if (obj3)
				obj3.guiText.color = alpha;
	
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
			if (obj)
				obj.guiText.color = Color.clear;
			if (obj2)
				obj2.guiText.color = Color.clear;
			if (obj3)
				obj3.guiText.color = alpha;

			sceneStarting = true;
			sceneEnding = false;
			Application.LoadLevel (level);
		}
	}

	public void End(int level)
	{
		sceneEnding = true;
		this.level = level;
	}
}
