using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Sprite[] grilleState = new Sprite[2];
    public Image grilleSpriteRenderer;
    public string tag;
    public Text statsTextDescription;
    public Text statsTextHealth;
    public Text statsTextDamage;
    
    //Json files
    private PlayerDataOpenCell _playerDataOpenCell= new PlayerDataOpenCell();
    private string pathToPlayerDataOpenCell;

    private void Start()
    {
        //задаём путь
        pathToPlayerDataOpenCell = Path.Combine(Application.dataPath, "PlayerDataOpenCell.json");
        if (File.Exists(pathToPlayerDataOpenCell)) // если файл найден
        {
            _playerDataOpenCell = JsonUtility.FromJson<PlayerDataOpenCell>(File.ReadAllText(pathToPlayerDataOpenCell));
        }
        else // если файл не найден
        {
            SavePlayerDataOpenCell();
        }

        if (tag == "knight")
        {
            grilleSpriteRenderer.sprite = grilleState[1];
            // statsTextDescription.text =
        }
    }

    public void SavePlayerDataOpenCell() // сохранение изменений
    {
        File.WriteAllText(pathToPlayerDataOpenCell, JsonUtility.ToJson(_playerDataOpenCell));
    }
    
}
[Serializable]
public class PlayerDataOpenCell
{
    public string description, heath, damage;
    // public Array Knight =[3, 2, "dsadasd"]
}