using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingGem : MonoBehaviour
{
    [SerializeField] private int value;
    private bool hasTriggered;
    private GemManager gemManager;

    private void Start()
    {
        gemManager = GemManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            gemManager.ChangeGems(value);
            Destroy(gameObject);
        }
    }


}
