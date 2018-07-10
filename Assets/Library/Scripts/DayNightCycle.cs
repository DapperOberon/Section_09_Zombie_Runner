using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

	[Header("Day Length in Minutes")]
	public float timeScale = 60f;

	// Update is called once per frame
	void Update () {
		transform.Rotate(Time.deltaTime / (timeScale / 3.6f), 0, 0);
	}
}
