﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{

     public MunCanne @object;
    public PlayerController Pc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            @object.Setup(Pc, new Vector3(0, 0, 1));
        }
    }
}