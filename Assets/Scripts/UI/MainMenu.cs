using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Forrest");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeSettings()
    {
        Debug.Log("Comming Soon!");
    }
   
}
