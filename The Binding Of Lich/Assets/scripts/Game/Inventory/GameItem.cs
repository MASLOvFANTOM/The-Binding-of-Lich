using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameItem : MonoBehaviour
{
    public InventoryItemData _InventoryItemData;
    public int amount = 1;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0 ,0, Random.Range(0, 361));
        GetComponent<SpriteRenderer>().sprite = _InventoryItemData.mainSprite;
        transform.DOMove(new Vector3(transform.position.x + Random.Range(-1, 2),transform.position.y +  Random.Range(-1, 2), 1.7f), 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D other) // Игрок подобрал
    {
        if (other.CompareTag("Player"))
        {
            _InventoryItemData.meCount += amount;
            Destroy(gameObject);
        }
    }
}