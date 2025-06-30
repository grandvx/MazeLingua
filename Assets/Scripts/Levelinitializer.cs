using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Kartograph.Entities;

public class Levelinitializer : MonoBehaviour
{
    [SerializeField] LevelGeneratorBase generator;
    // Start is called before the first frame update
    void Start()
    {
        generator.Generate(() => { });
    }
}
