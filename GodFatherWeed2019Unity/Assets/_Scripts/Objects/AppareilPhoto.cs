using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppareilPhoto : ObjectBase
{
    [Header("Flash")]
    public float flashRange;
    public float flashAngle;

    public override void Capacité()
    {
        foreach (var Player in PlayerController._players)
        {
            //if(Player.item!=this)
            {
                Vector3 dir = myPLayer.transform.InverseTransformPoint(Player.transform.position);
                float angle = Mathf.Atan2(dir.x, dir.z);
                if (Mathf.Abs(angle) > flashAngle && dir.magnitude<flashRange)
                {
                    Debug.Log("flash");
                }
            }
        }  
    }

}
