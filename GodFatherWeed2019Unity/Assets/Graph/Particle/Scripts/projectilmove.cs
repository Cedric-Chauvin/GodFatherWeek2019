using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectilmove : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;
    void Start()
    {
        if (muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;
            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
                Destroy(muzzleVFX, psMuzzle.main.duration);
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
 

                
        }
    }

    
    void Update()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("No Speed");
        }
    }
 void OnTriggerEnter (Collider other) {
        if (!other.CompareTag("la famille"))
        {
            speed = 0;
    
            
               
                Vector3 pos = gameObject.transform.position;
            
            if (hitPrefab != null)
            {
                var hitVFX = Instantiate(hitPrefab, transform.position, Quaternion.identity);
                hitVFX.transform.forward = gameObject.transform.forward;
                var pshit = hitVFX.GetComponent<ParticleSystem>();
                if (pshit != null)
                    Destroy(hitVFX, pshit.main.duration);
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }

                //var projectil = Instantiate(hitPrefab,pos,hitPrefab.transform.rotation);

            }
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.tag !="la famille")
        {
            speed = 0;



            Vector3 pos = gameObject.transform.position;

            if (hitPrefab != null)
            {
                var hitVFX = Instantiate(hitPrefab, transform.position, Quaternion.identity);
                hitVFX.transform.forward = gameObject.transform.forward;
                var pshit = hitVFX.GetComponent<ParticleSystem>();
                if (pshit != null)
                    Destroy(hitVFX, pshit.main.duration);
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }

                //var projectil = Instantiate(hitPrefab,pos,hitPrefab.transform.rotation);

            }
            Destroy(gameObject);
        }
    }
}

