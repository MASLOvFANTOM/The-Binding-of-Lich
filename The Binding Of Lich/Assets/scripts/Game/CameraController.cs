using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private RoomTemplate templates;
    private float timeReload = 1f;
    [SerializeField]
    private float reloading;
    private bool canReloding;
    public Animator LevelReload;

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

        if (timeReload <= 1)
        {
            if (Input.GetKey(KeyCode.R))
            {
                if (timeReload <= 0)
                {
                    LevelReload.SetTrigger("Start");
                    canReloding = true;
                    reloading = 1f;
                    for (int i = 0; i < templates.rooms.Count; i++)
                    {
                        Destroy(templates.rooms[i].gameObject);
                    }

                    templates.rooms.Clear();
                    Instantiate(templates.mainRoom, new Vector3(0, 0, 0), quaternion.identity);
                    transform.position = new Vector3(0, 0, -10);
                    timeReload = 2f;
                }
                else timeReload -= Time.deltaTime;
            }
        }
        else if (timeReload >= 1) timeReload -= Time.deltaTime;
        

        if (templates.rooms.Count > 10)
        {
            for (int i = 0; i < templates.rooms.Count; i++)
            {
                Destroy(templates.rooms[i].gameObject);
            }

            templates.rooms.Clear();
            Instantiate(templates.mainRoom, new Vector3(0, 0, 0), quaternion.identity);
            transform.position = new Vector3(0, 0, -10);
            timeReload = 2f;
            reloading = 1f;
        }
        else
        {
            if (reloading <= 0f && canReloding)
            {
                print("random");
                LevelReload.SetTrigger("End");
                canReloding = false;
            }
            else
            {
                if(canReloding) reloading -= Time.deltaTime;
            }
        }
        
    }
}
