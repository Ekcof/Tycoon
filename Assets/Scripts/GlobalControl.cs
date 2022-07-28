using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    // Global variables
    public int level;
    public int[] levelRating;
    [SerializeField] private int money;
    [SerializeField] private int rating = 50;
    [SerializeField] private int day;

    public static GlobalControl Instance;

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

    public int Money { get { return money; } set { money = value; } }
    public int Rating { get { return rating; } set { rating = value; } }
    public int Day { get { return day; } set { day = value; } }
}
