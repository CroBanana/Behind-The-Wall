using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalGateRiddle : MonoBehaviour
{
    public bool isSolving;
    public int correctOnes;
    public List<TextMeshProUGUI> allNumbers;
    public List<string> correctNumbers;
    public TextMeshProUGUI currentlyWriting;
    int currentIndex;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(isSolving){
            if(currentlyWriting!=null){
                if(correctOnes==6){

            }else{
                EnterNumbers();
            }
            }
            
        }
    }
    void EnterNumbers(){
        if(Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0)){
            SetText(0);
        }
        else if(Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)){
            SetText(1);
        }
        else if(Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)){
            SetText(2);
        }
        else if(Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)){
            SetText(3);
        }
        else if(Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4)){
            SetText(4);
        }
        else if(Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5)){
            SetText(5);
        }
        else if(Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6)){
            SetText(6);
        }
        else if(Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7)){
            SetText(7);
        }
        else if(Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8)){
            SetText(8);
        }
        else if(Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9)){
            SetText(9);
        }
    }

    void SetText(int number){
        currentIndex=allNumbers.IndexOf(currentlyWriting);
        if(allNumbers[currentIndex].text.Length>correctNumbers[currentIndex].Length){
            allNumbers[currentIndex].text = number.ToString();
        }else{
            allNumbers[currentIndex].text +=number.ToString();
        }
        if(allNumbers[currentIndex].text.Length==correctNumbers[currentIndex].Length){
            TestIfCorrect();
        }

    }

    public void TestIfCorrect(){
        if(allNumbers[currentIndex].text == correctNumbers[currentIndex]){
            allNumbers[currentIndex].color=Color.green;
            Destroy(allNumbers[currentIndex].gameObject.transform.GetChild(0));
            currentlyWriting=null;
            correctOnes++;
        }else{
            allNumbers[currentIndex].color=Color.red;
        }
    }
}
