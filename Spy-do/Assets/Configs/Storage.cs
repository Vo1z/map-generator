// written by Voiz
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using config;

public class Storage : MonoBehaviour
{
    public static float[] variables;
    
    void Start()
    {
        Config cfg = new Config("Assets\\Configs\\Cfg.txt", 100);
        variables = cfg.variables;

        //for test
        foreach (float d in variables)
        Debug.Log(d);
    }
}
