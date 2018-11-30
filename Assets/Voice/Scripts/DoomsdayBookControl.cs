using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomsdayBookControl : Registry<DoomsdayBookEntry, DoomsdayBookEntryData> {
    protected override string Suffix { get {return ".Voice.TheBasement"; } }
}
