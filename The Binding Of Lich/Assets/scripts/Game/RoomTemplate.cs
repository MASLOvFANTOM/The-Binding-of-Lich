using System.Collections.Generic;
using UnityEngine;

public class RoomTemplate : MonoBehaviour
{
    // Rooms
    [Header("Списки")]
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject[] monsters;
    
    [Header("Другое")]
    public GameObject closedRoom;
    public GameObject mainRoom;
    public List<GameObject> rooms;
}
