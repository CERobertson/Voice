using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public abstract class Registry<T,E> : SharedPrefs
    where T : Entry<E>
    where E : Named {
    protected abstract string Suffix { get; }
    public GameObject Default;
    public T Entry;
    public E[] Entries;
    //public void Save() {
    //    Save(PlayerCharacters.CurrentCharacter);
    //}
    public void Save(int character) {
        var bFormatter = new BinaryFormatter();
        using (var ms = MemoryStream(character)) {
            bFormatter.Serialize(ms, Entries);
        }
    }
    //public void Load() {
    //    Load(PlayerCharacters.CurrentCharacter);
    //}
    public void Load(int character) {
        var bFormatter = new BinaryFormatter();
        using (var ms = MemoryStream(character)) {
            if (ms.Length > 0) {
                Entries = (E[])bFormatter.Deserialize(ms);
            }
        }
    }
    string FileString(string input) {
        return input ;
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
        return File.Open(Path.Combine(Application.persistentDataPath, character.ToString() + "_" + Suffix), FileMode.OpenOrCreate);
    }
}
