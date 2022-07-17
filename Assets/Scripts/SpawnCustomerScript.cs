using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomerScript : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int CustomerLimit;
    [SerializeField] private GameObject receptionObject;
    private CustomerScript customerScript;

    // Start is called before the first frame update
    private void Awake()
    {
        CreateCustomer();
    }

    private void OnTriggerEnter(Collider other)
    {
        customerScript = other.GetComponent<CustomerScript>();
        if(customerScript != null)
        {
            if (customerScript.IsServed)
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void CreateCustomer()
    {
        GameObject instantiatedCustomer = Instantiate(prefab, transform.position, transform.rotation);
        CustomerScript customerScript = instantiatedCustomer.GetComponent<CustomerScript>();
        if (customerScript != null)
        {
            customerScript.SetDestination(receptionObject.transform.position);
        }
    }
}
