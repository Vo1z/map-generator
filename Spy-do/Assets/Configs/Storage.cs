// written by Voiz
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using config;

public class Storage : MonoBehaviour
{
    float[] variables;
    
    void Start()
    {
        Config cfg = new Config("Assets\\Configs\\Cfg.txt"); // has to be changed to local path
        this.variables = cfg.variables;

        //for test
        foreach (float d in variables)
        Debug.Log(d);
    }
}
