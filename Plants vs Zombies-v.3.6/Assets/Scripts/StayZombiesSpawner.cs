using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class StayZombiesSpawner : MonoBehaviour
{
	public List<GameObject> zombiesPrefabs;
	public List<Zombie> zombies;
	public GameManager gameManager;
	public GameObject zombiesSpawner;
	private void Start()
	{
		gameManager = GameManager.instance;
		for (int i = 0; i < zombies.Count; i++)
		{
			zombies[i].Spawner = i;
			GameObject zombieInstance = Instantiate(zombiesPrefabs[Random.Range(0, zombiesPrefabs.Count)], transform.GetChild(zombies[i].Spawner).transform);
			transform.GetChild(zombies[i].Spawner).GetComponent<ZombieSlot>().isFull = true;
			zombies[i].isSpawned = true;
			string layername = (zombies[i].Spawner).ToString();
			zombieInstance.GetComponent<SpriteRenderer>().sortingLayerName = "StayZombie" + layername;
		}
	}
}