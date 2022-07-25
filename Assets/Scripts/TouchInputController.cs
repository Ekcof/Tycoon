using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavigationController))]
public class TouchInputController : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private float maxDubbleTapTime = 0.5f;
    public static TouchInputController Instance;

    private NavigationController navigationController;
    private int tapCount;
    private float newTime;
    private Vector3 touchPosition;
    private Vector3 touchToWorldPosition;
    private Grid grid;
    private bool blockControl;

    #region Singleton
    private void Awake()
    {
        Instance = this;
        navigationController = GetComponent<NavigationController>();
        grid = GameObject.Find("Grid").GetComponent<Grid>();
    }
    #endregion

    private void Update()
    {
        if (!blockControl)
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
                    Vector3 destinationVector = new Vector3(touchToWorldPosition.x, touchToWorldPosition.y, 0);
                    Collider2D collider2D = Physics2D.OverlapBox(destinationVector, new Vector2(0.2f, 0.2f), 0);
                    if (collider2D != null)
                    {
                        if (collider2D.tag != "Walls")
                        {
                            destination.position = destinationVector;
                            GetGridScript.Instance.SetObjectToGridCenter(destination);
                        }
                    }

                    ResetParameters(0);
                }
                else if (Time.time > newTime && newTime != 0)
                {
                    ResetParameters(1);
                }
            }
        }
    }

    public bool BlockControl { get { return blockControl; } set { blockControl = value; } }

    private void ResetParameters(int newTapCount)
    {
        tapCount = newTapCount;
        newTime = 0;
    }
}
