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
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject Options;


    void Start()
    {
        GetComponent<playerController>();
    }
    public void OnPlay()
    {
        SceneManager.LoadScene("main_testscene");
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnResume()
    {
        PauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnOptions()
    {
        Options.gameObject.SetActive(true);
    }


   public void OnOptionsMenu()
    {
        SceneManager.LoadScene("Options");
    }

    public void OnCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void pauseGame()
    {
        PauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void continueGame()
    {
        PauseMenu.gameObject.SetActive(false);
        Options.gameObject.SetActive(false);
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
