using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform cam;
    private float height;

    private void Start()
    {
        cam = Camera.main.transform;
        height = cam.transform.position.z;
    }


    private void Update()
    {
        cam.position = new Vector3 (transform.position.x, transform.position.y, height);
    }
}
