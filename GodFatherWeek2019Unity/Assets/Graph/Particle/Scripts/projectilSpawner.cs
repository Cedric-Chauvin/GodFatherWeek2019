using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectilSpawner : MonoBehaviour
{
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();
    private GameObject effectToSpawn;
    private float timeToFire = 0;

    void Start(){
        effectToSpawn = vfx [0];
}


    void Update(){
        if(timeToFire > 0)
        {
            timeToFire -= Time.deltaTime;
        }
        if (Input.GetMouseButton (0) && timeToFire <= 0){
            timeToFire = 1 / 1;// effectToSpawn.GetComponent<projectilmove>().fireRate
           ; SpawnVFX();
        }
    }
    void SpawnVFX() { 
    GameObject vfx;

    if (firePoint !=null){
        vfx = Instantiate (effectToSpawn, firePoint.transform.position, Quaternion.identity);
       } else {
         Debug.Log ("No fire point");

        }
    }

   }

