using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    [SerializeField] Text numberText;
    [SerializeField] private CanvasInput canvasInput;
    private int cardNumber;

    private int prevNumber;

    private void Awake()
    {
        canvasInput = GetComponentInParent<CanvasInput>();
        RandomizeCardNumber();
    }

    public int CardNumber
    {
        get { return cardNumber; }
        set { cardNumber = value; }
    }

    public void CancelCard()
    {
        prevNumber = cardNumber;
        RandomizeCardNumber();
    }

    public void AssetCard()
    {

    }

    public void RandomizeCardNumber()
    {
        do
        {
            cardNumber = Random.Range(1, 4);
        } while (cardNumber == prevNumber);
        numberText.text = cardNumber.ToString();
    }
}
