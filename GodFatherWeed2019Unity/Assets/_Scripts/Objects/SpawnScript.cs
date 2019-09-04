using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public Vector2 tempsSpawn;

    public List<Transform> transforms;
    public List<float> rates;

    private float timer;
    private float totalRates;
    private Transform item;


    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(tempsSpawn.x, tempsSpawn.y);
        foreach (var item in rates)
        {
            totalRates += item;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (item == null)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else
            {
                timer = Random.Range(tempsSpawn.x, tempsSpawn.y);
                float random = Random.Range(0, totalRates);
                float toto = 0;
                int i = 0;
                while (toto < random)
                {
                    toto += rates[i];
                    i++;
                }
                item = Instantiate(transforms[i-1]);
            }
        }
    }
}
