using UnityEngine;

public class TheBasement : Registry<Scenario, ScenarioData> {
    public static TheBasement Singleton { get; private set; }
    protected override string Suffix { get { return ".Voice.TheBasement"; } }
    void Awake() {
        if (Singleton == null) {
            Singleton = this;
        }
        Entries = new ScenarioData[transform.childCount];
        int index = 0;
        foreach (Transform t in transform) {
            var c = transform.Find(t.name).gameObject.GetComponent<Scenario>();
            c.Data.Id = index;
            Entries[index] = c.Data;
            index++;
        }
        Entry = transform.Find(Entries[0].name).gameObject.GetComponent<Scenario>();
        Game.Singleton.Data.CurrentScenario = Entries[0];
    }
}
