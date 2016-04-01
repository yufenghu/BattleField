using UnityEngine;
using System.Collections;

public class CamDirection : MonoBehaviour {

	private GameObject terrain;
	private Terrain terr;
	public float cam_y;
	public float cam_z;
	public float cam_x;
	public float lookpos_x;
	public float lookpos_y;
	public float lookpos_z;

	private Vector3 position;

	void Awake()
	{	terrain = GameObject.Find ("Terrain");
		terr = terrain.GetComponent<Terrain> ();
		cam_x=terr.terrainData.size.x;
		cam_z = terr.terrainData.size.z/2;
		cam_y = 50;
		lookpos_x = terr.terrainData.size.x / 2;
		lookpos_y = 0;
		lookpos_z=terr.terrainData.size.z/2;
	}

	void Update ()
	{
		

		//position = new Vector3 (terr.terrainData.size.x/2,terr.terrainData.size.y-100,terr.terrainData.size.z/2);
//		Debug.Log ("this is :" + position);
//		this.transform.position = position;
//		this.transform.LookAt (new Vector3 (position.x, 0, position.z));
//		this.transform.Rotate (0,0,-90);

		position = new Vector3 (cam_x,cam_y,cam_z);
		this.transform.position = position;
		this.transform.LookAt (new Vector3 (lookpos_x, lookpos_y, lookpos_z));


	}


}
