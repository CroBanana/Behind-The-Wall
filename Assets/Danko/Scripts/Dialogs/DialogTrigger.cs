using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialoge;

    public void TriggerDialog()
    {
        GameObject.Find("Dialoge panel").GetComponent<DialogManager>().StartDialoge(dialoge);
    }
}
