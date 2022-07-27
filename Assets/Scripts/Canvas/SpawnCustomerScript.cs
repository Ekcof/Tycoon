using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomerScript : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int CustomerLimit;
    [SerializeField] private GameObject receptionObject;
    [SerializeField] private float interval = 2f;
    private CustomerScript customerScript;
    private float newTime;
    private bool isCustomerSpawned;


    private void Awake()
    {
        CreateCustomer();
    }

    private void Update()
    {
        if (!isCustomerSpawned)
        {
            if (newTime < Time.time)
            {
                CreateCustomer();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        customerScript = other.GetComponent<CustomerScript>();
        if (customerScript != null)
        {
            if (customerScript.IsServed)
            {
                newTime = Time.time + interval;
                   isCustomerSpawned = false;
                Destroy(other.gameObject);

            }
        }
    }


    private void CreateCustomer()
    {
        isCustomerSpawned = true;
        GameObject instantiatedCustomer = Instantiate(prefab, transform.position, transform.rotation);
        CustomerScript customerScript = instantiatedCustomer.GetComponent<CustomerScript>();
        if (customerScript != null)
        {
            customerScript.SetDestination(receptionObject.transform.position);
        }
    }

}
