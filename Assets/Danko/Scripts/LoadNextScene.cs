using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public void NextScene(){
        Quest.currentObjective++;
        int index = SceneManager.GetActiveScene().buildIndex;
        if(index ==1){
            Quest.isItNight=true;
            SceneManager.LoadScene(index+1);
        }
        if(index==2){
            Quest.currentActivatedObject++;
            Quest.isItNight=false;
            DialogTriggerMiguel.whatDayMiguel++;
            SceneManager.LoadScene(index-1);
        }
    }
}
