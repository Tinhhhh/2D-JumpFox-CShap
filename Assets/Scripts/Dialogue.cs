using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    // Start is called before the first frame update
    public static Dialogue instance;
    public TextMeshProUGUI textDisplay;
    public string[] line;
    public float typingSpeed;
    private int index;

    void Start()
    {
        textDisplay.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textDisplay.text == line[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textDisplay.text = line[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        yield return new WaitForSeconds(1);
        foreach (char letter in line[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void NextLine()
    {
        if (index < line.Length - 1)
        {
            index++;
            textDisplay.text = string.Empty;
            StartCoroutine(Type());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}
