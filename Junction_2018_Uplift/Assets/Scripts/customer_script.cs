using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customer_script : MonoBehaviour {
	private int shirtcolor, destination;
	private SpriteRenderer rend;
	public Sprite red, blue, green, black;
	public TextMesh destText;
	public float speed;
	private bool jumping;
	private int jumpcounter;
	private float ypos;
	private Transform thinkbubble;

	private int currentFloor;

	private GameObject elevator;
	private elevator_script elevscript;
	private GameObject gameController;
	private GameController gamescript;

	void Start () {
		shirtcolor = Random.Range (0, 3);
		jumping = false;
		jumpcounter = 0;

		rend = GetComponent<SpriteRenderer>();
	
		speed *= 1 + ((Random.value - 0.5f) / 5);

		switch (shirtcolor) {
		case 0:
			rend.sprite = red;
			break;
		case 1:
			rend.sprite = blue;
			break;
		case 2:
			rend.sprite = green;
			break;
		default:
			print ("No shirt color chosen. This shouldn't be possible.");
			break;
		}

		ypos = gameObject.transform.position.y;
		if (ypos == 21) {
			currentFloor = 5;
		} else if (ypos == 10.5) {
			currentFloor = 4;
		} else if (ypos == 0) {
			currentFloor = 3;
		} else if (ypos == -10.5) {
			currentFloor = 2;
		} else if (ypos == -21) {
			currentFloor = 1;
		} else {
			print ("FFFFFUUUUU");
		}
		do {
			destination = Random.Range (1, 6);
		} while (destination == currentFloor);

		destText.text = destination.ToString ();

		thinkbubble = gameObject.transform.GetChild (0);
		thinkbubble.transform.Translate (Vector3.forward * Random.value);

		// Assign elevator
		if (gameObject.transform.position.x > 0) {
			elevator = GameObject.Find ("elevator2");
		} else {
			elevator = GameObject.Find ("elevator1");
		}
		elevscript = elevator.GetComponent<elevator_script> ();
		gameController = GameObject.Find ("GameController");
		gamescript = gameController.GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Go with the lift
		if (gameObject.transform.position.x > -9 && gameObject.transform.position.x < 9) {
			currentFloor = elevscript.floor_number;
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x, elevator.transform.position.y, gameObject.transform.position.z);
		}

		// Despawn if in destination
		if (gameObject.transform.position.x > 35 || gameObject.transform.position.x < -34) {
			gamescript.AddScore (1);
			Destroy (gameObject);
		}
			
		if (currentFloor == destination && !((gameObject.transform.position.x > -9 && gameObject.transform.position.x < 9) && elevscript.moving)) { // If on the destination floor, go to the destination
			if (gameObject.transform.position.x > 0) {
				gameObject.transform.Translate (speed * Vector3.right);
			} else {
				gameObject.transform.Translate (speed * Vector3.left);
			}
		} else if (elevscript.floor_number == currentFloor && !(gameObject.transform.position.x > -5.5 && gameObject.transform.position.x < 5.5)) { // Elevator is in the same floor, but customer is not yet in the elevator
			if (gameObject.transform.position.x > 0) {
				gameObject.transform.Translate (speed * Vector3.left);
			} else {
				gameObject.transform.Translate (speed * Vector3.right);
			}
		}

		if (currentFloor != elevscript.floor_number && currentFloor != destination) {
			
			if (gameObject.transform.position.x > 0) {
				if (gameObject.transform.position.x > 14) {
					gameObject.transform.Translate (speed * Vector3.left);
				} else {
					fidget ();
				}
			} else {
				if (gameObject.transform.position.x < -13.5) {
					gameObject.transform.Translate (speed * Vector3.right);
				} else {
					fidget ();
				}
			}
		}

		if (jumping) {
			gameObject.transform.Translate (.5f * Vector3.up);
			jumpcounter++;
			if (jumpcounter > 4) {
				gameObject.transform.Translate (.5f * Vector3.down);
				jumping = false;
			}
		} else if (jumpcounter > 0) {
			gameObject.transform.Translate (.5f * Vector3.down);
			jumpcounter--;
			if (jumpcounter == 0) {
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x, ypos, gameObject.transform.position.z);
			}
		}
	}

	void fidget() {
		if (Random.value > 0.95 && !jumping) {
			jump ();
		}
	}

	void jump() {
		jumping = true;
		jumpcounter = 0;
	}
}
