﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public LayerMask	collisionMask;

	public float		velocity;
	float				lifespan = 10;

	float				distanceLeft = 0;
	bool				broken;

	void Start () {
		StartCoroutine(AutoDestroy());
	}

	IEnumerator AutoDestroy () {
		yield return new WaitForSeconds(lifespan);
		Destroy(gameObject);
	}

	void Update () {
		distanceLeft = velocity * Time.deltaTime;
		AttemptMove();
	}

	void AttemptMove () {
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, distanceLeft, collisionMask)) {
			if (hit.transform) {
				if (hit.transform.gameObject.tag == "Player") {
					GameObject.Find("Player Body").GetComponent<Player>().TakeDamage(25);
					StartCoroutine(BreakProjectile(hit.point));
				} else {
					distanceLeft = distanceLeft - Vector3.Distance(transform.position, hit.point);
					transform.position = hit.point;
					transform.rotation = Quaternion.LookRotation(Vector3.Reflect(transform.forward, hit.normal));
					float minDist = Mathf.Clamp(0.001f, 0, distanceLeft);
					transform.position += transform.forward * minDist;
					distanceLeft -= minDist;
				}
			}
		} else {
			transform.position += transform.forward * distanceLeft;
		}
	}

	IEnumerator BreakProjectile(Vector3 finalPos) {
		if (broken == false) {
			broken = true;
			yield return new WaitForSeconds(GetComponent<TrailRenderer>().time);
			Destroy(gameObject);
		} else {
			yield return null;
		}
	}

}