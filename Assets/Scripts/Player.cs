using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

	private Rigidbody2D rb;
	private const float baseForce = 0.7f, baseForceStabilize = 1.0f;
	private bool moving = false, colliding = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	
	void FixedUpdate() {
		moving = false;
		if (Input.GetKey(KeyCode.A)) {
			rb.AddForce(new Vector2((rb.velocity.x < 0.0f)?-baseForce:-baseForce*2, 0.0f));
			moving = true;
		}
		if (Input.GetKey(KeyCode.D)) {
			rb.AddForce(new Vector2((rb.velocity.x > 0.0f)?baseForce:baseForce*2, 0.0f));
			moving = true;
		}
		if (Input.GetKey(KeyCode.W)) {
			rb.AddForce(new Vector2(0.0f, (rb.velocity.y < 0.0f)?baseForce*2:baseForce));
			moving = true;
		}
		if (Input.GetKey(KeyCode.S)) {
			rb.AddForce(new Vector2(0.0f, (rb.velocity.y > 0.0f)?-baseForce*2:-baseForce));
			moving = true;
		}
		if (!moving)
			Stabilize();
		if (colliding)
			colliding = !colliding;
	}

	private void Stabilize() {
		if (rb.velocity.x < 0.0f )
			rb.AddForce(new Vector2(baseForceStabilize, 0.0f));
		if (rb.velocity.x > 0.0f)
			rb.AddForce(new Vector2(-baseForceStabilize, 0.0f));
		if (rb.velocity.y < 0.0f)
			rb.AddForce(new Vector2(0.0f, baseForceStabilize));
		if (rb.velocity.y > 0.0f)
			rb.AddForce(new Vector2(0.0f, -baseForceStabilize));
	}

	void OnTriggerEnter2D(Collider2D gameObject) {
		if (!colliding) {
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
				rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
			if (min == dyMin || min == dyMax)
				rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
			Debug.Log ("Player: " + pPosition);
			Debug.Log ("Object: " + oPosition);
		}
	}
	
}
