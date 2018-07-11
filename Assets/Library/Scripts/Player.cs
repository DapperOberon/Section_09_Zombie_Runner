using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float maxHealth = 100f;
	private float health;
	public Slider healthSlider;

	public GameObject deathScreen;

	private bool respawn;

	public GameObject landingAreaPrefab;
	public Transform spawnPointParent;
	private Transform[] spawnPoints;

	private void Start()
	{
		spawnPoints = spawnPointParent.GetComponentsInChildren<Transform>();
		health = maxHealth;
	}

	// Update is called once per frame
	private void Update () {
		if (respawn)
		{
			Respawn();
		}

		healthSlider.value = health;

		if (health <= 0)
		{
			deathScreen.SetActive(true);
			Time.timeScale = 0;
		}
	}

	private void Respawn()
	{
		transform.position = spawnPoints[Random.Range(1, 4)].position;
		respawn = false;
	}

	private void OnFindClearArea()
	{
		Invoke("DropFlare", 3f);
	}

	void DropFlare()
	{
		Instantiate(landingAreaPrefab, transform.position, transform.rotation);
	}

	IEnumerator OnTriggerStay(Collider other)
	{
		yield return new WaitForSeconds(1f);
		if (other.gameObject.CompareTag("Zombie"))
		{
			health -= 0.5f;
		}
	}
}
