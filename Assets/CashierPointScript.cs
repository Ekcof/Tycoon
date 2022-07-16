using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashierPointScript : MonoBehaviour
{
    [SerializeField] private GameObject receptionPoint;
    [SerializeField] private GameObject cashier;
    private ReceptionPointScript receptionPointScript;
    private GameObject customer;
    private float distanceToCashier;


    private void Awake()
    {
        receptionPointScript = receptionPoint.GetComponent<ReceptionPointScript>();
    }

    private void Update()
    {
        distanceToCashier = Vector3.Distance(cashier.transform.position, transform.position);
        if (distanceToCashier < 0.5f)
        {
            if (receptionPointScript.CustomerIsHere)
            {
                Serve(receptionPointScript.GetCustomer());
            }
        }
    }

    private void Serve(GameObject customer)
    {
        if (customer != null)
        {
            Debug.Log("notNull");
        }
    }
}