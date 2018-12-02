using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class Saves : Registry<Save, SaveData> {
    protected override string Suffix { get { return ".Voice.ss"; } }
    public static Saves Singleton { get; private set; }
    void Awake() {
        if (Singleton == null) {
            Singleton = this;
        }
        RebuildSaveProfiles();
    }
    public GameObject[] Profiles;
    public Save CurrentSave;

    public void ChangeSave(int i) {
    }
    public void RebuildSaveProfiles() {
        Profiles = new GameObject[PlayerCharacters.Singleton.Entries.Length-1];
        int i = 0;
        foreach (var e in PlayerCharacters.Singleton.Entries.Where(x => x.id != 0)) {
            var previous_saveProfile = transform.Find(e.name);
            if (previous_saveProfile != null) {
                Destroy(previous_saveProfile.gameObject);
            }
            var go_saveProfile = new GameObject(e.Name);
            go_saveProfile.transform.parent = transform;
            Profiles[i] = go_saveProfile;
            i++;
        }
    }
}
