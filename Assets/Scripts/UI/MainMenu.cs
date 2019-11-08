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

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip creditsVoice;

    [SerializeField]
    private AudioClip controlsVoice;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
        audioSource.PlayOneShot(creditsVoice);
        creditsMenu.SetActive(true);
        controlsMenu.SetActive(false);
        main.SetActive(false);
    }

    public void ControlsButton()
    {
        audioSource.PlayOneShot(controlsVoice);
        creditsMenu.SetActive(false);
        controlsMenu.SetActive(true);
        main.SetActive(false);
    }
}
