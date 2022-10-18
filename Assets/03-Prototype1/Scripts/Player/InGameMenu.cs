using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject inGameMenu;
    public GameObject gameoverMenu;
    public GameObject winMenu;
    public bool isOver = false;

    private void Start()
    {

    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void OnESC()
    {
        if (!isOver)
        {
            inGameMenu.SetActive(!inGameMenu.active);
        }
    }

    public void GameOver()
    {
        isOver = true;
        gameoverMenu.SetActive(true);
    }

    public void Win()
    {
        isOver = true;
        winMenu.SetActive(true);
    }
}
