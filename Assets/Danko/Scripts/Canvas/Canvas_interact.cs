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
        Cursor.lockState = CursorLockMode.Locked;

    }


    public void Set_Canvas(bool interact, bool lokot, bool dialoge,bool puzzle,bool inventory,bool quest,bool menu){
        interactCanvas.SetActive(interact);
        lockCanvas.SetActive(lokot);
        characterDialog.SetActive(dialoge);
        puzzleCanvas.SetActive(puzzle);
        inventoryCanvas.SetActive(inventory);
        QuestCanvas.SetActive(quest);

        if(lokot || dialoge || puzzle || inventory || menu){
            Cursor.lockState=CursorLockMode.None;
        }else{
            Cursor.lockState=CursorLockMode.Locked;
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
