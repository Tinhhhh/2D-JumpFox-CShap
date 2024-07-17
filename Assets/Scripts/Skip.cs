using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skip : MonoBehaviour
{
   
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement.instance.transform.position = new Vector3(2, 174, 0);
            // newGameBtn.SetActive(true);
            
        }
    }
}
