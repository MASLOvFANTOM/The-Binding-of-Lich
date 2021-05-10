using UnityEngine;

public class MainRoomCenter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            Destroy(other.gameObject);
        }
    }
    private void Start()
    {
        Destroy(gameObject, 4f);
    }
}