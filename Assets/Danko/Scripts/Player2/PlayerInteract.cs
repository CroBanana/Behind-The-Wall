using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerInteract : MonoBehaviour
{
    public PlayerMovment2 playerMovement2;
    public GameObject focusedObject;
    public Camera camera;
    public Canvas_interact canvasInteract;
    public RotateViaMouse rotateViaMouse;
    public GameObject noDestroy;

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
    public bool pickingCrop;
    public int crop=0;
    public bool canEBePressed=true;

    public bool firstPerson;

    public float distanceFromObject;
    public float zDistanceFromPlayer;
    public float heightOfNPC;


    //raycast
    // maska za raycast da se vidi s cime igrac moze interactati
    public LayerMask selectedMask;
    private RaycastHit hit;
    private Ray ray;


    //DanijelMenu
    public bool isPaused;
    public GameObject pauseMenu, optionsMenu;
     public GameObject pauseFirstBtn, optionsFirstBtn, optionsClosedBtn;

    void Start()
    {
        noDestroy = GameObject.Find("ItemsPickedUp");
        camera = Camera.main;
        camera.cullingMask = cameraLayersOriginal;
        canvasInteract = GameObject.Find("AllUI").GetComponent<Canvas_interact>();
        rotateViaMouse = GameObject.Find("RotateObjects").GetComponent<RotateViaMouse>();
        playerMovement2 = gameObject.GetComponent<PlayerMovment2>();
        raycastWorks=true;
        if(Quest.playerSpawn){
            transform.position=GameObject.Find("PlayerSpawnPoint").transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(pickingCrop==false){

            CheckIfLookingAtObject();
            ResetIf();
        }

    }

    void CheckIfLookingAtObject()
    {
        if(raycastWorks){
            ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 5, selectedMask))
            {
                if(!hit.collider.gameObject.CompareTag("Untagged")){
                    focusedObject = hit.collider.gameObject;
                    if (ePressed == false )
                    {
                        canvasInteract.Set_Canvas(true, false, false,false,false,true,false,false);
                        //Debug.Log(hit.collider.name);
                        canInteract = true;
                    }
                    E();
                }
            }
            else if (focusedObject != null && ePressed == false)
            {
                canvasInteract.Set_Canvas(false, false, false, false,false,true,false,false);
                canInteract = false;
                if (focusedObject.CompareTag("Lock"))
                {
                    Debug.Log("am here!!!");
                    focusedObject.GetComponentInChildren<LockNumbers>().enabled = false;
                }
                else if(focusedObject.CompareTag("Puzzle")){
                    focusedObject.GetComponentInParent<Puzzle>().enabled = false;
                }else if(focusedObject.CompareTag("Riddle")){
                    try
                    {
                        focusedObject.GetComponentInParent<PuzzleNumbers>().isSolving=false;
                    }
                    catch (System.Exception)
                    {
                        Debug.Log("No puzzle numbers");
                    }
                    try
                    {
                        focusedObject.GetComponentInChildren<PuzzleText>().isSolving=false;
                    }
                    catch (System.Exception)
                    {
                        Debug.Log("No puzzle text");
                    }
                    try
                    {
                        focusedObject.GetComponentInChildren<FinalGateRiddle>().isSolving=false;
                    }
                    catch (System.Exception)
                    {
                        Debug.Log("No puzzle text");
                    }
                }
                focusedObject = null;
                //Debug.Log("Nema canvasa valjda");
            }
        }
        if (ePressed == false)
        {
            
            R();
            I();
            ESC();
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
            canvasInteract.Set_Canvas(false, false, false,false,iPressed,!iPressed,false,false);
            raycastWorks=!iPressed;
        }
    }

    void E()
    {
        //Debug.Log("E Pressed");
        if(canEBePressed){
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
                    Inventory.AddItem(focusedObject);
                    focusedObject.transform.parent= noDestroy.transform;
                    focusedObject.transform.position= noDestroy.transform.position;
                    canvasInteract.Set_Canvas(false, false, false, false,false,true,false,false);
                }
            }
        }


    }

    void ESC(){
        if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused=!isPaused;
                if (isPaused)
                {
                    ActivateMenu();
                }
                else
                {
                    DeactivateMenu();
                }
            }
    }
    //Meni Funkcije

    public void ActivateMenu()
    {
        Debug.Log("Activate menu");
        Time.timeScale = 0;
        AudioListener.pause = true;
        //canvasInteract.PauseMenu.SetActive(true);
        //canvasInteract.QuestCanvas.SetActive(false);
        pauseMenu.SetActive(true);
        isPaused=true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseFirstBtn);
        canvasInteract.Set_Canvas(false,false,false,false,false,false,true,false);

    }

   public  void DeactivateMenu()
    {
        Debug.Log("deactivate menu");
        Time.timeScale = 1;
        AudioListener.pause = false;
        
        //pauseMenu.SetActive(false);
        //optionsMenu.SetActive(false);
        isPaused = false;
        canvasInteract.Set_Canvas(false,false,false,false,false,true,false,false);
    }
    public void MainMenuOpen()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OpenOptions()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsFirstBtn);
        canvasInteract.Set_Canvas(false, false, false, false, false, false, false, true);
    }

    public void CloseOptions()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsClosedBtn);
        canvasInteract.Set_Canvas(false, false, false, false, false, false, true, false);
    }


    public void Interact()
    {
        playerMovement2.anim.SetFloat("Speed", 0);
        playerMovement2.enabled = false;
        //Debug.Log("HERE!#");
        if (focusedObject.CompareTag("Lock"))
        {
            //Debug.Log("HERE!4");
            canvasInteract.Set_Canvas(false, true, false, false,false,false,false,false);
            focusedObject.GetComponentInChildren<LockNumbers>().enabled = true;
        }
        else if (focusedObject.CompareTag("NPC") || focusedObject.CompareTag("Farmer"))
        {
            //Debug.Log("HERE!5");
            canvasInteract.Set_Canvas(false, false, true, false,false,false,false,false);
            focusedObject.GetComponent<EnemyInteract2>().DisableScripts();
            if(focusedObject.name=="Miguel"){
                focusedObject.GetComponent<DialogTriggerMiguel>().TriggerDialog();
            }else if (focusedObject.name=="Pablo Silva"){
                focusedObject.GetComponent<DialogTriggerPablo>().TriggerDialog();
            }else
                focusedObject.GetComponent<DialogTrigger>().TriggerDialog();
        }
        else if(focusedObject.CompareTag("Puzzle")){
            //Debug.Log("WHAT!!!");
            canvasInteract.Set_Canvas(false,false,false, true,false,false,false,false);
            focusedObject.GetComponentInParent<Puzzle>().enabled=true;
        }else if(focusedObject.CompareTag("Riddle")){
            Debug.Log("after  " +Cursor.lockState);
            canvasInteract.Set_Canvas(false,false,false, false,false,false,false,false);
            try
            {
                focusedObject.GetComponentInParent<PuzzleNumbers>().isSolving=true;
            }
            catch (System.Exception)
            {
                //Debug.Log("No puzzle numbers");
            }
            try
            {
                focusedObject.GetComponentInChildren<PuzzleText>().isSolving=true;
                canEBePressed=false;
                Cursor.lockState = CursorLockMode.None;
            }
            catch (System.Exception)
            {
                //Debug.Log("No puzzle text");
            }
            try
            {
                Cursor.lockState = CursorLockMode.None;
                focusedObject.GetComponentInChildren<FinalGateRiddle>().isSolving=true;
            }
            catch (System.Exception)
            {
                Debug.Log("No puzzle text");
            }
        }else if(focusedObject.CompareTag("Item")){
            canvasInteract.Set_Canvas(false,false,false, false,false,false,false,false);
            objectCanBeDestroyed=true;
        }else if (focusedObject.CompareTag("Bed")){
            focusedObject.GetComponent<LoadNextScene>().NextScene();
        }else if(focusedObject.CompareTag("Mrkva")){
            StartCoroutine(PickCrop());
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
            canvasInteract.Set_Canvas(false,false,false,false,false,true,false,false);
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
        else if (focusedObject.CompareTag("NPC") || focusedObject.CompareTag("Farmer"))
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
        Debug.Log("FOCUSED: "+focusedObject.name+"  QUEST TARGET: "+Quest.targetss[Quest.currentObjective]);
        focusedObject.GetComponent<EnemyInteract2>().DialogeEnded();
        if(focusedObject==Quest.targetss[Quest.currentObjective]){
            Quest.SetNextObjective();
        }
    }

    IEnumerator PickCrop(){
        pickingCrop=true;
        yield return new WaitForSeconds(2f);
        Quest.corn.Remove(focusedObject);
        Destroy(focusedObject);
        if(Quest.corn.Count==0){
            Quest.SetNextObjective();
        }else{
            Waypoint.SetWaypoint(Quest.corn[0].transform);
        }
        pickingCrop=false;
        ePressed=false;
    }

    public void ResetEPress(){
        Cursor.lockState = CursorLockMode.Locked;
        canEBePressed=true;
        ePressed=false;
        reset=true;
    }
}
