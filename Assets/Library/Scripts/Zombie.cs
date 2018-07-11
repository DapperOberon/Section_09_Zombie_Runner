using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {

	public AudioClip[] audioClips;
	private Animator anim;
	private AudioSource audioSource;

	private void Start()
	{
		anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		audioSource.pitch = Random.Range(0.9f, 1.1f);
		audioSource.clip = audioClips[0];
		audioSource.Play();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			audioSource.Stop();
			audioSource.clip = audioClips[1];
			audioSource.Play();
			anim.SetBool("isAttacking", true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		audioSource.Stop();
		audioSource.clip = audioClips[0];
		audioSource.Play();
		anim.SetBool("isAttacking", false);
	}
}
