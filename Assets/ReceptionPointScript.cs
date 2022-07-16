using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionPointScript : MonoBehaviour
{
    private GameObject customer;
    private bool customerIsHere = false;
    private float distanceToCustomer;


    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, GetGridScript.Instance.GetZ());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision!");
        if (!customerIsHere)
        {
            if (collision.tag == "Customer")
            {
                customerIsHere = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (customerIsHere)
        {
            if (collision.tag == "Customer")
            {
                customerIsHere = false;
            }
        }
    }

    public GameObject GetCustomer()
    {
        return customer;
    }

    /// <summary>
    /// Get if there is a customer on the spot
    /// </summary>
    public bool CustomerIsHere { get { return customerIsHere; } set { customerIsHere = value; } }

}
