using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public GameObject customer;
	public int totalscore;
	private GameObject[] spawnDoors;
	public bool gameover;

	private float timer;
	public GameObject timertext;
	private Text timert;
	public GameObject gotext;
	public GameObject gopanel;

	public GameObject scoretext;
	private Text guiscore;
	public GameObject ppmtext;
	private Text guippm;


	// Use this for initialization
	void Start () {
		guiscore = scoretext.GetComponent<Text>();
		guippm = ppmtext.GetComponent<Text>();
		timert = timertext.GetComponent<Text> ();

		spawnDoors = GameObject.FindGameObjectsWithTag ("Respawn");

		foreach (GameObject spawn in spawnDoors) {
			Instantiate (customer, new Vector3 (spawn.transform.position.x, spawn.transform.position.y - 1, 0), spawn.transform.rotation);
		}

		timer = 60.0f;
		gameover = false;
	}
	

	void Update () { // Number of people / minute
		if (!gameover) {
			guippm.text = (6 * totalscore / (Time.time)).ToString ("F2");

			timer -= Time.deltaTime;
			string min = Mathf.Floor (timer / 60).ToString ("00");
			string sec = (timer % 60).ToString ("00");

			timert.text = string.Format ("{0}:{1}", min, sec);
			if (timer < 0f) {
				gameover = true;
				timert.text = "00:00";
				gotext.SetActive (true);
				gopanel.SetActive (true);
			}

		}
	}

	public void AddScore(int score) {
		totalscore += score * 10;
		timer += 1;
		guiscore.text = totalscore.ToString ();
		if (!gameover) {
			SpawnDude ();
		}
	}


	private void SpawnDude () {
		int index = Random.Range (0,10);
		Instantiate (customer, new Vector3 (spawnDoors[index].transform.position.x, spawnDoors[index].transform.position.y - 1, 0), Quaternion.identity);
	}
}
