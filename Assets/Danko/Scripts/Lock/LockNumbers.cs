using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockNumbers : MonoBehaviour
{
    public bool unlocked=false;
    public string correctCode = "221";
    int numberSelected;
    string code="";
    public float rotationSpeed;
    public Text number;
    public bool check = false;
    public List<Transform> gates;

    public PlayerInteract playerInteract;
    // Start is called before the first frame update
    void Start()
    {
        playerInteract = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>();
    }

    // Update is called once per frame
    void Update()
    {
        if (check == false)
        {
            //Debug.Log(transform.localRotation.z);
            //Debug.Log(transform.localEulerAngles.z);
            //Debug.Log(Mathf.Abs (transform.localEulerAngles.z/36-10));
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                numberSelected = (int)Mathf.Abs(transform.localEulerAngles.z / 36 - 9.9999f);
                //Debug.Log(numberSelected);
                //Debug.Log(transform.localEulerAngles.z / 36);

                SubmitCode();
            }
            float mouseX = Input.GetAxis("Mouse X");
            if (Input.GetKey(KeyCode.Mouse0))
            {
                var rotation = transform.localEulerAngles;
                transform.localRotation = Quaternion.Euler(rotation.x,
                                                           rotation.y,
                                                           rotation.z + Time.deltaTime * mouseX * rotationSpeed);
            }
        }
    }

    void SubmitCode()
    {
        code += numberSelected;
        number.text = code;
        Debug.Log(code);
        if (code.Length == 3)
        {
            Debug.Log(code);
            StartCoroutine(CheckIfCorrect());
        }
    }

    IEnumerator CheckIfCorrect()
    {
        check = true;
        yield return new WaitForSeconds(0.5f);
        if (correctCode == code)
        {
            number.text = "Its Correct, unlocking";
            StartCoroutine(Unlock());
        }
        else
        {
            code = "";
            number.text = "Code Incorrect try again";
        }
        check = false;
    }
    IEnumerator Unlock(){
        unlocked=true;
        yield return new WaitForSeconds(1f);
        if(gates!=null){
            foreach(Transform gate in gates){
                gate.GetComponent<DoorUnlocked>().rotateTo=true;
                Debug.Log("Unlocked");
            }
        }
        yield return new WaitForSeconds (1f);
        playerInteract.canvasInteract.Set_Canvas(false,false,false,false,false);
        Destroy( transform.parent.gameObject);
    }

}
