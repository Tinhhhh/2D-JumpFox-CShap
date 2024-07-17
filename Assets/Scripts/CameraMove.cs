using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
     [SerializeField] private float speed;
    // Start is called before the first frame update
    public bool isMoving = true;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 1033)
        {
            Camera(isMoving);
        }

    }

    public void Camera(bool value)
    {
        if (value)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }

    }
}
