using System;
using System.Collections;
using System.Collections.Generic;
using MazeRunner;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionMenu2;
    public GameStatusController GSC;
     Player currentplayer;

    void Start()
    {
        currentplayer = GSC.GetPlayer();
    }

    public void Continue()
    {
        currentplayer.gameObject.GetComponent<Motion>().enabled = true;
        pauseMenu.gameObject.SetActive(false);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Options()
    {
        pauseMenu.gameObject.SetActive(false);
        optionMenu2.gameObject.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Back()
    {
        optionMenu2.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);

    }



}
