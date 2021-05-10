using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private RoomTemplate templates;
    private float timeReloaad = 2f;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 10.8f, -10);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 10.8f, -10);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + 19, transform.position.y, -10);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - 19, transform.position.y, -10);
        }
        else if (Input.GetKey(KeyCode.R))
        {
            if (timeReloaad <= 0)
            {
                for (int i = 0; i < templates.rooms.Count; i++)
                {
                    Destroy(templates.rooms[i].gameObject);
                }

                templates.rooms.Clear();
                Instantiate(templates.mainRoom, new Vector3(0, 0, 0), quaternion.identity);
                transform.position = new Vector3(0, 0, -10);
                timeReloaad = 2f;
            }
            else timeReloaad -= Time.deltaTime;
        }

        if (templates.rooms.Count > 10)
        {
            for (int i = 0; i < templates.rooms.Count; i++)
            {
                Destroy(templates.rooms[i].gameObject);
            }

            templates.rooms.Clear();
            Instantiate(templates.mainRoom, new Vector3(0, 0, 0), quaternion.identity);
            transform.position = new Vector3(0, 0, -10);
            timeReloaad = 2f;
        }
    }
}
