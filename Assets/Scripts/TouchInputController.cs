using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavigationController))]
public class TouchInputController : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private float maxDubbleTapTime = 0.5f;

    private NavigationController navigationController;
    private int tapCount;
    private float newTime;
    private Vector3 touchPosition;
    private Vector3 touchToWorldPosition;


    void Start()
    {
        navigationController = GetComponent<NavigationController>();
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                tapCount += 1;
            }

            if (tapCount == 1)
            {
                if (newTime == 0) newTime = Time.time + maxDubbleTapTime;
                touchPosition = new Vector3(touch.position.x, touch.position.y, 0);
            }
            else if (tapCount == 2 && Time.time <= newTime)
            {
                touchToWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                destination.position = new Vector3 (touchToWorldPosition.x, touchToWorldPosition.y,0);
                print("Dubble tap");
                ReserParameters(0);
            }else if(Time.time > newTime && newTime != 0)
            {
                ReserParameters(1);
            }
        }
    }

    private void ReserParameters(int newTapCount)
    {
        tapCount = newTapCount;
        newTime = 0;
    }
}
