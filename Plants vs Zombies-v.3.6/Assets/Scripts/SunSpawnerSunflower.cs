using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SunSpawnerSunflower : MonoBehaviour
{
	public bool isSunflower;
	public float minTime;
	public float maxTime;
	public float time;
	public GameManager gameManager;
	public GameObject sun;
	public Vector2 minPos;
	public Vector2 maxPos;
	Vector3 pos;
	public List<AudioClip> sunAudio;

	private void Start()
	{
		gameManager = GameManager.instance;
		time = 5;
		if (!isSunflower)
		{
			pos.x = Random.Range(minPos.x, maxPos.x);
			pos.y = Random.Range(minPos.y, maxPos.y);
			pos.z = -1;
		}
		else
		{
			pos.x = 0;
			pos.y = 0;
			pos.z = -1;
		}
		StartCoroutine(SpawnSun());
	}

	private void Update()
	{
		if (gameManager.isLose || gameManager.isWin)
		{
			StopCoroutine(SpawnSun());
		}
	}

	public IEnumerator SpawnSun()
	{
		yield return new WaitForSeconds(time);
		time = 23;
		GameObject SunObject = Instantiate(sun, pos, Quaternion.identity);
		this.GetComponent<AudioSource>().PlayOneShot(sunAudio[Random.Range(0, sunAudio.Count)]);
		if (!isSunflower)
		{
			pos.x = Random.Range(minPos.x, maxPos.x);
			pos.y = Random.Range(minPos.y, maxPos.y);
			pos.z = -1;
		}
		else
		{
			pos.x = 0;
			pos.y = 0;
			pos.z = -1;
			SunObject.GetComponent<SunSystem>().isSunflower = true;
			SunObject.transform.position = new Vector3(0, 1, -1);
			SunObject.transform.parent = this.transform;
			SunObject.transform.localPosition = new Vector3(0, 1, -1);
		}
		if (gameManager.isLose || gameManager.isWin)
		{
			StopCoroutine(SpawnSun());
		}
		else
		{
			StartCoroutine(SpawnSun());
		}
	}
	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Sun")
		{
			Destroy(collision.gameObject);
		}
	}
}
