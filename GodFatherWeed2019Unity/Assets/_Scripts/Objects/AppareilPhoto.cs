﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppareilPhoto : ObjectBase
{
    [Header("Flash")]
    public float flashRange;
    public float flashAngle;
    public float StunTime;

    public override void Capacité(float dir)
    {
        base.Capacité(dir);
        foreach (var Player in PlayerController._players)
        {
            if (myPLayer != Player)
            {
                Vector3 direction = (Player.transform.position) - myPLayer.transform.position;
                direction = new Vector3(direction.x, 0, direction.z);
                float angle = Vector3.Angle(myPLayer.transform.forward, direction);
                Debug.Log(angle);
                SoundManager.Instance.PlaySFX("Flash", SoundManager.DefaultTypes.UseItem);
                if (Mathf.Abs(angle) < flashAngle && direction.magnitude < flashRange)
                {
                    Player.StunPlayer(StunTime);
                }
            }
        }
    }

}
