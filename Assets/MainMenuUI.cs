using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OnMainMenuStart()
    {
        SceneManager.LoadScene(1);
    }

    public void OnMainMenuAbout()
    {
        // Display my info and portfolio website
        Application.OpenURL("www.mingbinportfolio.com");
    }

    public void OnGameRestart()
    {
        SceneManager.LoadScene(1);
    }
}
