using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Camera[] cameras;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {

        cameras[1].gameObject.SetActive(cameras[1]);
        cameras[0].gameObject.SetActive(cameras[0]);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SwitchCamera(Camera activeCamera)
    {
        // cameras[0].gameObject
    }

}
