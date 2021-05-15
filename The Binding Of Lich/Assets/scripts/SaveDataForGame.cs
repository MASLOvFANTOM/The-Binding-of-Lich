using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveDataForGame : MonoBehaviour
{
    public string characterTag;
    public GameObject[] characterList;
    public Scene scene;
    private bool characterCreated;

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }
    public void InstantiatePlayer()
    {
        print(characterTag);
        for (int i = 0; i < characterList.Length; i++)
        {
            if (characterList[i].name == characterTag)
            {
                Instantiate(characterList[i], transform.position, Quaternion.identity);
                break;
            }
        }
    }

    private void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "game" && !characterCreated)
        {
            InstantiatePlayer();
            characterCreated = true;
        }
    }

    
}
