using UnityEngine;

public class AddRooms : MonoBehaviour
{
    private RoomTemplate templates;
    private bool BossRoom;
    

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        templates.rooms.Add(this.gameObject); // добавение комнаты
    }
}
