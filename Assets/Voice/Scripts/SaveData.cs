using System;

[Serializable]
public class SaveData : Named {
    public string name;
    public string Name { get { return name; } set { name = value; } }
    public int id;
    public int Id { get { return id; } set { id = value; } }
}
