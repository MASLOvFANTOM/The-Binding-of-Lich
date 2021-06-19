using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class InventoryItem : MonoBehaviour
{
    public InventoryItemData _InventoryItemData;
    public Image image;
    public Text meCountText;
    private Text text;
    public ParentCharactrsController _CharactrsController;
    public ItemDescriptionTab _ItemDescriptionTab;


    private void Start() // Инициализация
    {
        image = GetComponentInChildren<Image>();
        image.sprite = _InventoryItemData.mainSprite;
        meCountText = GetComponentInChildren<Text>();
        _CharactrsController = GameObject.FindGameObjectWithTag("Player").GetComponent<ParentCharactrsController>();
        _ItemDescriptionTab = GameObject.FindGameObjectWithTag("Item Description Tab").GetComponent<ItemDescriptionTab>();
        text = GetComponentInChildren<Text>();

        _InventoryItemData.meCount = 0;
    }

    private void Update()
    {
        if (_InventoryItemData.meCount == 0) // Если в инвенторе нет
        {
            transform.SetSiblingIndex(transform.parent.childCount);
            image.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
        }
        else // Если есть
        {
            meCountText.text = _InventoryItemData.meCount.ToString();
            image.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
        }
    }

    public void Effect() // эффекты
    {
        _InventoryItemData.meCount--;
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
        _ItemDescriptionTab.SetUp(_InventoryItemData.description, this);
    }
}
