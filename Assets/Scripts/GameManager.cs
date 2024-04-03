using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static Boolean isGamePaused;
    public GameObject PauseMenu;
    public void OnPlay()
    {
        SceneManager.LoadScene("main_testscene");
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void pauseGame()
    {
        PauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;
        GetComponent<playerController>();
        Cursor.lockState = CursorLockMode.None;
    }

    public void continueGame()
    {
        PauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           if(isGamePaused)
            {
                continueGame();
                Debug.Log("Game should not be paused");
            } else
            {
                pauseGame();
                Debug.Log("Game paused");
            }
        }
    }
}
