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
    private Vector2 sprite_size;
    private Vector2 local_sprite_size;
    private Vector3 world_size;

    private void Awake()
    {
        startPosition = transform.position;
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        desire = Random.Range(1, 4);
        SetDesireImage();
        /*
        sprite_size = sprite.sprite.rect.size;
        local_sprite_size = sprite_size / sprite.sprite.pixelsPerUnit;
        world_size = local_sprite_size;
        world_size.x *= transform.lossyScale.x;
        world_size.y *= transform.lossyScale.y;
        */
    }


    public void SetMood(int mood)
    {
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
