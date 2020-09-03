using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour

{
    public GameObject optionsMenu, mainMenu, InstructisonsMenu, DevMenu;
    public GameObject fristBtn, firstBtnOptions, firstBtnInstructions, firstBtnDevInfo, 
        BtnBackfromOptions, BtnBackfromInstructions, BtnBackfromDevInfo;

    public void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(fristBtn);
    }
   //Options
    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstBtnOptions);
    }

    public void CloseOptions()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(BtnBackfromOptions);
    }
    //Instructions
    public void OpenInstructions()
    {
        InstructisonsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstBtnInstructions);
    }

    public void CloseInstructions()
    {
        InstructisonsMenu.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(BtnBackfromInstructions);
    }
    //Dev Info
    public void OpenDevInfo()
    {
        DevMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstBtnDevInfo);
    }

    public void CloseDevInfo()
    {
        DevMenu.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(BtnBackfromDevInfo);
    }




    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
       
    }


}
