using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class ShovelCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
	public GameObject UI;
	public Sprite ShovelSprite;
	public SpriteRenderer ShovelSpriteRenderer;
	public GameObject Shovel;
	public GameObject Shovel_Drag;
	protected GameObject objectDragInstance;
	protected GameManager gameManager;
	protected bool isHoldingShovel = false;
	public AudioClip draggingAudio;
	private void Start()
	{
		gameManager = GameManager.instance;
	}

	private void Update()
	{

	}
	public void OnDrag(PointerEventData eventData)
	{
		if (!gameManager.isLose && !gameManager.isWin)
		{
			if (isHoldingShovel)
			{
				objectDragInstance.GetComponent<SpriteRenderer>().sprite = Shovel_Drag.GetComponent<SpriteRenderer>().sprite;
				objectDragInstance.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (!gameManager.isLose && !gameManager.isWin)
		{
			this.gameObject.GetComponent<Image>().enabled = false;
			isHoldingShovel = true;
			Vector3 pos = new Vector3(0, 0, -1);
			objectDragInstance = Instantiate(Shovel_Drag, pos, Quaternion.identity);
			objectDragInstance.GetComponent<SpriteRenderer>().sprite = Shovel_Drag.GetComponent<SpriteRenderer>().sprite;
			objectDragInstance.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			objectDragInstance.GetComponent<ShovelDragging>().card = this;
			this.GetComponent<AudioSource>().PlayOneShot(draggingAudio);
			gameManager.draggingShovel = objectDragInstance;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (!gameManager.isLose && !gameManager.isWin)
		{
			if (isHoldingShovel)
			{
				isHoldingShovel = false;
				gameManager.DestroyObjectShovel();
				gameManager.draggingShovel = null;
				Destroy(objectDragInstance);
				this.gameObject.GetComponent<Image>().enabled = true;
			}
		}
	}
}
