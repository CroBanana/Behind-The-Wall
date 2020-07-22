using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_interact : MonoBehaviour
{
    public GameObject interactCanvas;
    public GameObject lockCanvas;
    public GameObject characterDialog;
    public GameObject puzzleCanvas;
    // Start is called before the first frame update
    private void Awake() {
        interactCanvas = transform.Find("Interact panel").gameObject;
        interactCanvas.SetActive(false);
        lockCanvas = transform.Find("Lock panel").gameObject;
        lockCanvas.SetActive(false);
        characterDialog = transform.Find("Dialoge panel").gameObject;
        characterDialog.SetActive(false);
        puzzleCanvas = transform.Find("Puzzle Panel").gameObject;
        puzzleCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

    }


    public void Set_Canvas(bool interact, bool lokot, bool dialoge,bool puzzle){
        interactCanvas.SetActive(interact);
        lockCanvas.SetActive(lokot);
        characterDialog.SetActive(dialoge);
        puzzleCanvas.SetActive(puzzle);

        if(lokot || dialoge || puzzle){
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
