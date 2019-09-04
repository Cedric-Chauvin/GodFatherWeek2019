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

    [HideInInspector]
    public PlayerController myPLayer;
    private float timerUtilisation;
    private Rigidbody rgb;
    private Vector3 initPos;
    private bool isLaunch;


    // Start is called before the first frame update
    void Awake()
    {
        rgb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - initPos).magnitude >= distanceMax && isLaunch)
            Destroy(gameObject);
        if (timerUtilisation > 0)
            timerUtilisation -= Time.deltaTime;
    }

    public bool Utilisation(float dir,PlayerController playerController)
    {
        myPLayer = playerController;
        bool retour = false;
        if (timerUtilisation <= 0)
        {
            timerUtilisation = cooldownUtilisation;
            if (nbUtilisation == 1)
            {
                Lancer(dir);
                retour = true;
            }
            else
            {
                Capacité(dir);
            }
        }
        return (retour);
    }

    public virtual void Lancer(float dir)
    {
        transform.rotation = Quaternion.Euler(0, dir, 0);
        Vector3 temp = transform.InverseTransformDirection(0, 0, speed);
        rgb.velocity = new Vector3(-temp.x, temp.y, temp.z);
        initPos = transform.position;
        isLaunch = true;
    }

    public virtual void Capacité(float dir)
    {
        nbUtilisation--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isLaunch && other.tag == "Player" && myPLayer.transform != other.transform)
        {
            other.GetComponent<PlayerController>().Damage(dommage);
            Destroy(gameObject);
        }

    }
}
