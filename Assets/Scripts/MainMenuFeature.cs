using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuFeature : MonoBehaviour
{
    public void StartNewGame()
    {
        PlayerMovement.isInputEnabled = false;
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(1);
        PlayerMovement.isInputEnabled = false;
        PlayerPrefs.SetInt("isContinue", 1);

    }

    public void Exit()
    {
        Application.Quit();
    }
}
