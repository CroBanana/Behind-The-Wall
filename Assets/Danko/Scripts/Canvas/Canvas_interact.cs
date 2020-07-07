using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_interact : MonoBehaviour
{
    public GameObject interactCanvas;
    public GameObject lockCanvas;
    public GameObject characterDialog;
    // Start is called before the first frame update
    private void Awake() {
        interactCanvas = transform.Find("Interact panel").gameObject;
        interactCanvas.SetActive(false);
        lockCanvas = transform.Find("Lock panel").gameObject;
        lockCanvas.SetActive(false);
        characterDialog = transform.Find("Dialoge panel").gameObject;
        characterDialog.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

    }


    public void Set_Canvas(bool interact, bool lokot, bool dialoge){
        interactCanvas.SetActive(interact);
        lockCanvas.SetActive(lokot);
        characterDialog.SetActive(dialoge);

        if(lokot || dialoge){
            Cursor.lockState=CursorLockMode.None;
        }else{
            Cursor.lockState=CursorLockMode.Locked;
        }

        // using canvas group
        /*
        if(interact){
            interactCanvas.alpha=1;
            Debug.Log("TREBALO BI RADIT REEEEEEEEEEE");
        }else{
            interactCanvas.alpha=0;
        }

        if(lokot){
            lockCanvas.alpha=1;
        }else{
            lockCanvas.alpha=0;
        }

        if(dialoge){
            characterDialog.alpha=1;
        }else{
            characterDialog.alpha=0;
        }
        */
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
