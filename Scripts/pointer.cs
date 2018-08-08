using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pointer : MonoBehaviour {
	[SerializeField]
	float depthDefault=10f; //depth if nothing is hit
	float pointerSpeed=2.5f;
	
	public Text temp;
	public int temperature;
	Vector3 CurrentPos;
	Vector3 TargetPos;
	Vector3 buffer=new Vector3(0.1f,0.1f,1.1f);
	// Use this for initialization it is ineffective when you look the other direction. we need direction normals I think
	void Start () {
		Debug.Log("Start");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Debug.Log("Running");
		transform.Translate(Vector3.up*Time.deltaTime);
		pointerMovement();
		temp.text=temperature.ToString();
	}

	void OnCollisionStay(Collision collisioninfo)
	{
		temperature=255;
		Debug.Log("Yee"+ collisioninfo.gameObject.name);
		Timer timer = collisioninfo.gameObject.GetComponent<Timer>();
		if (timer!=null)
		{
			timer.increase();			
		}
		
	}

	 void OnCollisionExit(Collision other) {
		 temperature=0;
		 
	}
	void pointerMovement(){
		//float rate=.5f;
		//float scale= (Mathf.Sin(Time.time*(rate*2*Mathf.PI))+1f)/2f;
		//scale =Mathf.Lerp (.8f,1.5f,scale);
		//transform.localScale = Vector3.one*scale;
		Vector3 mousePos= new Vector3(Screen.width/4,Screen.height/2,depthDefault);
		Debug.Log(mousePos);
		Vector3 cubePos= Camera.main.ScreenToWorldPoint(mousePos);
		
		//ray logic
		Ray ray = Camera.main.ScreenPointToRay(mousePos);
		RaycastHit hit;
		if(Physics.Raycast(ray,out hit,depthDefault)&&(hit.transform.tag!="Pointer"))
		{
			TargetPos=hit.point+buffer;
			Debug.Log(hit.collider);
		
		}
		else
		{
			TargetPos=ray.GetPoint(depthDefault);
			Debug.Log(hit.collider);
		}
		
		Debug.DrawLine(Camera.main.transform.position,TargetPos,Color.red);
		transform.position=Vector3.Lerp(transform.position,TargetPos,Time.deltaTime*pointerSpeed);
	}
}
