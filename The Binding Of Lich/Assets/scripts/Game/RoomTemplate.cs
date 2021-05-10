using System.Collections.Generic;
using UnityEngine;

public class RoomTemplate : MonoBehaviour
{
    // Rooms
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    
    public GameObject closedRoom;
    public GameObject mainRoom;
    public List<GameObject> rooms;
    
    public int level = 1;

    // Boss
    public float waitTime;
    private bool SpawnedBoss;
    public GameObject boss;

    private void Update()
    {
        if (waitTime <= 0 && SpawnedBoss == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    SpawnedBoss = true;
                }
            }
        }
        else if(waitTime >= 0)
        {
            waitTime -= Time.deltaTime;
        }
    }
}
