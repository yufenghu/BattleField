using UnityEngine;
using System.Collections;

public class Infantry {
	private int blood=500;
	private int a=80;//攻击力
	private float af=1f;//攻击间隔

	public int Blood
	{ 
		get{return blood;} 
		set{blood = value;} 
	}

	public int A
	{ 
		get{return a;} 
		set{a = value;} 
	}

	public float Af
	{ 
		get{return af;} 
		set{af = value;} 
	}

	public Infantry()
	{
	}


}
