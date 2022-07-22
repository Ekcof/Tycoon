using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public int level;
    public int[] levelRating;

    public static GlobalControl Instance;

    //TODO: Save data after every new day
    // Make a loading scene with _MainGameLogic object
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
