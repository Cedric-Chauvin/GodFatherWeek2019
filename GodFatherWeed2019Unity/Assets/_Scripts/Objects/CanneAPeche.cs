using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanneAPeche : ObjectBase
{

    public Transform ligne;

    public override void Lancer(float dir)
    {
        Transform instance = Instantiate(ligne);
        instance.GetComponent<MunCanne>().Setup(myPLayer, new Vector3(Mathf.Tan(dir), 0, 1), distanceMax, dommage);
    }
}
