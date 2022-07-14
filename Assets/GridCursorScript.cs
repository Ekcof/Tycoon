using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCursorScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject UnableImageGameObject;
    [SerializeField] private GameObject GreenFrameImageGameObject;
    private string currentTag;

    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if(Physics2D.OverlapPoint(transform.position)!=null) currentTag = Physics2D.OverlapPoint(transform.position).tag;
        switch (currentTag)
        {
            case "Walls": SetImage(UnableImageGameObject); break;
            default: SetImage(GreenFrameImageGameObject); break;
        }

    }

    private void SetImage(GameObject imageGO)
    {
        foreach (Transform child in transform)
        {
            if (child != imageGO.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        imageGO.SetActive(true);
    }

}
