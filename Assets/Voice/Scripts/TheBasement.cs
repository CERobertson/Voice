using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBasement : Registry<Scenario, ScenarioData> {
    public static TheBasement Singleton { get; private set; }
    protected override string Suffix { get { return ".Voice.TheBasement";} }
    void Awake() {
        if (Singleton == null) {
            Singleton = this;
        }
        Entries = new ScenarioData[transform.childCount];
        int index = 0;
        foreach (Transform t in transform) {
            if (t != null) {
                Entries[index] = new ScenarioData {
                    Id = index,
                    Name = t.name
                };
                index++;
            }
        }
        Entry = transform.Find(Entries[0].name).gameObject.GetComponent<Scenario>();
    }
}
