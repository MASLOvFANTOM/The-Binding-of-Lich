using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    public bool clearRoom;
    public bool monstersSpawned;
    [Tooltip("Стартавая комната не создаёт врагов")]public bool startRoom;
    public RoomTemplate _roomTemplate;
    [Tooltip("Тёмное покрытие комнаты")]public Animation roomDark;
    [Header("Списки")]
    [Tooltip("Точки появления монстров")]public Transform[] monsterSpawnPoints;
    [Tooltip("Монстры которых можно спавнить")]public GameObject[] monstersInRoomForSpawn;
    [Tooltip("Все двери в комнате")]public Door[] doors;
    [Tooltip("Все созданные монстры в комнате")]public List<GameObject> monstersInRoom;
    

    private void Start()
    {
        _roomTemplate = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        monstersInRoomForSpawn = _roomTemplate.monsters;
        if (startRoom)
        {
            roomDark.Play("fade off");
            clearRoom = true;
            OpenDoor();
        }
    }

    private void FixedUpdate()
    {
        CheckClearRoom();
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
        for (int i = 0; i < monsterSpawnPoints.Length; i++)
        {
            int rand = Random.Range(0, monstersInRoomForSpawn.Length);
            GameObject monster = Instantiate(monstersInRoomForSpawn[rand], monsterSpawnPoints[i].position, Quaternion.identity);
            monster.GetComponent<MainEnemyParametrs>().myRoom = this;
            monstersInRoom.Add(monster);
        }

        monstersSpawned = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !clearRoom)
        {
            roomDark.Play("fade off");
            SpawnMonsters();
            CloseDoor();
        }
    }

    
}
