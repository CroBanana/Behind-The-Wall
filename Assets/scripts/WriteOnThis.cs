using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WriteOnThis : MonoBehaviour
{
    public FinalGateRiddle finalGate;
    public TextMeshProUGUI writeHere;

    private void Start() {
        finalGate = gameObject.GetComponentInParent<FinalGateRiddle>();
        writeHere= GetComponent<TextMeshProUGUI>();
    }
    public void EnableWritingOnThis(){
        finalGate.currentlyWriting=writeHere;
    }

}
