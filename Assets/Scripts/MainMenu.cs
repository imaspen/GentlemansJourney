using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject main;

    [SerializeField]
    private GameObject creditsMenu;

    [SerializeField]
    private GameObject controlsMenu;


    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        creditsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        main.SetActive(true);
    }

    public void CreditsButton()
    {
        creditsMenu.SetActive(true);
        controlsMenu.SetActive(false);
        main.SetActive(false);
    }

    public void ControlsButton()
    {
        creditsMenu.SetActive(false);
        controlsMenu.SetActive(true);
        main.SetActive(false);
    }
}
