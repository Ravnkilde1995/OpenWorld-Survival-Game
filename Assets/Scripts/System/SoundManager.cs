using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource dropItemSound;
    public AudioSource musicMainMenu;
    public AudioSource musicForrest;
    public AudioSource natureForrest;
    public AudioSource musicCave;
    public AudioSource chopSound;
    public AudioSource toolSwing;
    public AudioSource crafting;
    public AudioSource pickup;
    public AudioSource walking;
    public AudioSource ObjectDestroyed;
    public AudioSource waterDrip;
    public AudioSource enemyChase;
    public AudioSource bigHit;
    public AudioSource gotHit;
    public AudioSource sprinting;
    public AudioSource jump;
    public AudioSource eating;
    public AudioSource drinking;




    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Stop background music
        StopAllBackgroundMusic();

        // Check the scene and play the background music
        if (scene.name == "MainMenu")
        {
            musicMainMenu.Play();
        }
        else if (scene.name == "Forrest")
        {
            musicForrest.Play();
            natureForrest.Play();

        }
        else if (scene.name == "Cave")
        {
            musicCave.Play();
            waterDrip.Play();
            
}
    }

    public void StopAllBackgroundMusic()
    {
        musicMainMenu.Stop();
        musicForrest.Stop();
        musicCave.Stop();
        natureForrest.Stop();
        waterDrip.Stop(); 
        enemyChase.Stop(); 
    }

    //Generic method for playing sound
    public void PlaySound(AudioSource soundToPlay)
    {
        if (!soundToPlay.isPlaying)
        {
            soundToPlay.Play();
        }
    }
}