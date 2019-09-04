using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Update()
    {
        // Look towards camera script (Optional)
        transform.LookAt(Camera.main.transform);
        transform.forward = -transform.forward;
    }
}
