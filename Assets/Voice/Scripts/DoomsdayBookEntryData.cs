using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DoomsdayBookEntryData : Named {
    public bool Met;
    public bool Alive;
    public float Height;
    public string Bio;

    public string name;
    public string Name { get { return name; } set { name = value; } }
    public int id;
    public int Id { get { return id; } set { id = value; } }
}
