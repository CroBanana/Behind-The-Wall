using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class menuScript : MonoBehaviour
{

    public Canvas_interact canvasInteract;
    public GameObject pauseFirstBtn, optionsFirstBtn, optionsClosedBtn;


    public void MainMenuOpen()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OpenOptions()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstBtn);
        canvasInteract.Set_Canvas(false, false, false, false, false, false, false, true);
    }

    public void CloseOptions()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsClosedBtn);
        canvasInteract.Set_Canvas(false, false, false, false, false, false, true, false);

    }


}
