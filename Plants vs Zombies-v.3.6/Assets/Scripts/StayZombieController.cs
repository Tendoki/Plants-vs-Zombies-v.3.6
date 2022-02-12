using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayZombieController : MonoBehaviour
{
	private void Update()
	{
		if (Time.timeSinceLevelLoad > 3f)
		{
			Destroy(this.gameObject);
		}
	}
}
