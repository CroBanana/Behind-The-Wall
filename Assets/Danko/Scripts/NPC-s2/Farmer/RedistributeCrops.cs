using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedistributeCrops : MonoBehaviour
{
    public List<GameObject> farmers;
    public GameObject[] temp;
    public List<GameObject> crops;
    public List<GameObject> cropsToFarmer;
    public GameObject corn;
    public int cropPerFarmer;
    // Start is called before the first frame update
    private void Awake() {

        foreach (Transform child in corn.transform)
        {
            crops.Add(child.gameObject);
        }

        temp=GameObject.FindGameObjectsWithTag("Farmer");
        for(int c=0; c<temp.Length;c++){
            farmers.Add(temp[c]);
        }
        temp=null;
        Debug.Log("Crops: "+ crops.Count);
        Debug.Log("Farmers: "+ farmers.Count);
        cropPerFarmer=crops.Count/farmers.Count;
        Debug.Log(cropPerFarmer);

        /*
        foreach (var farmer in farmers)
        {
            cropsToFarmer.Clear();
            for(int i =0;i<cropPerFarmer;i++){
                cropsToFarmer.Add(crops[0]);
                Debug.Log(crops[0]);
                crops.Remove(crops[0]);
            }
            Debug.Log("Current size of crops:" + crops.Count);
            farmer.GetComponent<Farmer>().crops = cropsToFarmer;

            continue;
        }*/
        int f=0;
        foreach (var crop in crops)
        {
            farmers[f].GetComponent<Farmer>().crops.Add(crop);
            f++;
            if(f==15){
                f=0;
            }
        }

    }

}
