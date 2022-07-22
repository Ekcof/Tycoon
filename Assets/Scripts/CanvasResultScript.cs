using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasResultScript : MonoBehaviour
{
    public static CanvasResultScript Instance;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private Text clientsText;
    [SerializeField] private Text currentMoneyText;
    [SerializeField] private Text currentRatingText;
    [SerializeField] private Text dailyText;
    [SerializeField] private Text rentText;
    [SerializeField] private Text gainText;
    [SerializeField] private Text ratingText;
    [SerializeField] private int rating = 50;
    [SerializeField] private int priceTag = 10;
    [SerializeField] private int rent = 50;
    private int clients;
    private int money;
    private int revenue;
    private int ratingChange;
    private int totalGain;

    #region Singleton
    private void Start()
    {
        Instance = this;
    }
    #endregion

    private void Awake()
    {
        clients = 0;
        revenue = 0;
        ratingChange = 0;
        totalGain = 0;
    }

    /// <summary>
    /// Take the money and rating after customer left cashier
    /// </summary>
    /// <param name="mood"></param>
    public void CustomerLeave(int mood)
    {
        ++clients;
        switch (mood)
        {
            case 0: ChangeRatingAndMoney(-priceTag, -priceTag / 2); break;
            case 1: ChangeRatingAndMoney(0, priceTag / 2); break;
            case 2: ChangeRatingAndMoney(priceTag / 2, priceTag); break;
        }
        RefreshCanvasData();
    }

    /// <summary>
    /// Sum up the results of the work day
    /// </summary>
    public void Results()
    {
        endPanel.SetActive(true);
        Time.timeScale = 0;
        totalGain = revenue - rent;
        clientsText.text += clients.ToString();

        string dailyString = revenue.ToString();
        if (revenue >= 0)
        {
            dailyString = "$" + dailyString;
        }
        else
        {
            dailyString = "-$" + dailyString;
        }

        dailyText.text += revenue.ToString();
        ratingText.text += rating.ToString();
        rentText.text += rent.ToString();

        string totalgainString = totalGain.ToString();
        if (totalGain >= 0)
        {
            totalgainString = "$" + totalgainString;
        }
        else{
            totalgainString = "-$" + totalgainString;
        }
        gainText.text += totalgainString;
    }

    /// <summary>
    /// Set active all children gameobjects
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="value"></param>
    public void SetActiveAllChildren(Transform transform, bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);

            SetActiveAllChildren(child, value);
        }
    }

    /// <summary>
    /// add or subtract rating and money after client has been served
    /// </summary>
    /// <param name="addRating"></param>
    /// <param name="addMoney"></param>
    private void ChangeRatingAndMoney(int addRating, int addMoney)
    {
        money += addMoney;
        revenue += addMoney;
        rating += addRating;
        if (rating < 0) rating = 0;
    }

    /// <summary>
    /// Refresh the digits on the top of the screen
    /// </summary>
    private void RefreshCanvasData()
    {
        currentMoneyText.text = "$" + money.ToString();
        currentRatingText.text = rating.ToString() + "%";
    }

}
