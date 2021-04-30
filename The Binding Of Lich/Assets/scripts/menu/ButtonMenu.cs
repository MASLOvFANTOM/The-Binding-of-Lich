using UnityEngine;

public class ButtonMenu : MonoBehaviour
{
    public Animator settingsBoard;
    public void Play()
    {
        //call menu change character
        //call game scene
    }

    public void Settigs()
    {
        settingsBoard.SetTrigger("select");
    }
    public void UnSettigs()
    {
        settingsBoard.SetTrigger("UnSelect");
    }

    public void Credits()
    {
        //call scene with Credits
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Collection()
    {
        //call scene with collection
    }
}
