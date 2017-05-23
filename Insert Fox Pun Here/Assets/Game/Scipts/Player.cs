using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 150f;
	public Vector2 maxVeloxity = new Vector2(60,100);
	public float jetSpeed = 200f;
	public bool standing;
	public float standingThreshold = 4f;
	public float airSpeedMultiplier = .3f;
	public bool facingLeft = true;

	private Rigidbody2D body2D;
	private SpriteRenderer renderer2D;
	private PlayerController controller;
	private Transform transform;

	private Animator animator;

	// Use this for initialization
	void Start () {
		body2D = GetComponent<Rigidbody2D> ();
		renderer2D = GetComponent<SpriteRenderer> ();
		controller = GetComponent<PlayerController> ();
		animator = GetComponent<Animator> ();
		transform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		var absVelX = Mathf.Abs (body2D.velocity.x);
		var absVelY = Mathf.Abs (body2D.velocity.y);

		var forceX = 0f;
		var forceY = 0f;

		if (absVelY <= standingThreshold) {
			standing = true;
		} else {
			standing = false;
		}

		if (controller.moving.x != 0) {
			if (absVelX < maxVeloxity.x) {
				
				var newSpeed = speed * controller.moving.x;

				forceX = standing ? newSpeed : (newSpeed * airSpeedMultiplier);
				if (forceX > 0 && facingLeft) {
					transform.RotateAround (Vector3.zero, Vector3.up, 180f); 
					facingLeft = false;
				} else if (forceX < 0 && !facingLeft){
					transform.RotateAround (Vector3.zero, Vector3.up, 180f);  
					facingLeft = true;
				}

			}

			animator.SetInteger ("AnimState", 1);

		} else {
			
			animator.SetInteger ("AnimState", 0);

		}

		if (controller.moving.y > 0) {
			if (absVelY < maxVeloxity.y) {
				//body2D.AddForce (new Vector2 (0, 10), ForceMode2D.Impulse);
			}

			animator.SetInteger ("AnimState", 2);

		} 

		body2D.AddForce(new Vector2(forceX, forceY));
	}
}
