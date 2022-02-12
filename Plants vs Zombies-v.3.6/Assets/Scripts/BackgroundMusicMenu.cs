using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class BackgroundMusicMenu : MonoBehaviour
{
	private GameManager gameManager;
	public AudioClip BackgroundMusic;
	public AudioClip currentMusic;
	private void Start()
	{
		gameManager = GameManager.instance;
		currentMusic = BackgroundMusic;
		this.GetComponent<AudioSource>().clip = currentMusic;
		this.GetComponent<AudioSource>().Play();
	}
}
