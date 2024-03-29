﻿using System;
using UnityEngine;

[Serializable]
public class KeyMapData : Named {
    public KeyCode[] Confirmation;
    public KeyCode[] Back;
    public KeyCode[] Menu;

    public string name;
    public string Name { get { return name; } set { name = value; } }
    public int id;
    public int Id { get { return id; } set { id = value; } }
}
