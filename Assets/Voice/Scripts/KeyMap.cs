﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class KeyMap : Entry<KeyMapData> {
    protected override string Suffix { get { return ".Voice.kme"; } }
}
