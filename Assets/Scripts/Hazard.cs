using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {
    public float Damage = 10;

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlatformerCharacter2D>().TakeDamage(Damage);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlatformerCharacter2D>().TakeDamage(Damage);
		}
	}
}
