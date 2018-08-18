using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Timer : MonoBehaviour {
	[SerializeField]
	float targetTime=5f; // 5 seconds of viewing total will make paricles come
	private Vector3 particlespawnposition; // the position the particle will spawn on the ball (flourish, not to be used in final steps)
	private bool isEmitting=false; // is the particle emitting
	private Vector3 temppos; // temp variable for logic
	[SerializeField]
	public GameObject particler; //
	//increase the timer
	public /// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		{
			particlespawnposition=this.transform.position;
			temppos=new Vector3(0,3,0)+particlespawnposition; // spawn above cylinder. It is temporary
			particlespawnposition=temppos;
		}
	}
	public void increase() //increase time when in contact
	{
		targetTime-=Time.deltaTime;
			if((targetTime<=0)&&(isEmitting==false))
		{
			Debug.Log("over");
			Instantiate(particler,particlespawnposition,Quaternion.identity); //spawn particle emitter once
			isEmitting=true;			
		}
	}
}
