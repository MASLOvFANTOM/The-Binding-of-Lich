using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class InventoryItem : MonoBehaviour
{
    public string description;
    // public string meEffectTag;
    public int meCount = 0;
    public Text meCountText;
    public Image image;
    private Text text;
    public ParentCharactrsController _CharactrsController;
    public ItemDescriptionTab _ItemDescriptionTab;


    private void Start()
    {
        meCountText = GetComponentInChildren<Text>();
        _CharactrsController = GameObject.FindGameObjectWithTag("Player").GetComponent<ParentCharactrsController>();
        _ItemDescriptionTab = GameObject.FindGameObjectWithTag("Item Description Tab").GetComponent<ItemDescriptionTab>();
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (meCount == 0)
        {
            transform.SetSiblingIndex(transform.parent.childCount);
            image.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
        }
        else
        {
            meCountText.text = meCount.ToString();
            image.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
        }
    }

    public void Effect()
    {
        meCount--;
        switch (name)
        {
            case "mushroom blue":
                _CharactrsController.health += 3;
                break;
            case "mushroom red":
                _CharactrsController.health += 5;
                break;
            case "mushroom green":
                _CharactrsController.health += 2;
                break;
            case "potion health mini":
                _CharactrsController.health += Random.Range(15, 21);
                break;
            case "potion health middle":
                _CharactrsController.health += Random.Range(24, 51);
                break;
            case "potion health large":
                _CharactrsController.health += Random.Range(86, 121);
                break;
        }
        
    }

    public void MouseDown()
    {
        _ItemDescriptionTab.SetUp(description, this);
    }
}
