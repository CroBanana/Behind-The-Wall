using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockLastMap : MonoBehaviour
{
    public Canvas_interact canvas_Interact;
    public void FinalScene(){
        Quest.SetNextObjective();
        canvas_Interact.Set_Canvas(false,false,false,false,false,true,false,false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
