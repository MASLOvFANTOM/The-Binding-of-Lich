using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door

    private RoomTemplate templates;
    private int rand;
    private bool spawned;
    private float waitTime = 4f;
    private int timeSleep = 0;

    private void Awake()
    {
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        Invoke("Spawn", 0.1f);
        //Instantiate(templates.closedRoom, transform.position, quaternion.identity);
    }

    private void Spawn()
    { if (spawned == false)
        {
            if (openingDirection == 1)
            {
                // Need to spawn a room with a BOTTOM door
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT   door
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }

            spawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (timeSleep > 3)
        {
            if (other.CompareTag("SpawnPoint"))
            {
                 if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
                 {
                     Instantiate(templates.closedRoom, transform.position, quaternion.identity);
                     print("gameOBG");
                     Destroy(gameObject);
                 }
                
                // if(other.GetComponent<RoomSpawner>().spawned) Destroy(gameObject);
                //
                // int otherDirection = other.GetComponent<RoomSpawner>().openingDirection;
                // switch (openingDirection)
                // {
                //     case 1:
                //         Instantiate(templates.bottomRooms[otherDirection], transform.position, Quaternion.identity);
                //         print(templates.bottomRooms[otherDirection]);
                //         break;
                //     case 2:
                //         Instantiate(templates.topRooms[otherDirection], transform.position, Quaternion.identity);
                //         print(templates.topRooms[otherDirection]);
                //         break;
                //     case 3:
                //         Instantiate(templates.rightRooms[otherDirection], transform.position, Quaternion.identity);
                //         print(templates.rightRooms[otherDirection]);
                //         break;
                //     case 4:
                //         Instantiate(templates.leftRooms[otherDirection], transform.position, Quaternion.identity);
                //         print(templates.leftRooms[otherDirection]);
                //         break;
                // }
                // Destroy(gameObject);
                // spawned = true;
            }
        }
        else timeSleep++;
    }
}
