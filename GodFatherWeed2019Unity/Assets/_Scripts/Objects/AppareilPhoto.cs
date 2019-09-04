using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppareilPhoto : ObjectBase
{
    [Header("Flash")]
    public float flashRange;
    public float flashAngle;

    public override void Capacité(float dir)
    {
        foreach (var Player in PlayerController._players)
        {
            //if(Player.item!=this)
            {
                Vector3 direction = myPLayer.transform.InverseTransformPoint(Player.transform.position);
                float angle = Mathf.Atan2(direction.x, direction.z);
                if (Mathf.Abs(angle) > flashAngle && direction.magnitude<flashRange)
                {
                    Debug.Log("flash");
                }
            }
        }  
    }

}
