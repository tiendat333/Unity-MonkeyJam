using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject controlMenu;
    [SerializeField] private GameObject creditMenu;

    private void Start()
    {
        ActivateMainMenu(true);
    }    

    public void Play()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void ActivateMainMenu(bool state)
    {
        mainMenu.SetActive(state);
        optionsMenu.SetActive(!state);
        controlMenu.SetActive(!state);
        creditMenu.SetActive(!state);
    }

    public void ActivateOptionsMenu()
    {
        if (optionsMenu.active == false)
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
            controlMenu.SetActive(false);
            creditMenu.SetActive(false);
        }
        else
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
            controlMenu.SetActive(false);
            creditMenu.SetActive(false);
        }        
    }

    public void ActivateControlMenu(bool state)
    {
        if (controlMenu.active == false)
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            controlMenu.SetActive(true);
            creditMenu.SetActive(false);
        }
        else
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
            controlMenu.SetActive(false);
            creditMenu.SetActive(false);
        }
    }

    public void ActivateCreditMenu(bool state)
    {
        if (creditMenu.active == false)
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            controlMenu.SetActive(false);
            creditMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
            controlMenu.SetActive(false);
            creditMenu.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
