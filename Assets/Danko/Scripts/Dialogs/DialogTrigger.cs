using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialoge;

    public void TriggerDialog()
    {
        GameObject.Find("CharacterDialoge").GetComponent<DialogManager>().StartDialoge(dialoge);
    }
}
