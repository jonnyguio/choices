using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

	private Rigidbody2D 
		rb;

	public const float 
		baseSpeed = 3.0f, 
		baseSpeedStabilize = 0.0525f,
		baseSpeedStabilize2 = 0.023f;

	public float x, y;

	public bool 
		moving = false, 
		colliding = false, 
		knowing = false, 
		canMoveXLeft = true, 
		canMoveXRight = true, 
		canMoveYUp = true,
		canMoveYDown = true;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	
	void FixedUpdate() {
		moving = false;
		x = Input.GetAxis ("Horizontal");
		y = Input.GetAxis ("Vertical");
		if (x >= 0 && canMoveXLeft) {
			//rb.AddForce(new Vector2((rb.velocity.x < 0.0f)?-baseForce:-baseForce*2, 0.0f));
			rb.velocity = new Vector2(baseSpeed * x, rb.velocity.y);
			moving = true;
		}
		if (x < 0 && canMoveXRight) {
			//rb.AddForce(new Vector2((rb.velocity.x > 0.0f)?baseForce:baseForce*2, 0.0f));
			rb.velocity = new Vector2(baseSpeed * x, rb.velocity.y);
			moving = true;
		}
		if (y >= 0 && canMoveYUp) {
			//rb.AddForce(new Vector2(0.0f, (rb.velocity.y < 0.0f)?baseForce*2:baseForce));
			rb.velocity = new Vector2(rb.velocity.x, baseSpeed * y);
			moving = true;
		}
		if (y < 0 && canMoveYDown) {
			//rb.AddForce(new Vector2(0.0f, (rb.velocity.y > 0.0f)?-baseForce*2:-baseForce));
			rb.velocity = new Vector2(rb.velocity.x, baseSpeed * y);
			moving = true;
		}
		if (colliding)
			colliding = !colliding;
	}

	/*private void Stabilize() {
		if (rb.velocity.x < 0.0f )
			//rb.AddForce(new Vector2(baseSpeedStabilize, 0.0f));
			rb.velocity = new Vector2((rb.velocity.x < -baseSpeed) ? rb.velocity.x + baseSpeedStabilize : rb.velocity.x + baseSpeedStabilize2, rb.velocity.y);
		if (rb.velocity.x > 0.0f)
			//rb.AddForce(new Vector2(-baseSpeedStabilize, 0.0f));
			rb.velocity = new Vector2((rb.velocity.x > baseSpeed) ? rb.velocity.x - baseSpeedStabilize : rb.velocity.x - baseSpeedStabilize2, rb.velocity.y);
		if (rb.velocity.y < 0.0f)
			//rb.AddForce(new Vector2(0.0f, baseSpeedStabilize));
			rb.velocity = new Vector2(rb.velocity.x, (rb.velocity.y < -baseSpeed) ? rb.velocity.y + baseSpeedStabilize : rb.velocity.y + baseSpeedStabilize2);
		if (rb.velocity.y > 0.0f)
			//rb.AddForce(new Vector2(0.0f, -baseSpeedStabilize));
			rb.velocity = new Vector2(rb.velocity.x, (rb.velocity.y > baseSpeed) ? rb.velocity.y - baseSpeedStabilize : rb.velocity.y - baseSpeedStabilize2);
	}*/

	void OnTriggerEnter2D(Collider2D gameObject) {
		if (!colliding && !knowing) {
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
			Debug.Log (min + " - " + dxMin + " - " + dxMax + " - " + dyMin + " - " + dyMax);
			if (min == dxMin || min == dxMax)
				rb.velocity = new Vector2(0.0f, rb.velocity.y);
			if (min == dyMin || min == dyMax)
				rb.velocity = new Vector2(rb.velocity.x, 0.0f);
			Debug.Log ("Player: " + pPosition);
			Debug.Log ("Object: " + oPosition);
		}
	}

	void OnTriggerStay2D(Collider2D gameObject) {
		Bounds pPosition, oPosition;
		float dxMin, dyMin, dxMax, dyMax, min;
		
		pPosition = rb.collider2D.bounds;
		oPosition = gameObject.collider2D.bounds;
		dxMin = Math.Abs((Math.Abs(pPosition.center.x) - Math.Abs(pPosition.extents.x)) - (Math.Abs(oPosition.center.x) + Math.Abs(oPosition.extents.x)));
		dxMax = Math.Abs((Math.Abs(pPosition.center.x) + Math.Abs(pPosition.extents.x)) - (Math.Abs(oPosition.center.x) - Math.Abs(oPosition.extents.x)));
		dyMin = Math.Abs((Math.Abs(pPosition.center.y) - Math.Abs(pPosition.extents.y)) - (Math.Abs(oPosition.center.y) + Math.Abs(oPosition.extents.y)));
		dyMax = Math.Abs((Math.Abs(pPosition.center.y) + Math.Abs(pPosition.extents.y)) - (Math.Abs(oPosition.center.y) - Math.Abs(oPosition.extents.y)));
		
		min = Math.Min (dxMin, Math.Min (dxMax, Math.Min (dyMin, dyMax)));
		if (min == dxMin)
			canMoveXLeft = false;
		if (min == dxMax)
			canMoveXRight = false;
		if (min == dyMin)
			canMoveYUp = false;
		if (min == dyMax)
			canMoveYDown = false;
	}

	void OnTriggerExit2D(Collider2D other){
		canMoveXRight = true;
		canMoveXLeft = true;
		canMoveYUp = true;
		canMoveYDown = true;
	} 

	public void changeKnowledge()
	{
		knowing = true;
	}
}
