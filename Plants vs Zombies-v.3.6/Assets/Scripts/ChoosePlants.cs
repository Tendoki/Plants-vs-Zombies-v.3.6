using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ChoosePlants : MonoBehaviour
{
	private GameManager gameManager;
	private void Start()
	{
		gameManager = GameManager.instance;
	}
	private void Update()
	{
		if (gameManager.isLose || gameManager.isWin)
		{
			this.GetComponent<AudioSource>().Stop();
		}
	}

	public void StartChoosePlantsMusic()
	{
		this.GetComponent<AudioSource>().Play();
	}
}
