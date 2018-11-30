using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class Registry<T,E> : SharedPrefs
    where T : Entry<E>
    where E : Named {
    protected abstract string Suffix { get; }
    public void Serialize () {
       
    }
    private Stream MemoryStream() {
        return File.Open(Path.Combine(Application.persistentDataPath, name + Suffix), FileMode.OpenOrCreate);
    }
}
