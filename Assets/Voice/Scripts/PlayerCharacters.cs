using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class PlayerCharacters : Registry<PlayerCharacter, PlayerCharacterData> {
    protected override string Suffix { get { return ".Voice.pcs"; } }
    public static PlayerCharacter CurrentCharacter;
    public static PlayerCharacters Singleton { get; private set; }
    public string DefaultCharacterName = "<System>";
    void Awake() {
        if (Singleton == null) {
            Singleton = this;
        }
        var previous_default = transform.Find(DefaultCharacterName);
        if (previous_default != null) {
            Destroy(previous_default.gameObject);
        }
        var go_default = Instantiate(Default, transform);
        go_default.name = DefaultCharacterName;
        CurrentCharacter = go_default.GetComponent<PlayerCharacter>();
        CurrentCharacter.Data = new PlayerCharacterData {
            Name = DefaultCharacterName,
            Id = 0
        };
        Load(0);
        if (Entries.Length == 0) {
            Entries = new PlayerCharacterData[1];
            Entries[0] = CurrentCharacter.Data;
            Save(0);
        }
        for (int i = 1; i < Entries.Length; i++) {
            CreateEntry(i);
        }
    }
    public void ChangeCharacter(PlayerCharacterData c) {
        var new_playercharacter = transform.Find(c.name);
        if (new_playercharacter == null) {
            var e = Entries.SingleOrDefault(x => x.id == c.id);
            if (e != null) {
                CreateEntry(e.id);
                new_playercharacter = transform.Find(c.name);
            }
            else {
                return;
            }
        }
        CurrentCharacter = new_playercharacter.gameObject.GetComponent<PlayerCharacter>();
    }
    public void NewCharacter(string name) {
        if (name != string.Empty) {
            var e = new PlayerCharacterData[Entries.Length + 1];
            Entries.CopyTo(e, 0);
            e[Entries.Length] = new PlayerCharacterData {
                Name = name,
                Id = Entries.Max(x => x.Id) + 1
            };
            Entries = e;
            CreateEntry(Entries.Length - 1);
            Save(0);
            ChangeCharacter(Entries[Entries.Length - 1]);
        }
    }
    private void CreateEntry(int i) {
        var previous_player = transform.Find(Entries[i].Name);
        if (previous_player != null) {
            Destroy(previous_player.gameObject);
        }
        var go_player = Instantiate(Default, transform);
        go_player.name = Entries[i].Name;
        var pc = go_player.GetComponent<PlayerCharacter>();
        pc.Data = Entries[i];
    }
    private static string[] DistinctCharacters() {
        return Directory.GetFiles(Application.persistentDataPath).Select(x => {
            var paths = x.Split('\\');
            var file = paths[paths.Length - 1];
            return file.Split('_')[0];
        })
        .Distinct().ToArray();
    }
}
