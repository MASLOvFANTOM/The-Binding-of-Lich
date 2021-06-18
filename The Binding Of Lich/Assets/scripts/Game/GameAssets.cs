using UnityEngine;

public class GameAssets : MonoBehaviour
{
    // Все нужные ссылки в одном месте, как это удобно
    [Tooltip("Монстры для спавна")]public GameObject[] monsterForSpawnLevelOne;
    [Tooltip("Ловушки для спавна")]public GameObject[] trapsForSpawn;
    [Tooltip("Интерактивные объекты для спавна")]public GameObject[] interactiveObjForSpawn;
    [Tooltip("Префаб дамага")]public DamagePopUp _DamagePopUp;
    public GameObject inventoryContent;
}
