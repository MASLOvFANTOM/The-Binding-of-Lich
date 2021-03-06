using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    public bool clearRoom; // Зачищена ли комната
    public bool monstersSpawned; // Монстры заспавнены
    [Tooltip("Стартавая комната не создаёт врагов")]public bool startRoom;
    public RoomTemplate _roomTemplate;
    [Tooltip("Тёмное покрытие комнаты")]public Animation roomDark;
    [Space]

    // Spawn points
    [Header("Точки спавна")]
    [Tooltip("Точки появления монстров")]public List<Transform> monsterSpawnPoints;
    [Tooltip("Точки появления ловушек")]public List<Transform> trapsSpawnPoint;
    [Tooltip("Точки появления интерактивных объектов")]public List<Transform> interactiveObjSpawnPoint;
    
    // For spawn
    [Header("Объекты для спавна")]
    [Tooltip("Монстры которых можно спавнить")] public GameObject[] monstersInRoomForSpawn;
    [Tooltip("Ловушки которых можно спавнить")] public GameObject[] trapsInRoomForSpawn;
    [Tooltip("Интерактивные Объекты которых можно спавнить")] public GameObject[] interactiveObjForSpawn;
    
    [Header("Для проверики зачистки комнаты")]
    [Tooltip("Все двери в комнате")]public Door[] doors;
    [Tooltip("Все созданные монстры в комнате")]public List<GameObject> monstersInRoom;

    // Parent spawn points
    [Header("Родители точек спавна")]
    public GameObject trapsSpawnPointParent;
    public GameObject interactiveObjSpawnPointParent;
    public GameObject monsterSpawnPointParent;
    public GameAssets _RoomMasterSpawn;


    private void Start()
    {
        _RoomMasterSpawn = GameObject.FindGameObjectWithTag("GameAssets").GetComponent<GameAssets>();
        _roomTemplate = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        monstersInRoomForSpawn = _roomTemplate.monsters;
        if (startRoom)
        {
            roomDark.Play("fade off");
            clearRoom = true;
            OpenDoor();
        }
        SetObjForSpawn(); // Указать все объекты для спавна
        SetSpawnPoints(); // Указать все точки спавна объектов
    }

    private void SetSpawnPoints() // Указать все точки спавна объектов
    {
        for (int i = 0; i < trapsSpawnPointParent.transform.childCount; i++)
        {
            trapsSpawnPoint.Add(trapsSpawnPointParent.transform.GetChild(i));
        }
        for (int i = 0; i < interactiveObjSpawnPointParent.transform.childCount; i++)
        {
            interactiveObjSpawnPoint.Add(interactiveObjSpawnPointParent.transform.GetChild(i));
        }
        for (int i = 0; i < monsterSpawnPointParent.transform.childCount; i++)
        {
            monsterSpawnPoints.Add(monsterSpawnPointParent.transform.GetChild(i));
        }
    }

    public void SetObjForSpawn() // Указать все объекты для спавна
    {
        monstersInRoomForSpawn = _RoomMasterSpawn.monsterForSpawnLevelOne;
        trapsInRoomForSpawn = _RoomMasterSpawn.trapsForSpawn;
        interactiveObjForSpawn = _RoomMasterSpawn.interactiveObjForSpawn;
    }

    private void FixedUpdate()
    {
        CheckClearRoom(); // Проверка на зачистку комнаты
    }

    private void CheckClearRoom() // Проверка на зачистку комнаты
    {
        if (monstersSpawned)
        {
            if (monstersInRoom.Count == 0)
            {
                clearRoom = true;
                monstersSpawned = false;
                OpenDoor();
            }
        }
    }

    private void OpenDoor() //Открыть все двери в комнате
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].StartCoroutine("OpenDoor");
        }
    }
    private void CloseDoor() // Закрыть все двери в комнате
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].CloseDoor();
        }
    }

    private void SpawnMonsters() // Спавн Монстров
    {
        for (int i = 0; i < monsterSpawnPoints.Count; i++)
        {
            int rand = Random.Range(0, monstersInRoomForSpawn.Length);
            
            // Добавить newPos для оси Z
            Vector3 newPos = monsterSpawnPoints[i].position;
            newPos.z = -1;
            
            GameObject monster = Instantiate(monstersInRoomForSpawn[rand], newPos, Quaternion.identity);
            monster.GetComponent<MainEnemyParametrs>().myRoom = this;
            
            monstersInRoom.Add(monster);
        }

        monstersSpawned = true;
    }

    public void SpawnTraps() // Спавн ловушек в комнате
    {
        for (int i = 0; i < trapsSpawnPoint.Count; i++)
        {
            int rand = Random.Range(0, 101);
            int randObj = Random.Range(0, trapsInRoomForSpawn.Length);
            
            if (rand > 50)
            {
                Vector3 newPos = trapsSpawnPoint[i].transform.position;
                // newPos.z = (trapsSpawnPoint[i].transform.localPosition.y * -1) / ((trapsSpawnPoint[i].transform.localPosition.y + 1) * -1);
                newPos.z = 0;

                Instantiate(trapsInRoomForSpawn[randObj], newPos, Quaternion.identity);
            }
        }
    }

    public void SpawnInteractiveObjects() // Спавн интерактивных объектов
    {
        for (int i = 0; i < interactiveObjSpawnPoint.Count; i++)
        {
            int rand = Random.Range(0, 101);
            int randObj = Random.Range(0, interactiveObjForSpawn.Length);
            if (rand > 25)
            {
                Vector3 newPos = interactiveObjSpawnPoint[i].transform.position;
                newPos.z = (interactiveObjSpawnPoint[i].transform.localPosition.y * -1) / ((interactiveObjSpawnPoint[i].transform.localPosition.y + 1) * -1);
                Instantiate(interactiveObjForSpawn[randObj], newPos, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // Стартуется, Если комната не зачищена
    {
        if (other.CompareTag("Player") && !clearRoom) // Стартовать прохождение комнаты если не зачищена
        {
            roomDark.Play("fade off");
            SpawnMonsters(); // Спавн монстров
            SpawnTraps(); // Спавн ловушек
            SpawnInteractiveObjects(); // Спавн интерактивных объектов
            CloseDoor(); // Закрыть двери
        }
    }

    
}
