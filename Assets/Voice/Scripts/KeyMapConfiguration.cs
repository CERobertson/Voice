using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyMapConfiguration : MonoBehaviour {
    public KeyMap KeyMap;
    public Transform Keys;
    public GameObject CommandTemplate;
    public GameObject NextKeyMessageWindow;
    public ControllerProbe ControllerProbe;
    public EventSystem EventSystem;
    private Command[] Commands;
    void Awake() {
        RebuildCommands();
        InputController.OnBack += InputController_OnBack;
    }
    void OnDestroy() {
        InputController.OnBack -= InputController_OnBack;
    }
    private void InputController_OnBack() {
        SceneControl.LoadScene("NewCharacter");
    }

    private void RebuildCommands() {
        KeyMap = InputController.Singleton.Entry;

        var i = 0;
        var commands = typeof(KeyMapData).GetFields().Where(x => x.FieldType == typeof(KeyCode[])).Select(x => new { x.Name, Keys = (KeyCode[])x.GetValue(KeyMap.Data) }).ToArray();
        Commands = new Command[commands.Length];
        foreach (var keys in commands) {
            var previous_command = Keys.Find(keys.Name);
            if (previous_command != null) {
                Destroy(previous_command.gameObject);
            }
            var go = Instantiate(CommandTemplate, Keys);
            go.name = keys.Name;
            var command = go.GetComponent<Command>();
            command.Name.text = keys.Name;
            command.Index = i;
            command.Keys = keys.Keys;
            if (keys.Keys.Length > 0) {
                command.SetValuesText();
            }
            command.Append.onClick.AddListener(() => Append(command.Index));
            command.Replace.onClick.AddListener(() => Replace(command.Index));
            command.Restore.onClick.AddListener(() => Restore(command.Index));
            Commands[i] = command;
            i++;
        }
    }
    private IEnumerator AppendNextKeyDown(Command c) {
        var Continue = true;
        while (Continue) {
            foreach (var kc in ControllerProbe.DefaultKeyCodes) {
                if (Input.GetKeyDown(kc.Value)) {
                    var t = c.Keys.ToList();
                    t.Add(kc.Value);
                    c.Keys = t.ToArray();
                    c.SetValuesText();
                    Continue = false;
                    break;
                }
            }
            yield return null;
        }
    }
    private void Append(int id) {
        EventSystem.enabled = false;
        StartCoroutine(AppendNextKeyDown(Commands[id]));
        EventSystem.enabled = true;
    }
    private IEnumerator ReplaceNextKeyDown(Command c) {
        var Continue = true;
        while (Continue) {
            foreach (var kc in ControllerProbe.DefaultKeyCodes) {
                if (Input.GetKeyDown(kc.Value)) {
                    c.Keys = new KeyCode[] { kc.Value };
                    c.SetValuesText();
                    Continue = false;
                }
            }
            yield return null;
        }
    }
    private void Replace(int id) {
        EventSystem.enabled = false;
        StartCoroutine(ReplaceNextKeyDown(Commands[id]));
        EventSystem.enabled = true;
    }
    private void Restore(int id) {
        var c = Commands[id];
        c.Keys = (KeyCode[])typeof(KeyMapData).GetFields().Single(x => x.FieldType == typeof(KeyCode[]) && x.Name == c.gameObject.name).GetValue(KeyMap.Data);
        c.SetValuesText();
    }
    public void Apply() {
        foreach (var c in Commands) {
            typeof(KeyMapData).GetFields().Single(x => x.FieldType == typeof(KeyCode[]) && x.Name == c.gameObject.name).SetValue(KeyMap.Data, c.Keys);
        }
        KeyMap.Save(string.Empty);
    }
    public void Clear() {
        RebuildCommands();
    }
    public void Restore() {
        InputController.Singleton.RebuildKeymap();
        KeyMap = InputController.Singleton.Entry;
    }
}
