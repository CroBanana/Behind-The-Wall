using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public PlayerMovment2 playerMovement2;
    public GameObject focusedObject;
    public Camera camera;
    public Canvas_interact canvasInteract;
    public RotateViaMouse rotateViaMouse;

    // maska koja vidi sve
    public LayerMask cameraLayersOriginal;

    //maska kada interacta s necim
    public LayerMask cameraLayers;

    public bool ePressed;
    public bool canInteract;
    public bool talkTriggered;
    public bool reset;
    public bool iPressed;
    public bool raycastWorks=true;
    public bool objectCanBeDestroyed;

    public bool firstPerson;

    public float distanceFromObject;
    public float zDistanceFromPlayer;
    public float heightOfNPC;


    //raycast
    // maska za raycast da se vidi s cime igrac moze interactati
    public LayerMask selectedMask;
    private RaycastHit hit;
    private Ray ray;

    void Start()
    {
        camera = Camera.main;
        camera.cullingMask = cameraLayersOriginal;
        canvasInteract = GameObject.Find("AllUI").GetComponent<Canvas_interact>();
        rotateViaMouse = GameObject.Find("RotateObjects").GetComponent<RotateViaMouse>();
        playerMovement2 = gameObject.GetComponent<PlayerMovment2>();
        raycastWorks=true;

    }

    // Update is called once per frame
    void Update()
    {
        CheckIfLookingAtObject();
        ResetIf();
    }

    void CheckIfLookingAtObject()
    {
        if(raycastWorks){
            ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 5, selectedMask))
            {
                focusedObject = hit.collider.gameObject;
                if (ePressed == false)
                {
                    canvasInteract.Set_Canvas(true, false, false,false,false,true);
                    Debug.Log(hit.collider.name);
                    canInteract = true;
                }
                E();
            }
            else if (focusedObject != null && ePressed == false)
            {
                canvasInteract.Set_Canvas(false, false, false, false,false,true);
                canInteract = false;
                if (focusedObject.CompareTag("Lock"))
                {
                    Debug.Log("am here!!!");
                    focusedObject.GetComponentInChildren<LockNumbers>().enabled = false;
                }
                else if(focusedObject.CompareTag("Puzzle")){
                    focusedObject.GetComponentInParent<Puzzle>().enabled = false;
                }else if(focusedObject.CompareTag("Riddle")){
                    focusedObject.GetComponent<PuzzleNumbers>().isSolving = false;
                }
                focusedObject = null;
                Debug.Log("Nema canvasa valjda");
            }
        }
        if (ePressed == false)
        {
            R();
            I();
        }
    }

    void R()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetCamera();
        }
    }
    void I(){
        
        if(Input.GetKeyDown(KeyCode.I)){
            iPressed=!iPressed;
            canvasInteract.Set_Canvas(false, false, false,false,iPressed,!iPressed);
            raycastWorks=!iPressed;
        }
        
    }

    void E()
    {
        //Debug.Log("E Pressed");
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            ePressed = !ePressed;
            if (ePressed)
            {
                Interact();
            }
        }

        else if (ePressed == false && reset)
        {
            playerMovement2.enabled = true;
            camera.cullingMask = cameraLayersOriginal;
            ResetCameraPosition();
            reset = false;
            rotateViaMouse.GetComponent<RotateViaMouse>().enabled = true;
            if(focusedObject.CompareTag("Item") && objectCanBeDestroyed){
                    Debug.Log("Getting destroyed");
                    Destroy(focusedObject);
                    canvasInteract.Set_Canvas(false, false, false, false,false,true);
                }
        }
    }

    public void Interact()
    {
        playerMovement2.anim.SetFloat("Speed", 0);
        playerMovement2.enabled = false;
        Debug.Log("HERE!#");
        if (focusedObject.CompareTag("Lock"))
        {
            Debug.Log("HERE!4");
            canvasInteract.Set_Canvas(false, true, false, false,false,false);
            focusedObject.GetComponentInChildren<LockNumbers>().enabled = true;
        }
        else if (focusedObject.CompareTag("NPC"))
        {
            Debug.Log("HERE!5");
            canvasInteract.Set_Canvas(false, false, true, false,false,false);
            focusedObject.GetComponent<EnemyInteract2>().DisableScripts();
            focusedObject.GetComponent<DialogTrigger>().TriggerDialog();
        }
        else if(focusedObject.CompareTag("Puzzle")){
            Debug.Log("WHAT!!!");
            canvasInteract.Set_Canvas(false,false,false, true,false,false);
            focusedObject.GetComponentInParent<Puzzle>().enabled=true;
        }else if(focusedObject.CompareTag("Riddle")){
            canvasInteract.Set_Canvas(false,false,false, false,false,false);
            focusedObject.GetComponentInParent<PuzzleNumbers>().isSolving=true;
        }else if(focusedObject.CompareTag("Item")){
            canvasInteract.Set_Canvas(false,false,false, false,false,false);
            Inventory.instance.AddItem(focusedObject);
            objectCanBeDestroyed=true;
        }
        FocusOnAnObject();

        
        reset = true;
    }

    void ResetIf()
    {
        if (focusedObject == null && reset)
        {
            playerMovement2.enabled = true;
            camera.cullingMask = cameraLayersOriginal;
            ResetCameraPosition();
            reset = false;
            rotateViaMouse.GetComponent<RotateViaMouse>().enabled = true;
        }
    }

    void FocusOnAnObject()
    {
        camera.cullingMask = cameraLayers;
        rotateViaMouse.GetComponent<RotateViaMouse>().enabled = false;

        if (focusedObject.CompareTag("Lock"))
        {
            camera.transform.position = focusedObject.transform.position - focusedObject.transform.right * distanceFromObject;

            camera.transform.LookAt(focusedObject.transform);

            if (focusedObject.GetComponentInChildren<LockNumbers>().unlocked)
            {
                Debug.Log("HERE!!!!");
                reset = true;
                ePressed = false;
                focusedObject = null;
            }

        }
        else if (focusedObject.CompareTag("NPC"))
        {
            Transform body = focusedObject.transform.Find("Body");
            camera.transform.position = body.position + body.forward * distanceFromObject;
            Vector3 lookAtPoint = body.position+Vector3.up*heightOfNPC;
            camera.transform.position +=Vector3.up*heightOfNPC;
            camera.transform.LookAt(lookAtPoint);
            //camera.transform.position = focusedObject.transform.position+ Vector3.up * heightOfNPC;
            //camera.transform.position+=Vector3.forward*distanceFromObject;
            //camera.transform.rotation = new Quaternion(0,focusedObject.transform.rotation.y,0,focusedObject.transform.rotation.w);
            //cameraObject.transform.LookAt(new Vector3(focusedObject.transform.position.x,
                                                    //focusedObject.transform.position.y,
                                                    //focusedObject.transform.position.z)+ Vector3.up * heightOfNPC);
        }else if(focusedObject.CompareTag("Puzzle")){
            Debug.Log("HERE!2");
            Transform puzzleParent=focusedObject.transform.parent;
            camera.transform.position = puzzleParent.position - puzzleParent.right * distanceFromObject;

            camera.transform.LookAt(puzzleParent);
        }else if(focusedObject.CompareTag("Riddle") || focusedObject.CompareTag("Item")){
            camera.transform.position = focusedObject.transform.position + focusedObject.transform.up *(distanceFromObject*2);
            camera.transform.LookAt(focusedObject.transform.position);
        }


    }

    void ResetCameraPosition()
    {
        camera.transform.localPosition = new Vector3(0, 0, 0);
        camera.transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (!firstPerson)
        {
            camera.transform.localPosition = new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y, zDistanceFromPlayer);
        }
        else
        {
            camera.transform.localPosition = new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y, 0f);
        }
    }

    // sets camera when R is pressed
    void SetCamera()
    {
        if (firstPerson)
        {
            camera.transform.localPosition = new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y, zDistanceFromPlayer);
            firstPerson = false;
            playerMovement2.firstPerson = firstPerson;
        }
        else
        {
            camera.transform.localPosition = new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y, 0f);
            firstPerson = true;
            playerMovement2.firstPerson = firstPerson;
        }
    }

    public void EndConversation()
    {
        playerMovement2.enabled = true;
        ePressed = false;
        focusedObject.GetComponent<EnemyInteract2>().DialogeEnded();
    }
}
