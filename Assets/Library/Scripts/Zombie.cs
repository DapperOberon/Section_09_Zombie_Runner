using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {

	private Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			anim.SetBool("isAttacking", true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		anim.SetBool("isAttacking", false);
	}
}
