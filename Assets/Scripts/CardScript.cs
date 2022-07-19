using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    [SerializeField] Text numberText;
    [SerializeField] private CanvasInput canvasInput;
    private int cardNumber;
    private CustomerScript customerScript;

    private int prevNumber;

    private void Awake()
    {
        canvasInput = GetComponentInParent<CanvasInput>();
        RandomizeCardNumber();
    }

    /// <summary>
    /// Set customer's behaviour script
    /// </summary>
    /// <param name="newCustomerScript"></param>
    public void SetCustomerScript(CustomerScript newCustomerScript)
    {
        customerScript = newCustomerScript;
    }

    /// <summary>
    /// Get or set card number
    /// </summary>
    public int CardNumber
    {
        get { return cardNumber; }
        set { cardNumber = value; }
    }

    /// <summary>
    /// Cancel the current card
    /// </summary>
    public void CancelCard()
    {
        prevNumber = cardNumber;
        RandomizeCardNumber();
    }

    /// <summary>
    /// Accept the current card
    /// </summary>
    public void AcceptCard()
    {
        if (customerScript != null)
        {
            if (customerScript.GetDesire() == cardNumber)
            {
                customerScript.SetMood(2);
            }
            else
            {
                customerScript.SetMood(0);
            }
        }
    }

    /// <summary>
    /// Randomize the card number
    /// </summary>
    public void RandomizeCardNumber()
    {
        do
        {
            cardNumber = Random.Range(1, 4);
        } while (cardNumber == prevNumber);
        numberText.text = cardNumber.ToString();
    }
}
