using System;

[Serializable]
public abstract class Named {
    private string name;
    public string Name {
        get {
            return name;
        }
        set {
            name = value;
        }
    }
}