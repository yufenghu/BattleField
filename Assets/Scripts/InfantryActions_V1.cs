using UnityEngine;
using System.Collections;

public class InfantryActions_V1 : MonoBehaviour {
	
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
			if (other.GetType ().ToString ().Equals ("UnityEngine.SphereCollider")) {
				if (!enemy) {
					enemy = other.gameObject;
					MoveTo (other.transform);
				}
			} 
			//fight
			else if (other.GetType ().ToString ().Equals ("UnityEngine.CapsuleCollider")) {
				//AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo (0);
				//if (stateInfo.shortNameHash == Animator.StringToHash ("Run")) {
				Debug.Log("enter CapsuleCollider");
//				animator.SetBool ("Fight", true);	
//				animator.SetBool ("Walk", false);					
			}					
		}
	}

	void OnTriggerStay( Collider other )
	{	
		if (!other.gameObject.CompareTag (thistag)) 
		{	
			if (other.GetType ().ToString ().Equals ("UnityEngine.SphereCollider")) {
				if (enemy) {
					if (other.gameObject.Equals (enemy)) {				
						MoveTo (other.transform);
					}
				} else {
					enemy = other.gameObject;
					MoveTo (other.transform);
				}
			} else if (other.GetType ().ToString ().Equals ("UnityEngine.CapsuleCollider")) {				
//				animator.SetBool ("Fight", true);	
//				animator.SetBool ("Walk", false);	
			}	
		}
	}


	void OnTriggerExit( Collider other )
	{
		if (!other.gameObject.CompareTag (thistag)) 
		{	
			if (other.GetType ().ToString ().Equals ("UnityEngine.SphereCollider")) {
				if (other.gameObject.Equals(enemy)) {				
					enemy = null;
					MoveHead ();
				}
			}else if (other.GetType ().ToString ().Equals ("UnityEngine.CapsuleCollider")) {
//				animator.SetBool ("Fight", false);	
//				animator.SetBool ("Walk", true);	
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
