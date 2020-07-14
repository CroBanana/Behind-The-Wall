using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Farmer : MonoBehaviour
{
    public Animator anim;
    public List<GameObject> crops;
    public GameObject crop;
    public GameObject chill;

    public NavMeshPath path;
    public bool pathCalculated;
    public bool pickingCrop;
    public bool picked;
    public int cropInLIst=0;
    public int picked__seeded=0;

    public Vector3 startSize;
    public Vector3 replantSize;
    public Vector3 zero;

    public float rotateSpeed;
    public float distanceNew;
    public float distanceOld;
    public float distanceToCrop;
    bool stuckDistanceCorutine;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        path = new NavMeshPath();
        /*
        GameObject[] corns = GameObject.FindGameObjectsWithTag("Kukuruz");
        Debug.Log(corns.Length);
        foreach(GameObject corn in corns){
            crops.Add(corn);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceToCrop_AndGatherCrop();
        if(stuckDistanceCorutine==false){
            StartCoroutine(CheckIfStuck());
            stuckDistanceCorutine=true;
        }
        //Debug.Log(Vector3.Distance(transform.position,crop.transform.position));
        DrawPath();
    }

    void CheckDistanceToCrop_AndGatherCrop()
    {
        //ako crop ne postoji uzme prvog iz liste
        if (crop == null)
        {
            crop = crops[0];
        }

        //kalkulira put prema biljki
        if (pathCalculated == false)
        {
            NavMesh.CalculatePath(transform.position, crop.transform.position, NavMesh.AllAreas, path);
            pathCalculated=true;
            //Debug.Log("test");
        }

        //ako je udaljenost veca od neke vrijednosti npc ce se kretati do biljke. 
        // ako dođe u blizinu pokrene se corutine koji pobere biljku nakon nekoliko sekundi
        if (Vector3.Distance(transform.position, crop.transform.position) > distanceToCrop)
        {
            anim.SetFloat("Speed", 1f);
            RotateToCrop();
        }
        else
        {
            if(pickingCrop == false && picked__seeded!=2){
                StopAllCoroutines();
                StartCoroutine(PickCrop());
            }
        }
    }

    void DrawPath(){
        for (int i = 0; i < path.corners.Length - 1; i++)
            {

                Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
            }
    }

    IEnumerator PickCrop(){
        anim.SetFloat("Speed", 0f);
        pickingCrop=true;
        yield return new WaitForSeconds(0.5f);
        Pick__Replant__Scale();
        pickingCrop=false;
    }

    IEnumerator CheckIfStuck(){
        while(true){
            //Debug.Log("Cheching if stuck");
            distanceOld=Vector3.Distance(path.corners[1],transform.position);
            yield return new WaitForSeconds(10);
            distanceNew=Vector3.Distance(path.corners[1],transform.position);
            if(distanceNew == distanceOld){
                pathCalculated=false;
            }
        }
    }

    void Pick__Replant__Scale(){
        if(crop.transform.localScale.x==startSize.x){
            crop.transform.localScale =  zero;
        }
        else if(crop.transform.localScale.x==zero.x){
            crop.transform.localScale=replantSize;
        }
        FindNewCrop();
    }

    void FindNewCrop()
    {
        cropInLIst++;
        try
        {
            crop = crops[cropInLIst];
        }
        catch
        {
            picked__seeded++;
            //Debug.Log("is it fully picked?    "+ picked);
            if(picked__seeded==2){
                StartCoroutine(RelaxYouHaveDoneYourJob());
            }
            else{
                cropInLIst = 0;
                crop = crops[cropInLIst];
            }
        }
        pathCalculated = false;
    }

    IEnumerator RelaxYouHaveDoneYourJob(){
        //Debug.Log("relaxing");
        cropInLIst=0;
        crop=chill;
        yield return new WaitUntil(() => Vector3.Distance(transform.position,crop.transform.position) < distanceToCrop);
        //Debug.Log("Waiting");
        anim.SetFloat("Speed", 0f);
        yield return new WaitForSeconds(10);
        picked__seeded=0;
        crop=null;
    }



    void RotateToCrop()
    {
        if(Vector3.Distance (path.corners[1],transform.position)<0.2f){
            pathCalculated=false;
        }
        Vector3 cornerPosition= new Vector3(path.corners[1].x-transform.position.x,
                                            0,
                                            path.corners[1].z-transform.position.z);
        //Debug.Log(target.name);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, cornerPosition, rotateSpeed * Time.deltaTime,0f);
        transform.rotation= Quaternion.LookRotation(newDirection);
    }
}
