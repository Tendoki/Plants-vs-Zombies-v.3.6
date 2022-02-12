using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[Header("Drag Parameters")]
	public GameObject draggingObject;
	public GameObject draggingShovel;
	public GameObject currentSlotShovel;
	public GameObject currentSlot;
	public GameObject currentPlant;
	public static GameManager instance;
	int sceneIndex;
	int levelComplete;
	[Header("Sun Parameters")]
	public TMP_Text sunDisp;
	public int startingSunAmnt;
	public int SunAmount = 0;
	public bool isLose = false;
	public bool isWin = false;
	public bool isStartMatch = false;
	public bool isSwitchStatusMatch = false;
	public int zombiesLevel1;
	public List<AudioClip> destroyAudio;
	public AudioClip ReadySetPlantAudio;
	public Animator CameraPan;

	public SunSpawner sunSpawner;

	public ZombiesSpawner zombiesSpawner;

	public BackgroudMusic backgroudMusicLevel;
	public GameObject StartingZombies;
	public bool isCardsBlocked = false;

	private void Start()
	{
		sceneIndex = SceneManager.GetActiveScene().buildIndex;
		levelComplete = PlayerPrefs.GetInt("LevelComplete");
		CardManager.isGameStart = false;
		AddSun(startingSunAmnt);
		instance = this;
		backgroudMusicLevel.StartChoosePlantsMusic();
	}
	public void StartMatch()
	{
		CameraPan.SetTrigger("PanToPlants");
		isCardsBlocked = true;
		backgroudMusicLevel.StartBackgroundMusic();
		StartCoroutine(DelayBeforeStarting());
		StartCoroutine(ReadySetPlant());
		StartCoroutine(DelayForStartingZombies());
	}
	public IEnumerator DelayBeforeStarting()
	{
		yield return new WaitForSeconds(4f);
		isCardsBlocked = false;
		CardManager.isGameStart = true;
		isStartMatch = true;
		zombiesSpawner.StartSpawnZombies();
		if(!this.GetComponent<LevelParametr>().isNight)
        {
			sunSpawner.StartSpawnSun();
		}
	}
	public IEnumerator DelayForStartingZombies()
    {
		yield return new WaitForSeconds(2f);
		StartingZombies.SetActive(false);
	}

	public IEnumerator ReadySetPlant()
	{
		yield return new WaitForSeconds(2f);
		this.GetComponent<AudioSource>().clip = ReadySetPlantAudio;
		this.GetComponent<AudioSource>().Play();
	}

	private void Update()
	{
		if (isWin)
		{
			LevelController.instance.isEndGame();
		}
	}

	public void AddSun(int amnt)
	{
		SunAmount += amnt;
		sunDisp.text = "" + SunAmount;
	}
	public void DeductSun(int amnt)
	{
		SunAmount -= amnt;
		sunDisp.text = "" + SunAmount;
	}

	private void Awake()
	{
		instance = this;
	}

	public void PlaceObjectShooter()
	{
		if (draggingObject != null && currentSlot != null)
		{
			GameObject objectGame = Instantiate(draggingObject.GetComponent<PlantDragging>().card.plant, currentSlot.transform);
			objectGame.GetComponent<PlantAttackController>().zombies = currentSlot.GetComponent<SlotsManagerCollider>().spawnPoint.zombies;
			objectGame.transform.parent = currentSlot.transform;
			currentSlot.GetComponent<SlotsManagerCollider>().isFull = true;
		}
	}
	public void PlaceObjectSunflower()
	{
		if (draggingObject != null && currentSlot != null)
		{
			GameObject objectGame = Instantiate(draggingObject.GetComponent<PlantDragging>().card.plant, currentSlot.transform);
			objectGame.transform.parent = currentSlot.transform;
			currentSlot.GetComponent<SlotsManagerCollider>().isFull = true;
		}
	}
	public void PlaceObjectWallNut()
	{
		if (draggingObject != null && currentSlot != null)
		{
			GameObject objectGame = Instantiate(draggingObject.GetComponent<PlantDragging>().card.plant, currentSlot.transform);
			objectGame.transform.parent = currentSlot.transform;
			currentSlot.GetComponent<SlotsManagerCollider>().isFull = true;
		}
	}

	public void PlaceObjectPlant()
	{
		if (draggingObject != null && currentSlot != null)
		{
			GameObject objectGame = Instantiate(draggingObject.GetComponent<PlantDragging>().card.plant, currentSlot.transform);
			if (draggingObject.GetComponent<PlantDragging>().card.plantCardParameters.isAttackPlant)
            {
				objectGame.GetComponent<PlantAttackController>().zombies = currentSlot.GetComponent<SlotsManagerCollider>().spawnPoint.zombies;
			}
            if (draggingObject.GetComponent<PlantDragging>().card.plantCardParameters.isBombPlant)
            {
				objectGame.GetComponent<PlantBombController>().zombies = currentSlot.GetComponent<SlotsManagerCollider>().spawnPoint.zombies;
			}
			objectGame.transform.parent = currentSlot.transform;
			currentSlot.GetComponent<SlotsManagerCollider>().isFull = true;
		}
	}

	public void PlaceObjectCherryBomb()
	{
		if (draggingObject != null && currentSlot != null)
		{
			GameObject objectGame = Instantiate(draggingObject.GetComponent<PlantDragging>().card.plant, currentSlot.transform);
			objectGame.GetComponent<PlantBombController>().zombies = currentSlot.GetComponent<SlotsManagerCollider>().spawnPoint.zombies;
			objectGame.transform.parent = currentSlot.transform;
			currentSlot.GetComponent<SlotsManagerCollider>().isFull = true;
		}
	}

	public void DestroyObjectShovel()
	{
		if (draggingShovel != null && currentPlant != null)
		{
			//currentSlotShovel.GetComponent<SlotsManagerCollider>().isFull = false;
			currentPlant.transform.parent.GetComponent<SlotsManagerCollider>().isFull = false;
			this.GetComponent<AudioSource>().PlayOneShot(destroyAudio[Random.Range(0, destroyAudio.Count)]);
			Destroy(currentPlant);
		}
	}
}
