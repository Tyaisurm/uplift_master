using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorController : MonoBehaviour {
	public float speed = 1.0f;//how fast? from 1-100
	public float placingspeed = 1.0f;
	public int height = 1;//how many floors use with elevator?
	public GameObject elevator_box;
	public bool placed = false;

	private SpriteRenderer sprRend;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!placed){
			if(Input.GetKeyDown ("right")){
				gameObject.transform.Translate (Vector2.right * 10);
			}
			else if(Input.GetKeyDown ("left")){
				gameObject.transform.Translate (Vector2.left * 10);
			}
			else if(Input.GetKeyDown ("up")){
				gameObject.transform.Translate (Vector2.up * 10);
			}
			else if(Input.GetKeyDown ("down")){
				gameObject.transform.Translate (Vector2.down * 10);
			}
		}
		if(!placed && Input.GetKeyDown("space")){

			Debug.Log ("PLACED ELEVATOR");
			placed = true;
		}
	}
		
}