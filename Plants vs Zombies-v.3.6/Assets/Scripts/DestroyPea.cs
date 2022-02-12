using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyPea : MonoBehaviour
{
	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 12)
		{
			Destroy(collision.gameObject);
		}

		if (collision.gameObject.layer == 19)
		{
			Destroy(collision.gameObject);
		}
	}
}
