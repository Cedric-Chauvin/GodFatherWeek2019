using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : MonoBehaviour
{

    public float speed;
    public float distanceMax;
    public float poids;
    public float dommage;
    public int nbUtilisation;
    public float cooldownPickup;
    public float cooldownUtilisation;


    private float timerUtilisation;
    private Rigidbody rgb;
    private Vector3 initPos;


    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - initPos).magnitude >= distanceMax)
            Destroy(gameObject);
        if (timerUtilisation > 0)
            timerUtilisation -= Time.deltaTime;
    }

    public void Utilisation(float dir)
    {
        if (timerUtilisation <= 0)
        {
            timerUtilisation = cooldownUtilisation;
            if (nbUtilisation == 1)
                Lancer(dir);
            else
            {
                Capacité();
            }
        }
    }

    public void Lancer(float dir)
    {
        transform.rotation = Quaternion.Euler(0, dir, 0);
        rgb.velocity = transform.InverseTransformDirection(0,0,speed);
        initPos = transform.position;
    }

    public virtual void Capacité()
    {
        nbUtilisation--;
    }
}
