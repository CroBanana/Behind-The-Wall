using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleText : MonoBehaviour
{
    public bool isSolving;
    public TextMeshProUGUI text;
    public string textCorrect;
    public string textGuess;
    public string textToShow;
    public int numberOfSpaces;
    public GameObject chest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isSolving){
            Check();
            EnterText();
        }
    }

    void EnterText(){
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if(Input.GetKeyDown(key)){
                string keyString=key.ToString();
                if(keyString.Length==1){
                    Debug.Log(key);
                    SetText(keyString);
                }
            }
        }
    }

    void Check()
    {
        if(textGuess.Length==textCorrect.Length){
            if(textGuess==textCorrect){
                text.color=Color.green;
                isSolving=false;
                if(chest != null){
                    chest.GetComponent<OpenChest>().OpenChestCorutine();
                }
            }
            else{
                text.color=Color.red;
            }
        }
    }

    void SetText(string letter){
        textGuess+=letter;
        if(textGuess.Length>textCorrect.Length){
            textToShow="";
            textGuess=letter;
            text.color=Color.black;
        }
        textToShow+=letter;
        for(int i =0; i<numberOfSpaces;i++){
            textToShow+=" ";
        }
        text.text=textToShow;

    }
}
