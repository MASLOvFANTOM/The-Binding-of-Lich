using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "items/Game Items", fileName = "game item")]
public class InventoryItemData : ScriptableObject
{
    public Image mainSprite;
    public int meCount;
    public string description;
    

    public void Effect(){}
}