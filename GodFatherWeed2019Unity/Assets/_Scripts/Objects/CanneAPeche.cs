using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanneAPeche : ObjectBase
{
    public Transform ligne;

    public override void Lancer(float dir)
    {
        Transform instance = Instantiate(ligne);
        instance.GetComponent<MunCanne>().Setup(myPLayer, new Vector3(Mathf.Cos(Mathf.Deg2Rad * (dir - 90)), 0, -Mathf.Sin(Mathf.Deg2Rad * (dir - 90))), damage, dommage, speed);
        Destroy(this.gameObject);
    }
}
