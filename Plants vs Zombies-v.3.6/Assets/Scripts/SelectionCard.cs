using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class SelectionCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public GameObject UI;
	public PlantCardParameters plantCardParameters;
	public Sprite plantSprite;
	protected GameManager gameManager;
	protected PlantCardManager plantCardManager;
	public bool isCoolingDown;
	public Image NotEnoughMoneyImage;

	public bool isSelected = false;
	public GameObject parentCard;
	public int IndexInList;

	[Tooltip("X: Max Height, Y: Min Height")]
	public Vector2 height;

	private void Start()
	{
		gameManager = GameManager.instance;
		plantCardManager = PlantCardManager.instance;
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		if (!isSelected)
		{
			isSelected = true;
			plantCardManager.AddPlantCard(IndexInList);
		}
		else
        {
			isSelected = false;
			plantCardManager.AddPlantCard(IndexInList);
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		return;
	}
}
