using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Timer : MonoBehaviour {
	[SerializeField]
	float targetTime=5f;
	private Vector3 particlespawnposition;
	private bool isEmitting=false;
	private Vector3 temppos;
	[SerializeField]
	public GameObject particler;
	//increase the timer
	public /// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		{
			particlespawnposition=this.transform.position;
			temppos=new Vector3(0,3,0)+particlespawnposition;
			particlespawnposition=temppos;
		}
	}
	public void increase()
	{
		targetTime-=Time.deltaTime;
			if((targetTime<=0)&&(isEmitting==false))
		{
			Debug.Log("over");
			Instantiate(particler,particlespawnposition,Quaternion.identity);
			isEmitting=true;			
		}
	}
}
