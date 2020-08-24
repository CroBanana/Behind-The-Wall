using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public List<GameObject> signs;
    
    [Tooltip("Signs based on rotation,\n0 - trokut,\n60 - hourglass,\n120 - arrow,\n180 - cube,\n240 - someting,\n300 - circle")]
    public List<int> rotations;
    public int countCorrects;
    RaycastHit hit;
    Ray ray;
    public LayerMask selectedMask;

    public GameObject chest;
    public PlayerInteract PI; 
    public Camera camera;
    private void Start() {
        camera=Camera.main;
        PI= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>();
    }

    private void Update() {
        LeftClick();
    }
    
    public void LeftClick(){
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            Debug.Log("see");
            ray = camera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit,5,selectedMask)){
                if(hit.collider.gameObject.GetComponent<RotatePart>()!=null){
                    hit.collider.gameObject.GetComponent<RotatePart>().rotation +=60;
                    hit.collider.gameObject.GetComponent<RotatePart>().RotateAndWait();
                }
            }
        }
    }

    public void CheckIfCorrect(){
        countCorrects=0;
        Debug.Log("its here");
        
        for (int i=0; i< signs.Count;i++){
            //Debug.Log(signs[i].name+": "+(int) signs[i].transform.eulerAngles.z);
            //Debug.Log("Rotation"+i+": "+(int) rotations[i]);
            if((int) rotations[i] == (int) signs[i].transform.eulerAngles.z){
                //Debug.Log("waduhek");
                countCorrects++;
            }
        }
        if(countCorrects==signs.Count){
            //Debug.Log("Its Correct");
            chest.GetComponent<OpenChest>().OpenChestCorutine();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>().ePressed=false;
            Destroy(gameObject);
        }
    }
}
