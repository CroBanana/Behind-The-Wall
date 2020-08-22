using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTriggerMiguel : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("First conversation")]
    public Dialog[] firstConversation;
    [Header("Second Conversation")]
    public Dialog[] secondConversation;
    public int howManyTimesTalked =0;
    static public int whatDayMiguel;

    public void TriggerDialog()
    {
        if(howManyTimesTalked==0){
            howManyTimesTalked++;
            GameObject.Find("Dialoge panel").GetComponent<DialogManager>().StartDialoge(firstConversation[whatDayMiguel],gameObject);
        }else{
            GameObject.Find("Dialoge panel").GetComponent<DialogManager>().StartDialoge(secondConversation[whatDayMiguel],gameObject);
        }
        
    }
}
