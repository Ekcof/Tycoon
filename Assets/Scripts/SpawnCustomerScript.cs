using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomerScript : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int CustomerLimit;
    [SerializeField] private GameObject receptionObject;
    private CustomerScript customerScript;
    private IEnumerator coroutine;


    private void Awake()
    {
        CreateCustomer();
        coroutine = WaitForNewCustomer(2f);
    }


    private void OnTriggerEnter(Collider other)
    {
        customerScript = other.GetComponent<CustomerScript>();
        Debug.Log("Found customer!");
        if (customerScript != null)
        {
            Debug.Log("Has script!");
            if (customerScript.IsServed)
            {
                Debug.Log("Destroy!");
                Destroy(other.gameObject);
                StartCoroutine(coroutine);
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


    private IEnumerator WaitForNewCustomer(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        CreateCustomer();
    }
}
