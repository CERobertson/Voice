using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public abstract class Entry<T> : MonoBehaviour
    where T : Named {
    public T Data;
    protected abstract string Suffix { get; }
    //public void Save() {
    //    Save(PlayerCharacters.CurrentCharacter);
    //}
    public void Save(int character) {
        var bFormatter = new BinaryFormatter();
        using (var ms = MemoryStream(character)) {
            bFormatter.Serialize(ms, Data);
        }
    }
    //public void Load() {
    //    Load(PlayerCharacters.CurrentCharacter);
    //}
    public void Load(int character) {
        var bFormatter = new BinaryFormatter();
        using (var ms = MemoryStream(character)) {
            if (ms.Length > 0) {
                Data = (T)bFormatter.Deserialize(ms);
            }
        }
    }
    string FileString(string input) {        
        return input + "_";
    }
    private string[] DistinctCharacters() {
        return Directory.GetFiles(Application.persistentDataPath).Select(x => {
            var paths = x.Split('\\');
            var file = paths[paths.Length - 1];
            return file.Split('_')[0];
        })
        .Distinct().ToArray();
    }
    private Stream MemoryStream(int character) {
        return File.Open(Path.Combine(Application.persistentDataPath, character.ToString() + "_" + Data.Id.ToString() + Suffix), FileMode.OpenOrCreate);
    }
}
