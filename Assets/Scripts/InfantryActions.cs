﻿using UnityEngine;
using System.Collections;

public class InfantryActions : MonoBehaviour {

	private bool Fight = false;
	private bool dead = false;
	private bool walk = false;
	private string thistag;
	private GameObject enemy;
	private Animator animator;
	private static int rotationSpeed = 5;
	private static float speed = 0.2f;

	// Use this for initialization
	void Start()
	{
		thistag = this.tag;
		enemy = null;
		animator = GetComponent<Animator> ();
	}


	void OnTriggerEnter( Collider other )
	{	
		if (!other.gameObject.CompareTag (thistag)) 
		{	//other.GetType ().ToString ().Equals ("UnityEngine.SphereCollider")
			//find an enemy

			if (!enemy) {
				enemy = other.gameObject;
				MoveTo (other.transform);
			}
			 
							
		}
	}

	void OnTriggerStay( Collider other )
	{	
		if (!other.gameObject.CompareTag (thistag)) 
		{				
				if (enemy) {
					if (other.gameObject.Equals (enemy)) {				
						MoveTo (other.transform);
					}
				} else {
					enemy = other.gameObject;
					MoveTo (other.transform);
				}
		}
	}


	void OnTriggerExit( Collider other )
	{
		if (!other.gameObject.CompareTag (thistag)) 
		{				
				if (other.gameObject.Equals(enemy)) {				
					enemy = null;
					MoveHead ();
				}
		}
	}
		
	void OnCollisionEnter( Collision other ) 
	{	Debug.Log ("enter Collider0");
		if (!other.gameObject.CompareTag (thistag)) {
			Debug.Log ("enter Collider1");
			if (other.gameObject.Equals (enemy)) {
				Debug.Log ("enter Collider2");
				animator.SetBool ("Fight", true);	
				animator.SetBool ("Walk", false);	
			}
		}
	}

	void OnCollisionStay (Collision other)
	{
		if (!other.gameObject.CompareTag (thistag)) {
			if (other.gameObject.Equals (enemy)) {
				animator.SetBool ("Fight", true);	
				animator.SetBool ("Walk", false);	
			}
		}
	}

	void OnCollisionExit (Collision other)
	{
		if (!other.gameObject.CompareTag (thistag)) {
			if (other.gameObject.Equals (enemy)) {
				animator.SetBool ("Fight", false);	
				animator.SetBool ("Walk", true);	
			}
		}
	}

	public  void MoveTo(Transform tf){

		Vector3 direction = tf.transform.position - transform.position;

		direction.y = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

		Vector3 eulerAngles = new  Vector3(0,transform.eulerAngles.y, 0);
		transform.eulerAngles = eulerAngles;

		transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
		//Vector3 forward = transform.TransformDirection(Vector3.forward);
		//		Debug.Log ("forward is: "+forward);
		//		float speedModifier = Vector3.Dot(forward, direction.normalized);
		//		Debug.Log ("speedModifier is: "+speedModifier);
		//		speedModifier = Mathf.Clamp01(speedModifier);
		//		Debug.Log ("speedModifier clamp is: "+speedModifier);
		//		direction = forward * speed * speedModifier;
		//		transform.position = transform.position + direction;
	}



	public void MoveHead()
	{

		Vector3 direction = new Vector3 (0, 0, 1);

		direction.y = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

		Vector3 eulerAngles = new  Vector3(0,transform.eulerAngles.y, 0);
		transform.eulerAngles = eulerAngles;
	}
}
