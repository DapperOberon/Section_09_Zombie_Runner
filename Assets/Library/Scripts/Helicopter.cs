using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Helicopter : MonoBehaviour {

	[Tooltip("Units per second")]
	public float airSpeed;
	[Tooltip("Units per second")]
	public float landingSpeed;
	private AudioSource audioSource;
	private bool bDispatched = false;
	private bool bLanding = false;
	private Vector3 landingArea;
	private Vector3 airTarget;
	private Vector3 landTarget;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void OnHelicopterDispatch()
	{
		audioSource.Play();
		bDispatched = true;
		//rb.velocity = new Vector3(0, 0, 55.55f); // roughly three minutes to come
		landingArea = GameObject.FindGameObjectWithTag("LandingArea").transform.position;
		airTarget = new Vector3(landingArea.x,landingArea.y + 20,landingArea.z);
		landTarget = new Vector3(landingArea.x, landingArea.y - 1, landingArea.z);
	}

	private void FixedUpdate()
	{
		float airStep = airSpeed * Time.deltaTime;
		float landingStep = landingSpeed * Time.deltaTime;

		if (bDispatched && !bLanding)
		{
			transform.position = Vector3.MoveTowards(transform.position, airTarget, airStep);
			print("Moving towards target airspace");
		}

		if (transform.position == airTarget && !bLanding)
		{
			print("Starting landing...");
			bLanding = true;
		}

		if (bLanding)
		{
			print("Landing...");
			transform.position = Vector3.MoveTowards(transform.position, landTarget, landingStep);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Time.timeScale = 0;
			print("Extracting...YOU WIN!!!");
		}
	}
}
