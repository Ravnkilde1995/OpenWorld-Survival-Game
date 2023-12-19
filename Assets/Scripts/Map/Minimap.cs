using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;
    public static Minimap instance;

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


    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }

}
