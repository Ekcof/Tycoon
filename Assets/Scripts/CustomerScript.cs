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
    private int desire;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        if (sprite==null) sprite = GetComponent<SpriteRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        desire = Random.Range(1, 4);
        SetDesireImage();
    }

   // public int desire { get { return desire; } set { desire = value; } }

    public void SetMood(int mood)
    {
        sprite.gameObject.SetActive(true);
        switch (mood)
        {
            case 0: sprite.sprite = badMood; break;
            case 1: sprite.sprite = midMood; break;
            default: sprite.sprite = goodMood; break;
        }
    }

    public void SetDesireImage()
    {
        Debug.Log("desire is " + desire);
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
}
