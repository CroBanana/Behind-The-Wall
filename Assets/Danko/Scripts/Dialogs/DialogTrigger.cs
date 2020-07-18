using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog firstConversation;
    public Dialog secondConversation;
    public int howManyTimesTalked =0;

    public void TriggerDialog()
    {
        if(howManyTimesTalked==0){
            howManyTimesTalked++;
            GameObject.Find("Dialoge panel").GetComponent<DialogManager>().StartDialoge(firstConversation);
        }else{
            GameObject.Find("Dialoge panel").GetComponent<DialogManager>().StartDialoge(secondConversation);
        }
        
    }
}
