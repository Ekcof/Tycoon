using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] private Sprite goodMood;
    [SerializeField] private Sprite midMood;
    [SerializeField] private Sprite badMood;
    [SerializeField] private Sprite[] desireImages;
    private bool isServed;
    private Vector2 startPosition;
    private int desire;
    private NavMeshAgent navMeshAgent;
    private int mood = 1;

    private void Awake()
    {
        startPosition = transform.position;
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        desire = Random.Range(1, 4);
        SetDesireImage();
    }

    /// <summary>
    /// Set the mood after the service
    /// </summary>
    /// <param name="newMood"></param>
    public void SetMood(int newMood)
    {
        mood = newMood;
        isServed = true;
        Time.timeScale = 1;
        sprite.gameObject.SetActive(true);
        switch (mood)
        {
            case 0: sprite.sprite = badMood; break;
            case 1: sprite.sprite = midMood; break;
            default: sprite.sprite = goodMood; break;
        }
        SetDestination(startPosition);
        CanvasResultScript.Instance.CustomerLeave(mood);
    }

    public void SetDesireImage()
    {
        sprite.gameObject.SetActive(true);
        if (desire <= desireImages.Length)
        {
            sprite.sprite = desireImages[desire - 1];
        }
    }

    public void SetDestination(Vector3 pos)
    {
        navMeshAgent.destination = pos;
    }

    public int GetDesire()
    {
        return desire;
    }

    public bool IsServed { get { return isServed; } set { isServed = value; } }

}
