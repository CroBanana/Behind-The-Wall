using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTriggerPablo : MonoBehaviour
{
    [Header("First conversation")]
    public Dialog[] firstConversation;
    [Header("Second Conversation")]
    public Dialog[] secondConversation;
    public int howManyTimesTalked =0;
    static public int whatDayPablo;

    public void TriggerDialog()
    {
        if(howManyTimesTalked==0){
            howManyTimesTalked++;
            GameObject.Find("Dialoge panel").GetComponent<DialogManager>().StartDialoge(firstConversation[whatDayPablo],gameObject);
        }else{
            GameObject.Find("Dialoge panel").GetComponent<DialogManager>().StartDialoge(secondConversation[whatDayPablo],gameObject);
        }
        
    }
}
