using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SaveControl : Registry<Save,SaveData> {
    protected override string Suffix { get { return ".Voice.sc"; } }
    public Transform SaveParent;
    public Dropdown Profiles;

    void Awake() {
        var os = new List<Dropdown.OptionData>();
        foreach (var pc in PlayerCharacters.Singleton.Entries.Where(x => x.id != 0)) {
            var o = new Dropdown.OptionData {
                text = pc.name
            };
            os.Add(o);
        }
        Profiles.options = os;
        Profiles.value = PlayerCharacters.Singleton.CurrentCharacter.Data.Id - 1;
        RebuildSaves();
    }
    public void RebuildSaves() {
        Saves.Singleton.Load(PlayerCharacters.Singleton.CurrentCharacter.Data.Id);
        foreach (Transform t in SaveParent) {
            Destroy(t.gameObject);
        }
        foreach (var s in Saves.Singleton.Entries) {
            var go_save = Instantiate(Default, SaveParent);
            var c_s = go_save.GetComponent<Save>();
            c_s.Data.Id = s.Id;
            c_s.Data.Name = s.Name;
        }
    }
    public void NewSave() {

    }
    public void LoadSave(int i) {
        Saves.Singleton.ChangeSave(i);
    }
    public void OverwriteSave(int i) {

    }
    public void DeleteSave(int i) {
    }
    public void OnValueChanged(int i) {
        PlayerCharacters.Singleton.ChangeCharacter(i + 1);
        RebuildSaves();
    }
}
