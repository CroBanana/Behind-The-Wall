using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public GameObject miguel;
    public GameObject pablo;
    public Canvas_interact canvas_Interact;
    public AfterFirst door;
    public void NextScene(){
        gameObject.layer=0;
        Quest.playerSpawn=true;
        Quest.miguelRandomSpawn=true;
        door.LockDoor();
        miguel.GetComponent<DialogTriggerMiguel>().howManyTimesTalked=0;
        int index = SceneManager.GetActiveScene().buildIndex;
        if(index ==1){
            miguel.SetActive(false);
            pablo.SetActive(false);
            Quest.EnablePuzzle();
            Quest.SetNextObjective();
            canvas_Interact.Set_Canvas(false,false,false,false,false,true,false,false);
            SceneManager.LoadScene(index+1);
        }
        if(index==2){
            miguel.SetActive(true);
            //pablo.SetActive(true);
            miguel.GetComponent<MiguelSpawn>().teleportTO();
            Quest.DisablePuzzle();
            Quest.currentActivatedObject++;
            Quest.isItNight=false;
            DialogTriggerMiguel.whatDayMiguel++;
            canvas_Interact.Set_Canvas(false,false,false,false,false,true,false,false);
            SceneManager.LoadScene(index-1);
            Quest.SetNextObjective();
        }
    }
}
