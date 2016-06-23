using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour
{
    void Update()
    {
        GameObject target;
        if (UserInput.IsClicked() && UserInput.GetClickedObject() != null)
        {
            target = UserInput.GetClickedObject();
            if (target.gameObject.tag == "Start")
                StartGame();
            else if (target.gameObject.tag == "Exit")
                ExitGame();
        }
    }

    private void StartGame()
    {
        Application.LoadLevel(1);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}