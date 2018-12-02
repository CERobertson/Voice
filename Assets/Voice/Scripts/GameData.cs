using System;

[Serializable]
public class GameData : Named {
    public string CurrentScene;
    public ScenarioData CurrentScenario;
    public string name;
    public string Name { get { return name; } set { name = value; } }
    public int id;
    public int Id { get { return id; } set { id = value; } }
}
