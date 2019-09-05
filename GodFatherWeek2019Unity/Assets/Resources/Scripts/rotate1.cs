using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate1 : MonoBehaviour
{
    private float rotationX, rotationY;
    private float rotationZ = 0F;
    private const float TIME_ROTATE = 1f / 100000;
    private Vector3 rotationEuler;
    void Update()
    {
        rotationEuler += Vector3.back * 8 * Time.deltaTime; //increment 30 degrees every second
        transform.rotation = Quaternion.Euler(rotationEuler);
    }
}
