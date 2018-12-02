using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TheBasementScenarioData : Named {

    public string name;
    public string Name { get { return name; } set { name = value; } }
    public int id;
    public int Id { get { return id; } set { id = value; } }
}
