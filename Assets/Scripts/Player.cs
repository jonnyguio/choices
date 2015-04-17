using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public static bool
		knowing, started;

	private Rigidbody2D 
		rb;

	private GameObject
		gui, letter;

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
		canMoveLeft = true, 
		canMoveRight = true, 
		canMoveUp = true,
		canMoveDown = true,
		destroyIt = false,
		read = false;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		gui = GameObject.Find("GUI Text");
		knowing = false;
		started = false;
	}
	
	void Update() {

		moving = false;
		x = Input.GetAxis ("Horizontal");
		y = Input.GetAxis ("Vertical");
		velx = rb.velocity.x;
		vely = rb.velocity.y;
	
		if (x >= 0 && canMoveRight) {
			//rb.AddForce(new Vector2((rb.velocity.x < 0.0f)?-baseForce:-baseForce*2, 0.0f));
			rb.velocity = (Player.knowing) ? new Vector2(rb.velocity.x, baseSpeed * -x) : new Vector2(baseSpeed * x, rb.velocity.y);
			moving = true;
		}
		if (x <= 0 && canMoveLeft) {
			//rb.AddForce(new Vector2((rb.velocity.x > 0.0f)?baseForce:baseForce*2, 0.0f));
			rb.velocity =  (Player.knowing) ? new Vector2(rb.velocity.x, baseSpeed * -x) : new Vector2(baseSpeed * x, rb.velocity.y);
			moving = true;
		}
		if (y >= 0 && canMoveUp) {
			//rb.AddForce(new Vector2(0.0f, (rb.velocity.y < 0.0f)?baseForce*2:baseForce));
			rb.velocity =  (Player.knowing) ? new Vector2(baseSpeed * y, rb.velocity.y) : new Vector2(rb.velocity.x, baseSpeed * y);
			moving = true;
		}
		if (y <= 0 && canMoveDown) {
			//rb.AddForce(new Vector2(0.0f, (rb.velocity.y < 0.0f)?baseForce*2:baseForce));
			rb.velocity =  (Player.knowing) ? new Vector2(baseSpeed * y, rb.velocity.y) : new Vector2(rb.velocity.x, baseSpeed * y);
			moving = true;
		}

		if (Input.GetKeyDown (KeyCode.E) && read) {
			if (!Message.know) {
				gui.guiText.text = Message.Messages[0];
				Message.know = true;
			}
			else {
				int index = (Message.Messages.Count > 3) ? UnityEngine.Random.Range(1,Message.Messages.Count-2) : 1;
				gui.guiText.text = Message.Messages[index];
				Message.Messages.RemoveAt (index);
				if (Message.Messages.Count == 1) {
					started = true;
				}
			}
			Destroy (letter);
			read = false;
		}

		if (!colliding) 
		{
			canMoveUp = true;
			canMoveDown = true;
			canMoveLeft = true;
			canMoveRight = true;
		}
	}

	void OnTriggerEnter2D(Collider2D gameObject) {

		if (!Player.knowing) {
			if (gameObject.tag == "Wall") {

				colliding = true;
				Bounds pPosition, oPosition;
				float dxLeft, dyDown, dxRight, dyUp, min;

				pPosition = rb.collider2D.bounds;
				oPosition = gameObject.collider2D.bounds;

				dxLeft = Math.Abs((pPosition.center.x - pPosition.extents.x) - (oPosition.center.x + oPosition.extents.x));
				dxRight = Math.Abs((pPosition.center.x + pPosition.extents.x) - (oPosition.center.x - oPosition.extents.x));
				dyDown = Math.Abs((pPosition.center.y - pPosition.extents.y) - (oPosition.center.y + oPosition.extents.y));
				dyUp = Math.Abs((pPosition.center.y + pPosition.extents.y) - (oPosition.center.y - oPosition.extents.y));

				min = Math.Min (dxLeft, Math.Min (dxRight, Math.Min (dyDown, dyUp)));
				if (min == dxLeft || min == dxRight)
				{
					rb.velocity = new Vector2(0.0f, rb.velocity.y);
				}
				if (min == dyDown || min == dyUp)
				{
					rb.velocity = new Vector2(rb.velocity.x, 0.0f);
				}
				if ((min == dxRight) && (x > 0)) 
					canMoveRight = false;
				if ((min == dxLeft) && (x < 0)) 
					canMoveLeft = false;
				if ((min == dyUp) && (y > 0)) 
					canMoveUp = false;
				if ((min == dyDown) && (y < 0)) 
					canMoveDown = false;
			}

			if (gameObject.tag == "Letter") {
				if (!Message.know)
					gui.guiText.text = Message.tip;
				letter = gameObject.gameObject;
				read = true;
			}
		}
	}

	void OnTriggerStay2D(Collider2D gameObject){
		colliding = true;
		if (!Player.knowing) {
			if (gameObject.tag == "Wall") {
				Bounds pPosition, oPosition;
				float dxLeft, dyDown, dxRight, dyUp, min;

				pPosition = rb.collider2D.bounds;
				oPosition = gameObject.collider2D.bounds;
				dxLeft = Math.Abs((pPosition.center.x - pPosition.extents.x ) - (oPosition.center.x + oPosition.extents.x));
				dxRight = Math.Abs((pPosition.center.x + pPosition.extents.x ) - (oPosition.center.x - oPosition.extents.x));
				dyDown = Math.Abs((pPosition.center.y - pPosition.extents.y ) - (oPosition.center.y + oPosition.extents.y));
				dyUp = Math.Abs((pPosition.center.y + pPosition.extents.y ) - (oPosition.center.y - oPosition.extents.y));

				min = Math.Min (dxLeft, Math.Min (dxRight, Math.Min (dyDown, dyUp)));
				if (min == dxLeft || min == dxRight)
				{
					rb.velocity = new Vector2(0.0f, rb.velocity.y);
				}
				if (min == dyDown || min == dyUp)
				{
					rb.velocity = new Vector2(rb.velocity.x, 0.0f);
				}
				if ((min == dxRight) && (x > 0)) 
				{
					canMoveRight = false;
					return;
				}
				if ((min == dxLeft) && (x < 0)) 
				{
					canMoveLeft = false;
					return;
				}
				if ((min == dyUp) && (y > 0))
				{
					canMoveUp = false;
					return;
				}
				if ((min == dyDown) && (y < 0)) 
				{
					canMoveDown = false;
					return;
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D gameObject) {
		if (!Player.knowing) {
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
			colliding = !colliding;
		}
	} 


	public void changeKnowledge()
	{
		knowing = true;
	}
}
