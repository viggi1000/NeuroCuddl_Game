using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pointer : MonoBehaviour {
	[SerializeField]
	float depthDefault=10f; //depth if nothing is hit
	float pointerSpeed=2.5f; // the response speed of the pointer ( affects the linear interpolation factor of the smooth movement)
	
	public Text temp; //Text that is sent to Cuddl temp stands for temperature
	public int temperature; // controls for peltire 
	Vector3 CurrentPos; // the current position of the pointer
	Vector3 TargetPos; // position of the target the pointer should move towards
	Vector3 buffer=new Vector3(0.1f,0.1f,1.1f);
	// Use this for initialization it is ineffective when you look the other direction. we need direction normals I think
	void Start () {
		Debug.Log("Start"); // Initialization 
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Debug.Log("Running");
		//transform.Translate(Vector3.up*Time.deltaTime); //dont know why I wrote this
		pointerMovement(); // Movement of the ball
		temp.text=temperature.ToString(); // send Temperature data to the Cuddl
	}

	void OnCollisionStay(Collision collisioninfo)
	{
		temperature=255; //Peltier Response Value
		Debug.Log("Yee"+ collisioninfo.gameObject.name); //Debug state to see the object it is interacting with
		Timer timer = collisioninfo.gameObject.GetComponent<Timer>(); //Link with timer script
		if (timer!=null)
		{
			timer.increase();	//increase count if touching an object		
		}
		
	}

	 void OnCollisionExit(Collision other) {
		 temperature=0; // reset temp to zero ( interpolation of temp is handled by the peltier and the beauty of thermodynamics at this point)
		 
	}
	void pointerMovement(){
		//float rate=.5f;
		//float scale= (Mathf.Sin(Time.time*(rate*2*Mathf.PI))+1f)/2f;
		//scale =Mathf.Lerp (.8f,1.5f,scale);
		//transform.localScale = Vector3.one*scale;
		
		Vector3 mousePos= new Vector3(Screen.width/4,Screen.height/2,depthDefault); // slightly offcenter in order for the cardboard view to be correct
		Debug.Log(mousePos);
		Vector3 cubePos= Camera.main.ScreenToWorldPoint(mousePos); // turn 2d point on screen to 3d point on game
		
		//ray logic
		Ray ray = Camera.main.ScreenPointToRay(mousePos);
		RaycastHit hit;
		if(Physics.Raycast(ray,out hit,depthDefault)&&(hit.transform.tag!="Pointer")) // if the ray hits an object that is a certain distance infront of it and that object is not the ball itself
		{
			TargetPos=hit.point+buffer;
			Debug.Log(hit.collider); // the object it is colliding with
		
		}
		else
		{
			TargetPos=ray.GetPoint(depthDefault); // default position infront of camera
			Debug.Log(hit.collider); // the object it is colliding with
		}
		
		Debug.DrawLine(Camera.main.transform.position,TargetPos,Color.red); // debug state, draws line between camera and ball
		transform.position=Vector3.Lerp(transform.position,TargetPos,Time.deltaTime*pointerSpeed); // smoothly interpolates the ball while obeying physics.
	}
}
