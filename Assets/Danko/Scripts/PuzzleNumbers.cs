using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleNumbers : MonoBehaviour
{
    public TextMeshProUGUI text;
    public bool isSolving;
    public string textNumbers;
    public string numbers;
    public string correctNumbers;
    public int sizeOfNumber;
    public int numberOfSpaces;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update() {
        if(isSolving){
            Check();
            EnterNumbers();
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

    void Check(){
        if(numbers.Length==sizeOfNumber){
            if(numbers ==correctNumbers){
                text.color = Color.green;
                isSolving=false;
            }
            else{
                text.color = Color.red;
            }
        }
    }

    void SetText(int number){
        Debug.Log(numbers.Length);
        if(numbers.Length+1>sizeOfNumber){
            textNumbers="";
            numbers=null;
            text.color = Color.black;
        }
        textNumbers += number;
        for(int i =0; i<numberOfSpaces;i++){
            textNumbers+=" ";
        }
        numbers +=number;
        text.text=textNumbers;
    }
}
