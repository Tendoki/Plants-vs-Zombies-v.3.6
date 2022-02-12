using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class BackgroudMusic : MonoBehaviour
{
	private GameManager gameManager;
	public AudioClip BackgroundMusicLevel;
	public AudioClip ChoosePlantsMusic;
	public AudioClip currentMusic;
	public static BackgroudMusic instance;
	private void Start()
	{
		gameManager = GameManager.instance;
		instance = this;
	}
	private void Awake()
	{
		instance = this;
	}
	private void Update()
	{
		if (gameManager.isLose || gameManager.isWin)
		{
			this.GetComponent<AudioSource>().Stop();
		}
	}

	public void StartBackgroundMusic()
    {
		StartCoroutine(DelayBackgroundMusic());
	}

	public void StartChoosePlantsMusic()
	{
		currentMusic = ChoosePlantsMusic;
		this.GetComponent<AudioSource>().clip = currentMusic;
		this.GetComponent<AudioSource>().Play();
	}

	public IEnumerator DelayBackgroundMusic()
    {
		yield return new WaitForSeconds(4f);
		currentMusic = BackgroundMusicLevel;
		this.GetComponent<AudioSource>().clip = currentMusic;
		this.GetComponent<AudioSource>().Play();
	}
}
