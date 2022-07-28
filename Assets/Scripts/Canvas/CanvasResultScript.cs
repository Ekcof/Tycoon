using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasResultScript : MonoBehaviour
{
    public static CanvasResultScript Instance;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private Text dayText;
    [SerializeField] private Text clientsText;
    [SerializeField] private Text currentMoneyText;
    [SerializeField] private Text currentRatingText;
    [SerializeField] private Text dailyText;
    [SerializeField] private Text rentText;
    [SerializeField] private Text gainText;
    [SerializeField] private Text ratingText;
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private int rating = 50;
    [SerializeField] private int priceTag = 10;
    [SerializeField] private int rent = 50;
    private CanvasInput canvasInput;
    private int clients;
    private int money;
    private int revenue;
    private int ratingChange;
    private int totalGain;
    private int day;

    #region Singleton
    private void Awake()
    {
        canvasInput = GetComponent<CanvasInput>();
        Time.timeScale = 1;
        Instance = this;
        clients = 0;
        revenue = 0;
        ratingChange = 0;
        totalGain = 0;
        if (GlobalControl.Instance)
        {
            rating = GlobalControl.Instance.Rating;
            money = GlobalControl.Instance.Money;
            day = GlobalControl.Instance.Day;
        }
        RefreshCanvasData();
        if (day > 1)
        {
            hintPanel.SetActive(false);
        }
    }
    #endregion

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
        if (canvasInput != null) canvasInput.DisableCardControl();
        endPanel.SetActive(true);
        Time.timeScale = 0;
        ratingChange = rating - GlobalControl.Instance.Rating;
        totalGain = revenue - rent;
        dayText.text += day.ToString();
        clientsText.text += clients.ToString();
        dailyText.text += DollarToString(revenue);
        ratingText.text += ratingChange.ToString();
        rentText.text += rent.ToString();
        gainText.text += DollarToString(totalGain);

        ++day;
        money -= rent;
        
        GlobalControl.Instance.Rating = rating;
        GlobalControl.Instance.Money = money;
        GlobalControl.Instance.Day = day;
        RefreshCanvasData();
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

    public void RestartDay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Add or subtract rating and money after client left
    /// </summary>
    /// <param name="addRating"></param>
    /// <param name="addMoney"></param>
    private void ChangeRatingAndMoney(int addRating, int addMoney)
    {
        money += addMoney;
        revenue += addMoney;
        rating += addRating;
        if (rating < 0) rating = 0;
        if (rating > 100) rating = 100;
    }

    /// <summary>
    /// Refresh the digits on the top of the screen
    /// </summary>
    private void RefreshCanvasData()
    {
        currentMoneyText.text = "$" + money.ToString();
        currentRatingText.text = rating.ToString() + "%";
    }

    /// <summary>
    /// Convert number of dollars to string
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    private string DollarToString (int number)
    {
        string newString = "$" + number.ToString();
        if (number < 0)
        {
            newString = "-$" + Mathf.Abs(number);
        }
        return newString;
    }

}
