using System.Collections;
using UnityEngine;
using TMPro;

public class Win : MonoBehaviour
{
    public static Dialogue instance;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject newGameBtn;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject dialogue;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has reached the finish line!");
            PlayerMovement.isInputEnabled = false;
            winScreen.SetActive(true);
            PlayerAnimation.instance.SetIdleAnimation(false);
            dialogue.SetActive(true);
            // newGameBtn.SetActive(true);
            
        }
    }

    // Start is called before the first frame update
   
}
