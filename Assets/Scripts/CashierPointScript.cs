using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierPointScript : MonoBehaviour
{
    [SerializeField] private GameObject receptionPoint;
    [SerializeField] private GameObject cashier;
    private ReceptionPointScript receptionPointScript;
    private GameObject customer;
    private GameObject newCustomer;
    private float distanceToCashier;
    private bool isServing;
    private GameObject canvas;
    private GameObject swipeImage;
    private CardScript cardScript;
    private CustomerScript customerScript;


    private void Awake()
    {
        receptionPointScript = receptionPoint.GetComponent<ReceptionPointScript>();
        canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            swipeImage = canvas.transform.Find("SwipeImage").gameObject;
        }
    }


    private void OnTriggerStay(Collider collision)
    {
        if (customer == null)
        {
            if (receptionPointScript.CustomerIsHere)
            {
                customer = receptionPointScript.GetCustomer();
                Serve(customer);
            }
        }
    }


    private void Serve(GameObject customer)
    {
        if (customer != null && swipeImage != null)
        {
            Time.timeScale = 0;
            swipeImage.SetActive(true);
            customerScript = customer.GetComponent<CustomerScript>();
            if (customerScript != null)
            {
                cardScript = swipeImage.GetComponent<CardScript>();
                if (cardScript != null) cardScript.SetCustomerScript(customerScript);
            }
        }
        //isServing = false;
    }
}