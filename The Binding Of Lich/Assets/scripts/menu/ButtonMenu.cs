using UnityEngine;

public class ButtonMenu : MonoBehaviour
{
    public Animator settingsBoard;

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
        settingsBoard.SetTrigger("UnSelect");
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
