using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class CardManager : MonoBehaviour
{
	//public GameObject object_Drag;
	//public GameObject object_Game;
	//public Canvas canvas;
	//protected GameObject objectDragInstance;
	//protected GameManager gameManager;
	public GameObject UI;
	//public SlotsManagerCollider colliderName;
	//SlotsManagerCollider prevName;
	public PlantCardParameters plantCardParameters;
	public Sprite plantSprite;
	//public GameObject plantPrefab;
	//public bool isOverCollider = false;
	public GameObject plant;
	public GameObject plant_Drag;
	protected GameObject objectDragInstance;
	protected GameManager gameManager;
	protected bool isHoldingPlant = false;
	public List<AudioClip> draggingAudio;
	public bool isCoolingDown;
	protected bool isFirstPlant = true;
	public Image refreshImage;
	public Image NotEnoughMoneyImage;
	public AudioClip seedliftAudio;
	public AudioClip buzzerAudio;

	public bool isStart = false;
	public bool isSelection;
	public bool isSelected;
	public static bool isGameStart = false;
	public PlantCardManager plantCardManager;
	public GameObject parentCard;
	public int IndexInList;

	[Tooltip("X: Max Height, Y: Min Height")]
	public Vector2 height;
	private void Start()
	{
		gameManager = GameManager.instance;
		plantCardManager = PlantCardManager.instance;
	}

	private void Update()
	{
		if (gameManager.isStartMatch)
		{
			if (gameManager.SunAmount >= plantCardParameters.cost && !isCoolingDown)
			{
				NotEnoughMoneyImage.rectTransform.offsetMin = new Vector3(0, 95, 0);
			}
			else
			{
				NotEnoughMoneyImage.rectTransform.offsetMin = new Vector3(0, 0, 0);
			}
		}
		if (!isStart && gameManager.isStartMatch)
		{
			isStart = true;
			if (gameManager.SunAmount >= plantCardParameters.cost && !isCoolingDown)
			{
				NotEnoughMoneyImage.rectTransform.offsetMin = new Vector3(0, 95, 0);
			}
			else
			{
				NotEnoughMoneyImage.rectTransform.offsetMin = new Vector3(0, 0, 0);
			}
			refreshImage.rectTransform.offsetMin = new Vector3(0, height.y, 0);
			if (!this.GetComponent<PlantCardParameters>().isSunflower)
			{
				StartCoroutine(CardCooldown(plantCardParameters.cooldown));
			}
		}
	}

	public IEnumerator CardCooldown(float cooldownDuration)
	{
		if (gameManager.SunAmount >= plantCardParameters.cost && !isCoolingDown)
		{
			NotEnoughMoneyImage.rectTransform.offsetMin = new Vector3(0, 95, 0);
		}
		else
		{
			NotEnoughMoneyImage.rectTransform.offsetMin = new Vector3(0, 0, 0);
		}
		isCoolingDown = true;
		for (float i = height.x; i <= height.y; i++)
		{
			refreshImage.rectTransform.offsetMin = new Vector3(0, i, 0);
			yield return new WaitForSeconds(cooldownDuration / height.y);
		}
		isCoolingDown = false;
		if (gameManager.SunAmount >= plantCardParameters.cost && !isCoolingDown)
		{
			NotEnoughMoneyImage.rectTransform.offsetMin = new Vector3(0, 95, 0);
		}
		else
		{
			NotEnoughMoneyImage.rectTransform.offsetMin = new Vector3(0, 0, 0);
		}
	}
}
