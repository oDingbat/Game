﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	void Update () {
		transform.rotation *= Quaternion.Euler(10 * Time.deltaTime, 0, 0);
	}
}
