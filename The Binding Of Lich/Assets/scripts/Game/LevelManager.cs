using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [Header("Level parametrs")]
    public int startLevel;
    public int level;
    
    [Header("Level information object")]
    public Animator levelInformationAnimator;
    public Text levelInformation;

    private void Start()
    {
        int countFinishGame = PlayerPrefs.GetInt("count finish game"); //
        startLevel = (countFinishGame + 4); // Стартовый уровень
        startLevel = Mathf.Clamp(startLevel, 4, 7); // ограничить стартовый уровень
        level = startLevel; // текущий уровень
        ShowLevelInformation(level); // Показать информацию о уровне
    }

    private void ShowLevelInformation(int level) // Показать информацию о уровне 
    {
        levelInformation.text = "Level: " + level; // Текст
        levelInformationAnimator.SetTrigger("show-hide"); // Анимация
    }
}
