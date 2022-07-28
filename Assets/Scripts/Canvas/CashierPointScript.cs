using UnityEngine;
using UnityEngine.UI;

public class CashierPointScript : MonoBehaviour
{
    [SerializeField] private GameObject receptionPoint;
    [SerializeField] private GameObject cashier;
    private ReceptionPointScript receptionPointScript;
    private GameObject customer;
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
            Transform swipeTransform = canvas.transform.Find("SwipeImage");
            if (swipeTransform == null)
            {
                for (int i = 0; i < canvas.transform.childCount; ++i)
                {
                    Transform child = canvas.transform.GetChild(i);
                    swipeTransform = child.Find("SwipeImage");

                    if (swipeTransform != null)
                    {
                        swipeImage = swipeTransform.gameObject;
                        break;
                    }
                }
            }
            else
            {
                swipeImage = swipeTransform.gameObject;
            }
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

    /// <summary>
    /// Initialize the UI elements for satisfying the customer
    /// </summary>
    /// <param name="customer"></param>
    private void Serve(GameObject customer)
    {
        if (customer != null && swipeImage != null)
        {
            //Time.timeScale = 0;
            Image image = swipeImage.GetComponent<Image>();
            image.enabled = true;
            CanvasResultScript.Instance.SetActiveAllChildren(swipeImage.transform, true);
            TouchInputController.Instance.BlockControl = true;
            customerScript = customer.GetComponent<CustomerScript>();
            if (customerScript != null)
            {
                cardScript = swipeImage.GetComponent<CardScript>();
                if (cardScript != null) cardScript.SetCustomerScript(customerScript);
            }
        }
    }
}