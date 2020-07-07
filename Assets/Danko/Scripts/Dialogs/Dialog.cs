using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public string[] names;

    [TextArea(1,5)]
    public string[] sentences;
}
