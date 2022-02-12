using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class DeathManager : MonoBehaviour
{
	public GameObject restartButton;
	public GameObject loseText;
	public GameManager gameManager;
	public AudioClip loseAudio;
	private void Start()
	{
		gameManager = GameManager.instance;
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 11)
		{
			gameManager.isLose = true;
			restartButton.SetActive(true);
			loseText.SetActive(true);
			this.GetComponent<AudioSource>().PlayOneShot(loseAudio);
		}
	}
}
