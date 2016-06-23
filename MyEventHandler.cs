using UnityEngine;
using System.Collections;

public class MyEventHandler : MonoBehaviour
{

    public void StartGame()
    {
        Application.LoadLevel(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
