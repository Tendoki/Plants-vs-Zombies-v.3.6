using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ZombiesSpawner : MonoBehaviour
{
	public List<GameObject> zombiesPrefabs;
	public List<Zombie> zombies;
	public GameManager gameManager;
	public AudioClip awoogaAudio;
	public AudioClip sirenAudio;
	public int countZombies;

	private void Start()
	{
		countZombies = 0;
		gameManager = GameManager.instance;
		gameManager.zombiesLevel1 = zombies.Count;
	}
	private void Update()
	{
		//if (!gameManager.isLose && !gameManager.isWin && gameManager.isStartMatch)
		//{
		//	foreach (Zombie zombie in zombies)
		//	{
		//		if (zombie.isSpawned == false && zombie.spawnTime <= Time.timeSinceLevelLoad)
		//		{
		//			if (zombie.RandomSpawn)
		//			{
		//				zombie.Spawner = Random.Range(0, transform.childCount);
		//			}
		//			GameObject zombieInstance = Instantiate(zombiesPrefabs[(int)zombie.zombieType], transform.GetChild(zombie.Spawner).transform);
		//			transform.GetChild(zombie.Spawner).GetComponent<SpawnPoint>().zombies.Add(zombieInstance);
		//			zombie.isSpawned = true;
		//			if (zombie.isFirst)
		//			{
		//				this.GetComponent<AudioSource>().PlayOneShot(awoogaAudio);
		//			}
		//			if (zombie.isFirstInWave)
		//			{
		//				this.GetComponent<AudioSource>().PlayOneShot(sirenAudio);
		//			}
		//			string layername = (zombie.Spawner).ToString();
		//			zombieInstance.GetComponent<SpriteRenderer>().sortingLayerName = "Zombie" + layername;
		//		}
		//	}
		//}
		//if (gameManager.isLose || gameManager.isWin)
  //      {
		//	StopCoroutine(StartSpawnZombies());
  //      }
	}
	public void StartSpawnZombies()
	{
		StartCoroutine(SpawnZombies());
	}

	public IEnumerator SpawnZombies()
    {
		yield return new WaitForSeconds(zombies[countZombies].spawnTime);
		if (zombies[countZombies].isSpawned == false)
		{
			if (zombies[countZombies].RandomSpawn)
			{
				zombies[countZombies].Spawner = Random.Range(0, transform.childCount);
			}
			GameObject zombieInstance = Instantiate(zombiesPrefabs[(int)zombies[countZombies].zombieType], transform.GetChild(zombies[countZombies].Spawner).transform);
			transform.GetChild(zombies[countZombies].Spawner).GetComponent<SpawnPoint>().zombies.Add(zombieInstance);
			zombies[countZombies].isSpawned = true;
			if (zombies[countZombies].isFirst)
			{
				this.GetComponent<AudioSource>().PlayOneShot(awoogaAudio);
			}
			if (zombies[countZombies].isFirstInWave)
			{
				this.GetComponent<AudioSource>().PlayOneShot(sirenAudio);
			}
			string layername = (zombies[countZombies].Spawner).ToString();
			zombieInstance.GetComponent<SpriteRenderer>().sortingLayerName = "Zombie" + layername;
			countZombies++;
		}
		if (gameManager.isLose || gameManager.isWin)
		{
			StopCoroutine(SpawnZombies());
		}
		else
		{
			if(countZombies < zombies.Count)
            {
				StartCoroutine(SpawnZombies());
			}
		}
	}
}