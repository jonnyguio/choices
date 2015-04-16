using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	private Rigidbody2D 
		rb;

	private GameObject
		gui;

	private Message
		message;

	public const float 
		baseSpeed = 5.0f, 
		baseSpeedStabilize = 0.0525f,
		baseSpeedStabilize2 = 0.023f;

	public float 
		x, y, velx, vely;

	public Vector3
		disEnt, disExi;

	public bool 
		moving = false, 
		colliding = false, 
		knowing = false, 
		canMoveLeft = true, 
		canMoveRight = true, 
		canMoveUp = true,
		canMoveDown = true,
		destroyIt = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		gui = GameObject.Find("GUI Text");
	}
	
	// Update is called once per frame
	
	void Update() {
		moving = false;
		x = Input.GetAxis ("Horizontal");
		y = Input.GetAxis ("Vertical");
		velx = rb.velocity.x;
		vely = rb.velocity.y;
		if (x >= 0 && canMoveRight) {
			//rb.AddForce(new Vector2((rb.velocity.x < 0.0f)?-baseForce:-baseForce*2, 0.0f));
			rb.velocity = new Vector2(baseSpeed * x, rb.velocity.y);
			moving = true;
		}
		if (x <= 0 && canMoveLeft) {
			//rb.AddForce(new Vector2((rb.velocity.x > 0.0f)?baseForce:baseForce*2, 0.0f));
			rb.velocity = new Vector2(baseSpeed * x, rb.velocity.y);
			moving = true;
		}
		if (y >= 0 && canMoveUp) {
			//rb.AddForce(new Vector2(0.0f, (rb.velocity.y < 0.0f)?baseForce*2:baseForce));
			rb.velocity = new Vector2(rb.velocity.x, baseSpeed * y);
			moving = true;
		}
		if (y <= 0 && canMoveDown) {
			//rb.AddForce(new Vector2(0.0f, (rb.velocity.y < 0.0f)?baseForce*2:baseForce));
			rb.velocity = new Vector2(rb.velocity.x, baseSpeed * y);
			moving = true;
		}
		if (colliding)
			colliding = !colliding;
	}
	void OnTriggerEnter2D(Collider2D gameObject) {

		if (!colliding && !knowing && gameObject.tag == "Wall") {
			colliding = true;
			Bounds pPosition, oPosition;
			float dxMin, dyMin, dxMax, dyMax, min;

			pPosition = rb.collider2D.bounds;
			oPosition = gameObject.collider2D.bounds;
			dxMin = Math.Abs((Math.Abs(pPosition.center.x) - Math.Abs(pPosition.extents.x)) - (Math.Abs(oPosition.center.x) + Math.Abs(oPosition.extents.x)));
			dxMax = Math.Abs((Math.Abs(pPosition.center.x) + Math.Abs(pPosition.extents.x)) - (Math.Abs(oPosition.center.x) - Math.Abs(oPosition.extents.x)));
			dyMin = Math.Abs((Math.Abs(pPosition.center.y) - Math.Abs(pPosition.extents.y)) - (Math.Abs(oPosition.center.y) + Math.Abs(oPosition.extents.y)));
			dyMax = Math.Abs((Math.Abs(pPosition.center.y) + Math.Abs(pPosition.extents.y)) - (Math.Abs(oPosition.center.y) - Math.Abs(oPosition.extents.y)));

			min = Math.Min (dxMin, Math.Min (dxMax, Math.Min (dyMin, dyMax)));
			if (min == dxMin || min == dxMax)
			{
				rb.velocity = new Vector2(0.0f, rb.velocity.y);
			}
			if (min == dyMin || min == dyMax)
			{
				rb.velocity = new Vector2(rb.velocity.x, 0.0f);
			}
			if ((min == dxMin || min == dxMax) && (x >= 0)) 
				canMoveRight = false;
			if ((min == dxMin || min == dxMax) && (x <= 0)) 
				canMoveLeft = false;
			if ((min == dyMin || min == dyMax) && (y >= 0)) 
				canMoveUp = false;
			if ((min == dyMin || min == dyMax) && (y <= 0)) 
				canMoveDown = false;
		}

		if (gameObject.tag == "Letter" && !Message.know) {

			gui.guiText.text = Message.tip;

		}
	}

	void OnTriggerStay2D(Collider2D gameObject){
		Debug.Log ("Teste");
		if (gameObject.tag == "Wall") {
			Bounds pPosition, oPosition;
			float dxMin, dyMin, dxMax, dyMax, min;
			
			pPosition = rb.collider2D.bounds;
			oPosition = gameObject.collider2D.bounds;
			dxMin = Math.Abs((Math.Abs(pPosition.center.x) - Math.Abs(pPosition.extents.x)) - (Math.Abs(oPosition.center.x) + Math.Abs(oPosition.extents.x)));
			dxMax = Math.Abs((Math.Abs(pPosition.center.x) + Math.Abs(pPosition.extents.x)) - (Math.Abs(oPosition.center.x) - Math.Abs(oPosition.extents.x)));
			dyMin = Math.Abs((Math.Abs(pPosition.center.y) - Math.Abs(pPosition.extents.y)) - (Math.Abs(oPosition.center.y) + Math.Abs(oPosition.extents.y)));
			dyMax = Math.Abs((Math.Abs(pPosition.center.y) + Math.Abs(pPosition.extents.y)) - (Math.Abs(oPosition.center.y) - Math.Abs(oPosition.extents.y)));
			
			min = Math.Min (dxMin, Math.Min (dxMax, Math.Min (dyMin, dyMax)));
			/*if ((min == dxMin || min == dxMax) && (x >= 0)) 
				canMoveRight = false;
			if ((min == dxMin || min == dxMax) && (x <= 0)) 
				canMoveLeft = false;
			if ((min == dyMin || min == dyMax) && (y >= 0)) 
				canMoveUp = false;
			if ((min == dyMin || min == dyMax) && (y <= 0)) 
				canMoveDown = false;*/	
		}
		if (gameObject.tag == "Letter") {
			
			Debug.Log (Input.GetKeyDown(KeyCode.E));
			if (Input.GetKeyDown (KeyCode.E)) {
				if (!Message.know) {
					gui.guiText.text = Message.Messages[0];
					Message.know = true;
				}
				else {
					int index = UnityEngine.Random.Range(1,Message.Messages.Count);
					gui.guiText.text = Message.Messages[index];
					Message.Messages.RemoveAt (index);	
				}
				Destroy (gameObject.gameObject);
			}
		}
	}

	void OnTriggerExit2D(Collider2D gameObject) {
		if (gameObject.tag == "Wall") {
			if (gameObject.transform.eulerAngles.z != 0.0f) {
				canMoveLeft = true;
				canMoveRight = true;
			}
			else {
				canMoveUp = true;
				canMoveDown = true; 
			}
		}
		if (gameObject.tag == "Letter") {
			if (!Message.know)
				gui.guiText.text = "";
		}
	} 


	public void changeKnowledge()
	{
		knowing = true;
	}
}
