using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemManager : MonoBehaviour
{
    public static GemManager instance;
    [SerializeField] private TMP_Text gemsDisplay;

    public int gems;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void OnGUI()
    {
        gemsDisplay.text = gems.ToString();
    }

    public void ChangeGems(int amount)
    {
        gems += amount;
    }


}
