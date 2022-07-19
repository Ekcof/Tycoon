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
        if (customerScript != null)
        {
            if (customerScript.IsServed)
            {
                Destroy(other.gameObject);
                Debug.Log("Wait for new one!");
                coroutine = WaitForNewCustomer(2f);
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
