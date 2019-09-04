using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanneAPeche : ObjectBase
{

    public Transform ligne;

    public override void Lancer(float dir)
    {
        Instantiate(ligne);
    }

}
