using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasInput : MonoBehaviour
{
    [SerializeField] private GameObject card;
    [SerializeField] private AnimationClip idleClip;
    [SerializeField] private AnimationClip rightClip;
    [SerializeField] private AnimationClip leftClip;
    private CardScript cardScript;
    private Animator animator;
    private int touchPath;
    private Touch touch;
    private Vector2 swipeMove = new Vector3(0, 0, 0);
    private Vector3 startPos;
    private Vector3 endPos;
    private int direction;
    private float animPercent;
    public bool IsSwiped { get; set; }
    private AnimationClip clip;
    private Image image;


    private void Awake()
    {
        cardScript = card.GetComponent<CardScript>();
        animator = card.GetComponent<Animator>();
        image = card.GetComponent<Image>();
        touchPath = Screen.width / 8;
    }

    private void Update()
    {

        AnimateCard();
            if (Input.touchCount > 0 && !IsSwiped)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) startPos = touch.position;
                GetSwipe();
            }
            else if (Input.touchCount == 0)
            {
                IsSwiped = false;
                ResetPosition();
            }

    }


    private void AnimateCard()
    {
        animator.SetInteger("Swipe", direction);
        if (direction != 0) SetAnimationFrame();
    }

    private void GetSwipe()
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                endPos = startPos; break;
            default:
                endPos = touch.position;
                swipeMove = endPos - startPos; break;
        }

        animPercent = Mathf.Abs(swipeMove.x / touchPath);
        direction = (int)(swipeMove.x / Mathf.Abs(swipeMove.x));
        float x = Mathf.Abs(swipeMove.x);
        float y = Mathf.Abs(swipeMove.y);
        animator.speed = 0;
        if (x > touchPath && y < 100)
        {
            if (direction == -1) CancelCard();
            if (direction == 1) AcceptCard();
        }

    }

    private void ResetPosition()
    {
        animPercent = 0;
        endPos = new Vector3(0, 0, 0);
        startPos = new Vector3(0, 0, 0);
        swipeMove = new Vector2(0, 0);
        direction = 0;
        animator.speed = 1;
        animator.Play(idleClip.name, 0, 0);
    }

    private void CancelCard()
    {
        ResetPosition();
        cardScript.CancelCard();
        IsSwiped = false;
    }

    private void AcceptCard()
    {
        IsSwiped = true;
        ResetPosition();
        cardScript.AcceptCard();
        IsSwiped = false;
        //card.SetActive(false);
        image.enabled = false;
        CanvasResultScript.Instance.SetActiveAllChildren(image.transform, false);
    }

    private void SetAnimationFrame()
    {

        if (direction == 1)
        {
            clip = rightClip;
        }
        else
        {
            clip = leftClip;
        }
        float clipLength = clip.length;

        float newTime = Mathf.Lerp(0, clipLength, animPercent);
        animator.Play(clip.name, 0, newTime);
        animator.speed = 1;

    }
}
