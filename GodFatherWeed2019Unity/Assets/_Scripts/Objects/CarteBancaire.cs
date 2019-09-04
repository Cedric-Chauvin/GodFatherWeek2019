using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarteBancaire : ObjectBase
{

    public override void Capacité(float dir)
    {
        base.Capacité(dir);
        Transform instance = Instantiate(transform);
        instance.GetComponent<CarteBancaire>().Lancer(dir);
    }
}
