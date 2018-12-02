using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public abstract class Entry<T> : MonoBehaviour
    where T : Named {
    public T Data;
    protected abstract string Suffix { get; }
    public void Save() {
        Save(PlayerCharacters.Singleton.CurrentCharacter.Data.Id);
    }
    public void Save(int character) {
        var bFormatter = new BinaryFormatter();
        using (var ms = MemoryStream(character)) {
            bFormatter.Serialize(ms, Data);
        }
    }
    public void Load() {
        Load(PlayerCharacters.Singleton.CurrentCharacter.Data.Id);
    }
    public void Load(int character) {
        var bFormatter = new BinaryFormatter();
        using (var ms = MemoryStream(character)) {
            if (ms.Length > 0) {
                Data = (T)bFormatter.Deserialize(ms);
            }
        }
    }
    public void Delete(int character) {
        File.Delete(Path.Combine(Application.persistentDataPath, character.ToString() + "_" + Data.Id.ToString() + Suffix));
    }
    string FileString(string input) {        
        return input + "_";
    }
    private Stream MemoryStream(int character) {
        return File.Open(Path.Combine(Application.persistentDataPath, character.ToString() + "_" + Data.Id.ToString() + Suffix), FileMode.OpenOrCreate);
    }
}
