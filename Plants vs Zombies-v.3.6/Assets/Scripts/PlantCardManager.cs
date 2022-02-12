using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class PlantCardManager : MonoBehaviour
{
    [Header("Cards Parameters")]
    public int amtOfCards;
    //int currAmrCard = 0;
    //public PlantCardScriptableObject[] plantCardSO;
    public GameObject[] cardPrefab;
    public Transform cardHolderTransform;
    public PlantCardParameters[] plantCardP;
    [Header("Plant Parameters")]
    public List<GameObject> plantCards;
    public float cooldown;
    public int cost;
    //public Texture plantIcon;=
    public Transform selectionTransform;
    public GameObject[] selectionCardPrefab;
    public List<int> selectedIndexes;
    public List<GameObject> selectionCards;
    public static PlantCardManager instance;

    public int minCardAllowed;
    public Button LetsRockButton;
    public int CountPlantCard;
    public List<AudioClip> chooseAudio;
    private void Start()
	{
        amtOfCards = cardPrefab.Length;
        selectionCards = new List<GameObject>();
        instance = this;
        for (int i = 0; i < amtOfCards; i++)
        {
            plantCardP[i] = selectionCardPrefab[i].GetComponent<PlantCardParameters>();
            AddPlantCardSelection(i);
            cardPrefab[i].GetComponent<CardManager>().IndexInList = i;
        }
    }

    private void Update()
    {
        LetsRockButton.interactable = selectedIndexes.Count >= minCardAllowed;
    }

    private void Start_Old()
    {
        //for (int i = 0; i < cardPrefab.Length; i++)
        //{
        //          plantCardP[i] = cardPrefab[i].GetComponent<PlantCardParameters>();
        //      }
        for (int i = 0; i < amtOfCards; i++)
        {
            //AddPlantCard(i);
        }

    }

    public void AddPlantCard(int index)
    {
        if (selectedIndexes.Contains(index))
        {
            this.GetComponent<AudioSource>().PlayOneShot(chooseAudio[Random.Range(0, chooseAudio.Count)]);
            List<GameObject> tempObjList = new List<GameObject>(plantCards);
            tempObjList[index] = null;
            Destroy(plantCards[index]);
            plantCards = tempObjList;
            selectedIndexes.Remove(index);
            selectionCards[index].GetComponent<SelectionCard>().NotEnoughMoneyImage.rectTransform.offsetMin = new Vector3(0, 95, 0);
        }
        else
        {
            if (selectedIndexes.Count < CountPlantCard)
            {
                this.GetComponent<AudioSource>().PlayOneShot(chooseAudio[Random.Range(0, chooseAudio.Count)]);
                selectedIndexes.Add(index);
                GameObject card = Instantiate(cardPrefab[index], cardHolderTransform);
                CardManager cardManager = card.GetComponent<CardManager>();
                cardManager.plantCardParameters = cardPrefab[index].GetComponent<PlantCardParameters>();
                cardManager.plantSprite = cardPrefab[index].GetComponent<PlantCardParameters>().plantSprite;
                selectionCards[index].GetComponent<SelectionCard>().NotEnoughMoneyImage.rectTransform.offsetMin = new Vector3(0, 0, 0);
                plantCards[index] = card;
            }
        }
    }

    public void AddPlantCardSelection(int index)
    {
        GameObject card = Instantiate(selectionCardPrefab[index], selectionTransform);
        card.GetComponent<SelectionCard>().IndexInList = index;
        selectionCards.Add(card);
    }

}
