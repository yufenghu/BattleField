using UnityEngine;
using System.Collections;

public class InfantryActions : MonoBehaviour {

	private bool Fight = false;
	private bool dead = false;
	private bool walk = false;
	private string thistag;
	private GameObject enemy;
	private Animator animator;
	private Animation animation;
	private static int rotationSpeed = 5;
	private bool BeginFight = false;
	private Infantry infantry;

	// Use this for initialization
	void Start()
	{
		thistag = this.tag;
		enemy = null;
		animator = GetComponent<Animator> ();
		infantry = new Infantry ();
	
	}

	void Update()
	{
		MyInvoke (infantry.Af);
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
	{	
		if (!other.gameObject.CompareTag (thistag)) {
			if (other.gameObject.Equals (enemy)) {
				BeginFight = true;
			}
		}
	}

	void OnCollisionStay (Collision other)
	{
		if (!other.gameObject.CompareTag (thistag)) {
			if (other.gameObject.Equals (enemy)) {
				BeginFight = true;
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

	public void Attack()
	{	
		
		//Vector3 oldposition = this.transform.position;
		animator.SetBool ("Fight", true);	
		animator.SetBool ("Walk", false);
		animator.Play ("Fight");

		//this.transform.position = oldposition;
		if (enemy) {
			object[] message = new object[2];
			message [0] = enemy;  
			message [1] = infantry.A;  
			this.transform.parent.BroadcastMessage("OnDecreaseBlood", message);
		}
		else
			Debug.LogError ("No enenmy");
	}

	public void OnDecreaseBlood(object[] obj)
	{	
		GameObject enemy=(GameObject)obj[0];
		//Debug.Log ("Blood: " + infantry.Blood);
		GameObject my = this.gameObject;
		Debug.Log (my);
		Debug.Log (enemy);
		Debug.Log (my.Equals(enemy));


		if (my.Equals (enemy)) {
			infantry.Blood = infantry.Blood - (int)obj[1];
			//Debug.Log ("Blood: " + infantry.Blood);
			if(infantry.Blood<=0)
				Destroy (this);
			
		}

	}

	public void MyInvoke(float time)
	{
		if (BeginFight && !IsInvoking ("Attack"))
			InvokeRepeating ("Attack", -time, time);
	}

}
