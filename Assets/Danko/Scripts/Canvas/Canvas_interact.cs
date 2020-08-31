using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_interact : MonoBehaviour
{
    public GameObject interactCanvas;
    public GameObject lockCanvas;
    public GameObject characterDialog;
    public GameObject puzzleCanvas;
    public GameObject inventoryCanvas;
    public GameObject QuestCanvas;
    public GameObject PauseMenu;
    public GameObject OptionsMenu;
    // Start is called before the first frame update
    private void Start() {
        interactCanvas = transform.Find("Interact panel").gameObject;
        interactCanvas.SetActive(false);
        lockCanvas = transform.Find("Lock panel").gameObject;
        lockCanvas.SetActive(false);
        characterDialog = transform.Find("Dialoge panel").gameObject;
        characterDialog.SetActive(false);
        puzzleCanvas = transform.Find("Puzzle Panel").gameObject;
        puzzleCanvas.SetActive(false);
        inventoryCanvas= transform.Find("Inventory Panel").gameObject;
        inventoryCanvas.SetActive(false);
        QuestCanvas = transform.Find("Quest Panel").gameObject;
        QuestCanvas.SetActive(true);
        PauseMenu = transform.Find("pauseMenu").gameObject;
        PauseMenu.SetActive(false);
        OptionsMenu = transform.Find("optionsMenu").gameObject;
        OptionsMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible=false;

    }


    public void Set_Canvas(bool interact, bool lokot, bool dialoge,bool puzzle,bool inventory,bool quest,bool menu,bool options){
        interactCanvas.SetActive(interact);
        lockCanvas.SetActive(lokot);
        characterDialog.SetActive(dialoge);
        puzzleCanvas.SetActive(puzzle);
        inventoryCanvas.SetActive(inventory);
        QuestCanvas.SetActive(quest);
        PauseMenu.SetActive(menu);
        OptionsMenu.SetActive(options);

        if(lokot || dialoge || puzzle || inventory || menu || options){
            Cursor.lockState=CursorLockMode.None;
            Cursor.visible=true;
        }else{
            Cursor.lockState=CursorLockMode.Locked;
            Cursor.visible=false;
        }

    }

    public void Set_Canvas_Bot(bool interact, bool lokot, bool dialoge){
        interactCanvas.SetActive(interact);
        lockCanvas.SetActive(lokot);
        characterDialog.SetActive(dialoge);

        if(lokot || dialoge){
            Cursor.lockState=CursorLockMode.None;
        }else{
            Cursor.lockState=CursorLockMode.Locked;
        }
    }

}
