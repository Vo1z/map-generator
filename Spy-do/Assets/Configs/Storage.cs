// written by Voiz
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using config;

public class Storage : MonoBehaviour
{
    public static float[] variables;
    Config cfg = new Config("Assets\\Configs\\Cfg.txt", 100);
    
    void Awake()
    {
        variables = cfg.variables;

        //for test
        //for (int i = 0; i < variables.Length; i++)
        //Debug.Log(variables[i]);
    }
}
