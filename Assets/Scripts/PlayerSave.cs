using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        SaveGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveGame()
    {
        StartCoroutine(SavePosition());
    }


    IEnumerator SavePosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            Vector3 playerPos = Player.transform.position;
            PlayerPrefs.SetFloat("PlayerX", playerPos.x);
            PlayerPrefs.SetFloat("PlayerY", playerPos.y);
            PlayerPrefs.SetFloat("PlayerZ", playerPos.z);
            PlayerPrefs.SetInt("Gem", GemManager.instance.gems);
            Debug.Log("Player position saved");
        }

    }


}
