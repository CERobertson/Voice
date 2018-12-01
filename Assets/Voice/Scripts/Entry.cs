using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public abstract class Entry<T> : MonoBehaviour
    where T : Named {
    public static string[] Characters;
    public T Data;
    protected abstract string Suffix { get; }
    public void Save(string character) {
        var bFormatter = new BinaryFormatter();
        using (var ms = MemoryStream(character)) {
            bFormatter.Serialize(ms, Data);
        }
    }
    public void Load(string character) {
        var bFormatter = new BinaryFormatter();
        using (var ms = MemoryStream(character)) {
            if (ms.Length > 0) {
                Data = (T)bFormatter.Deserialize(ms);
            }
        }
    }
    private string FileString(string input) {
        return input + "_";
    }
    private Stream MemoryStream(string character) {
        return File.Open(Path.Combine(Application.persistentDataPath, FileString(character) + Data.Name + Suffix), FileMode.OpenOrCreate);
    }
}
