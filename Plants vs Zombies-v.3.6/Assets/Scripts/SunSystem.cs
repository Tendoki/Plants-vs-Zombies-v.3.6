using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]

public class SunSystem : MonoBehaviour
{
    public int SunValue;
	public bool isSunflower = false;
	public int spawnTime = 8;
	public List<AudioClip> sunAudio;
	public float DelaySound = 1f;
	public SpriteRenderer backgroundSprite;
	private bool isSunTake = false;

	private void OnMouseDown()
	{
		if (!isSunTake)
		{
			this.GetComponent<AudioSource>().PlayOneShot(sunAudio[Random.Range(0, sunAudio.Count)]);
			GameObject.FindObjectOfType<GameManager>().AddSun(SunValue);
			isSunTake = true;
			backgroundSprite.enabled = false;
			StartCoroutine(DelaySoundSun());
		}
	}
	private void Start()
	{
		if (isSunflower == true)
		{
			StartCoroutine(DestroySun());
		}
	}
	public IEnumerator DelaySoundSun()
	{
		yield return new WaitForSeconds(DelaySound);
		Destroy(this.gameObject);
	}
	public IEnumerator DestroySun()
	{
		yield return new WaitForSeconds(spawnTime);
		Destroy(this.gameObject);
	}
}
