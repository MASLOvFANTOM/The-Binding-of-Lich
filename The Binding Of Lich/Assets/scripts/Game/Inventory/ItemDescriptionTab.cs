using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class ItemDescriptionTab : MonoBehaviour
{
    public Text description;
    public InventoryItem _InventoryItem;
    public Button useButton;
    public Animator animator;

    public void SetUp(string text, InventoryItem _inventoryItem)
    {
        description.text = text;
        _InventoryItem = _inventoryItem;
        animator.SetTrigger("on");
    }

    private void Hide()
    {
        animator.SetTrigger("off");
    }

    public void Exit()
    {
        Hide();
    }

    public void PressUse()
    {
        if (_InventoryItem.meCount > 0)
        {
            _InventoryItem.Effect();
            Hide();
        }
    }
}
