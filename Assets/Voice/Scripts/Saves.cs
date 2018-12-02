using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Saves : Registry<Save, SaveData> {
    protected override string Suffix { get { return ".Voice.ss"; } }

    public Transform SaveParent;
    public Dropdown ProfilesDropdown;

    void Awake() {
        if (ProfilesDropdown != null) {
            var os = new List<Dropdown.OptionData>();
            foreach (var pc in PlayerCharacters.Singleton.Entries.Where(x => x.id != 0)) {
                var o = new Dropdown.OptionData {
                    text = pc.name
                };
                os.Add(o);
            }
            ProfilesDropdown.options = os;
            ProfilesDropdown.value = PlayerCharacters.Singleton.CurrentCharacter.Data.Id - 1;
        }
        RebuildSaves();
    }
    public void RebuildSaves() {
        Load(PlayerCharacters.Singleton.CurrentCharacter.Data.Id);
        foreach (Transform t in SaveParent) {
            if (t.name.Contains("_")) {
                Destroy(t.gameObject);
            }
        }
        int i = 0;
        foreach (var s in Entries) {
            var go_save = Instantiate(Default, SaveParent);
            go_save.name = s.Name;
            var c_s = go_save.GetComponent<Save>();
            c_s.TitleText.text = s.DateSaved.ToString("yyyy-MM-dd hh:mm:ss");
            c_s.Data.Id = i;
            c_s.Data.Name = s.Name;
            if (c_s.DeleteButton != null) {
                c_s.DeleteButton.onClick.AddListener(() => DeleteSave(c_s.Data.Id));
            }
            if (c_s.OverwriteButton != null) {
                c_s.OverwriteButton.onClick.AddListener(() => OverwriteSave(c_s.Data.Id));
            }
            if (c_s.LoadButton != null) {
                c_s.LoadButton.onClick.AddListener(() => LoadSave(c_s.Data.Id));
            }
            i++;
        }
    }
    public void NewSave() {
        var e = new SaveData[Entries.Length + 1];
        Entries.CopyTo(e, 0);
        e[Entries.Length] = new SaveData {
            DateSaved = DateTime.Now,
            Id = Entries.Length,
            Name = PlayerCharacters.Singleton.CurrentCharacter.Data.id + "_" + Entries.Length
        };
        Entries = e;
        Save(PlayerCharacters.Singleton.CurrentCharacter.Data.id);
        Game.Singleton.SaveGame(Entries[Entries.Length - 1]);
        RebuildSaves();
    }
    public void OverwriteSave(int i) {
        Game.Singleton.SaveGame(new SaveData {
            DateSaved = DateTime.Now,
            Id = i,
            Name = PlayerCharacters.Singleton.CurrentCharacter.Data.id + "_" + i
        });
        RebuildSaves();
    }
    public void DeleteSave(int i) {
        var delete = Entries[i];
        var l = Entries.Length;
        if (l == 1) {
            Game.Singleton.DeleteGame(Entries[0]);
            Entries = new SaveData[0];
        }
        else {
            var e = new SaveData[Entries.Length - 1];
            int index = 0;
            foreach (var entry in Entries) {
                if (entry.id != i) {
                    e[index] = entry;
                    index++;
                }
            }
            Entries = e;
        }
        Save(PlayerCharacters.Singleton.CurrentCharacter.Data.id);
        Game.Singleton.DeleteGame(delete);
        RebuildSaves();
    }
    public void LoadSave(int i) {
        Game.Singleton.LoadGame(Entries[i]);
    }
    public void OnValueChanged(int i) {
        PlayerCharacters.Singleton.ChangeCharacter(i + 1);
        RebuildSaves();
    }
}
