using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class Cell : MonoBehaviour
{
    public string tag;
    private static string selectObjectTag;
    public bool characterOpen;
    public Sprite[] grilleState = new Sprite[2]; //0-close, 1-open
    public Image grilleImage;
    public Text statsTextDescription;
    public Text statsTextHealth;
    public Text statsTextDamage;
    public Animator StatsBoardAnimator;
    
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

        //проверка на наличие персонажа
        for (int i = 0; i < _playerDataOpenCell.listOpenedCharacter.Count; i++)
        {
            if (_playerDataOpenCell.listOpenedCharacter[i] == tag)
            {
                characterOpen = true;
                grilleImage.sprite = grilleState[1];
            }
            else if(_playerDataOpenCell.listOpenedCharacter[i] != tag & !characterOpen)
            {
                print(gameObject.name);
                grilleImage.sprite = grilleState[0];
            }
        }
    }

    public void SavePlayerDataOpenCell() // сохранение изменений
    {
        File.WriteAllText(pathToPlayerDataOpenCell, JsonUtility.ToJson(_playerDataOpenCell));
    }

    private void OnApplicationQuit()
    {
        SavePlayerDataOpenCell();
    }

    public void MouseDown()
    {
        StartCoroutine(MouseDownCoorotine());
    }

    IEnumerator MouseDownCoorotine()
    {
        //анимация
        if (selectObjectTag != null)
        {
            StatsBoardAnimator.SetTrigger("ReCall");
        }
        else
        {
            StatsBoardAnimator.SetTrigger("Call");
        }

        if(selectObjectTag != null) yield return new WaitForSeconds(0.3f);
        
        //Смена текста параметров
        if (characterOpen)
        {
            selectObjectTag = tag;
            switch (tag)
            {
                case "knight":
                    statsTextDescription.text = _playerDataOpenCell.Knight[2].ToString();
                    statsTextHealth.text = _playerDataOpenCell.Knight[0].ToString();
                    statsTextDamage.text = _playerDataOpenCell.Knight[1].ToString();
                    break;
                case "wizard":
                    statsTextDescription.text = _playerDataOpenCell.Wizard[2].ToString();
                    statsTextHealth.text = _playerDataOpenCell.Wizard[0].ToString();
                    statsTextDamage.text = _playerDataOpenCell.Wizard[1].ToString();
                    break;
            }
        }
        StopCoroutine(MouseDownCoorotine());
    }
}
[Serializable]
public class PlayerDataOpenCell
{
    public List<string> listOpenedCharacter = new List<string>{"knight"};
    public List<object> Knight = new List<object>() {3, 1, "ЭТО ГЕРОЙ ВСЕМ ГЕРОЙМ ГЕРОЙ. ВЗМАХ МЕЧА ЕГО РУБИТ КАМНИ, А ТОПОТ ЕГО ГЛУШИТ ВРАГОВ." };
    public List<object> Wizard = new List<object>() {2, 1, "THIS IS WIZARD. Он очень хлипок, но силён умом!" };
}