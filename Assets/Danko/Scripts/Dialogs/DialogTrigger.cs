using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog firstConversation;
    public Dialog secondConversation;
    public int howManyTimesTalked =0;
    static public int whatDay;

    public void TriggerDialog()
    {
        if(howManyTimesTalked==0){
            howManyTimesTalked++;
            GameObject.Find("Dialoge panel").GetComponent<DialogManager>().StartDialoge(firstConversation,gameObject);
        }else{
            GameObject.Find("Dialoge panel").GetComponent<DialogManager>().StartDialoge(secondConversation,gameObject);
        }
        
    }
}
