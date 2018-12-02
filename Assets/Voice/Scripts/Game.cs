using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Entry<GameData> {
    protected override string Suffix { get { return ".Voice.g"; } }
    public static Game Singleton { get; private set; }
    void Awake() {
        if (Singleton == null) {
            Singleton = this;
        }
    }
    public void SaveGame(SaveData s) {
        Data.Id = s.Id;
        Save(PlayerCharacters.Singleton.CurrentCharacter.Data.Id);
    }
    public void LoadGame(SaveData s) {
        Data.Id = s.Id;
        Load(PlayerCharacters.Singleton.CurrentCharacter.Data.Id);
        SceneControl.Singleton.LoadScene(Data.CurrentScene);
    }
    public void DeleteGame(SaveData s) {
        Data.Id = s.Id;
        Delete(PlayerCharacters.Singleton.CurrentCharacter.Data.Id);
    }
}
