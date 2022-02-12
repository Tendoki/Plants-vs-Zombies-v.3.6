using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlantsCard : CardManager, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

	public void OnDrag(PointerEventData eventData)
	{
		if (!gameManager.isCardsBlocked)
		{
			if (!gameManager.isLose && !gameManager.isWin)
			{
				if (isCoolingDown)
				{
					return;
				}
				//Взять gameObject
				if (isHoldingPlant)
				{
					objectDragInstance.GetComponent<SpriteRenderer>().sprite = plantSprite;
					objectDragInstance.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				}
			}
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (!gameManager.isCardsBlocked)
		{
			if (gameManager.isStartMatch)
			{
				if (!gameManager.isLose && !gameManager.isWin)
				{
					if (isCoolingDown)
					{
						this.GetComponent<AudioSource>().PlayOneShot(buzzerAudio);
						return;
					}

					if (gameManager.SunAmount >= plantCardParameters.cost)
					{
						isHoldingPlant = true;
						Vector3 pos = new Vector3(0, 0, -1);
						objectDragInstance = Instantiate(plant_Drag, pos, Quaternion.identity);
						objectDragInstance.GetComponent<SpriteRenderer>().sprite = plantSprite;
						objectDragInstance.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
						objectDragInstance.GetComponent<PlantDragging>().card = this;
						this.GetComponent<AudioSource>().PlayOneShot(seedliftAudio);
						gameManager.draggingObject = objectDragInstance;
					}
                    else
                    {
						this.GetComponent<AudioSource>().PlayOneShot(buzzerAudio);
						return;
					}
				}
			}
			else
			{
				plantCardManager.AddPlantCard(IndexInList);
			}
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (!gameManager.isCardsBlocked)
		{
			if (!gameManager.isLose && !gameManager.isWin)
			{
				if (isCoolingDown)
				{
					return;
				}
				if (isHoldingPlant)
				{
					isHoldingPlant = false;
					gameManager.PlaceObjectPlant();
					if (gameManager.currentSlot != null)
					{
						gameManager.DeductSun(plantCardParameters.cost);
						this.GetComponent<AudioSource>().PlayOneShot(draggingAudio[Random.Range(0, draggingAudio.Count)]);
						StartCoroutine(CardCooldown(plantCardParameters.cooldown));
					}
					if (plantCardParameters.isSunflower)
					{
						SunSpawnerSunflower sunSpawner = plant.GetComponent<SunSpawnerSunflower>();
						sunSpawner.isSunflower = true;
						sunSpawner.time = plantCardParameters.sunSpawnerTemplate.time;
						sunSpawner.sun = plantCardParameters.sunSpawnerTemplate.sun;
					}
					gameManager.draggingObject = null;
					Destroy(objectDragInstance);
				}
			}
		}
	}
}
