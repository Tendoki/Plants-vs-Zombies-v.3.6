using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class WinningMusic : MonoBehaviour
{
	private GameManager gameManager;
	public AudioClip WinningAudio;
	public GameObject winText;
	public bool isFirst = true;
	private void Start()
	{
		gameManager = GameManager.instance;
	}
	private void Update()
	{
		if (gameManager.isWin && isFirst)
		{
			isFirst = false;
			winText.SetActive(true);
			this.GetComponent<AudioSource>().PlayOneShot(WinningAudio);
		}
	}
}
