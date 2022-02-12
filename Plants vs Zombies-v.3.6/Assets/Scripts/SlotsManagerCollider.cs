using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsManagerCollider : MonoBehaviour
{
	//public GameObject plant;
	//public bool isOccupied = false;
	//void OnMouseOver()
	//{
	//	foreach (CardManager item in GameObject.FindObjectsOfType<CardManager>())
	//	{
	//		item.colliderName = this.GetComponent<SlotsManagerCollider>();
	//		item.isOverCollider = true;
	//	}
	//	if (plant == null)
	//	{
	//		if (GameObject.FindGameObjectWithTag("Plant") != null)
	//		{
	//			plant = GameObject.FindGameObjectWithTag("Plant");
	//			plant.transform.SetParent(this.transform);
	//			Vector3 pos = new Vector3(0, 0, -1);
	//			plant.transform.localPosition = pos;
	//		}
	//	}
	//}
	//private void OnMouseExit()
	//{
	//	//Destroy(plant);
	//}
	public bool isFull;
	public GameManager gameManager;
	public SpriteRenderer backgroundSprite;
	public SpawnPoint spawnPoint;
	private void Start()
	{
		gameManager = GameManager.instance;
	}
	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (gameManager.draggingObject != null && isFull == false)
		{
			if (collision.gameObject.tag == "Plant")
			{
				gameManager.currentSlot = this.gameObject;
				backgroundSprite.enabled = true;
			}
		}
	}

	public void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Plant")
		{
			gameManager.currentSlot = null;
			backgroundSprite.enabled = false;
		}

		if (collision.gameObject.tag == "Sun" && collision.gameObject.GetComponent<SunSystem>().isSunflower == true)
		{
			Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
		}
	}
}
