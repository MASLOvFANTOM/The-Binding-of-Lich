using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [Header("Level parametrs")]
    public int maxLevel;
    public int level;
    
    [Header("Level information object")]
    public Animator levelInformationAnimator;
    public Text levelInformation;

    private void Start()
    {
        int countFinishGame = PlayerPrefs.GetInt("count finish game");
        maxLevel = (countFinishGame + 4);
        maxLevel = Mathf.Clamp(maxLevel, 4, 7);
        level = maxLevel;
        ShowLevelInformation(level);
    }

    private void ShowLevelInformation(int level)
    {
        levelInformation.text = "Level: " + level;
        levelInformationAnimator.SetTrigger("show-hide");
        print("Level: " + level);
    }
}
