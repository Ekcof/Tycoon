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

    public void Results()
    {
        endPanel.SetActive(true);
        Time.timeScale = 0;
        totalGain = revenue - rent;
        clientsText.text += clients.ToString();
        dailyText.text += revenue.ToString();
        ratingText.text += rating.ToString();
        rentText.text += rent.ToString();
        gainText.text += totalGain.ToString();
    }

    public void SetActiveAllChildren(Transform transform, bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);

            SetActiveAllChildren(child, value);
        }
    }

    private void ChangeRatingAndMoney(int addRating, int addMoney)
    {
        money += addMoney;
        revenue += addMoney;
        rating += addRating;
        if (rating < 0) rating = 0;
    }

    private void RefreshCanvasData()
    {
        currentMoneyText.text = "$" + money.ToString();
        currentRatingText.text = rating.ToString() + "%";
    }

}
