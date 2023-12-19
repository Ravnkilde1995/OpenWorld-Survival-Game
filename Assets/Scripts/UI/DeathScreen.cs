using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    
    public GameObject deathScreen;
    public GameObject deathScreenUI;
    public static bool isPaused;
    


    // Start is called before the first frame update
    void Start()
    {
        deathScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerStats.Instance.currentHealth <= 0)
        {
            GameOver();
        }
        
        
    }

    public void GameOver()
    {
        deathScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        SoundManager.Instance.StopAllBackgroundMusic();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
