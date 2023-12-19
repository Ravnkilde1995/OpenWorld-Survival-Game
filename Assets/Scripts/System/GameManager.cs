using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static bool IsInsideCave = false;
    public Vector3 outsideCaveSpawnPos;
    public Vector3 insideCaveSpawnPos;
    public Vector3 insideCaveSpawnRotation = new Vector3(0f, 0f, 0f); // Adjust the rotation as needed
    public Vector3 outsideCaveSpawnRotation = new Vector3(0f, 0f, 0f); // Adjust the rotation as needed

    private void Awake()
    {
        if (instance != null)
        {
            // Ensure that this GameObject persists across scenes
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // Subscribe to the scene loaded event to handle initialization
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "Forrest")
        {
            if (IsInsideCave)
            {
                Transform player = GameObject.FindGameObjectWithTag("Player").transform;
                player.transform.position = outsideCaveSpawnPos;
                player.transform.rotation = Quaternion.Euler(outsideCaveSpawnRotation);
                IsInsideCave = false;
            }
        }
        else if (scene.name == "Cave")
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            player.transform.position = insideCaveSpawnPos;
            player.transform.rotation = Quaternion.Euler(insideCaveSpawnRotation);
            IsInsideCave = true;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the scene loaded event when this object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}