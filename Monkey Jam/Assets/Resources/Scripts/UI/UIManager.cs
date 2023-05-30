using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool isPaused;

    [SerializeField] private GameObject hudCanvas = null;
    [SerializeField] private GameObject pauseCanvas  = null;
    [SerializeField] private GameObject endCanvas = null;

    private CameraController camController = null;
    private PlayerStats stats = null;

    private void Start()
    {
        GetReferences();
        SetActiveHud(true);
    }

    private void Update()
    {
        if (!stats.IsDead())
        {
            if (Input.GetKeyUp(KeyCode.Escape) && !isPaused)
                SetActivePause(true);
            else if (Input.GetKeyUp(KeyCode.Escape) && isPaused)
                SetActivePause(false);
        }
        
    }

    public void SetActiveHud(bool state)
    {
        hudCanvas.SetActive(state);
        endCanvas.SetActive(!state);

        if (!stats.IsDead())
            pauseCanvas.SetActive(!state);
    }

    public void SetActivePause(bool state)
    {
        pauseCanvas.SetActive(state);
        hudCanvas.SetActive(!state);

        Time.timeScale = state ? 0 : 1; //timeScale = 0 when state and 1 when !state
        if (state)
            camController.UnlockCursor();
        else 
            camController.LockCursor();
        isPaused = state;

    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void GetReferences()
    {
        camController = GetComponentInChildren<CameraController>();
        stats = GetComponent<PlayerStats>();
    }

}
