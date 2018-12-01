using System.Linq;
using UnityEngine;

public class InputController : Registry<KeyMap,KeyMapData> {
    public GameObject DefaultKeyMap;
    protected override string Suffix { get { return ".Voice.KeyMap"; } }
    public delegate void InputHandler();
    public static event InputHandler OnConfirmation;
    private static void Confirmed() {
        if (OnConfirmation != null) {
            OnConfirmation();
        }
    }
    public static event InputHandler OnBack;
    private static void Back() {
        if (OnBack != null) {
            OnBack();
        }
    }
    void Awake() {
        if (Singleton == null) {
            Singleton = this;
        }
        Entry.Load(string.Empty);
    }
    void Update() {
        if (QueryInput(Entry.Data.Confirmation)) {
            Confirmed();
        }
        if (QueryInput(Entry.Data.Back)) {
            Back();
        }
    }
    private bool QueryInput(KeyCode[] keys) {
        if (keys.Length > 0) {
            return keys.Select(x => Input.GetKeyUp(x)).Aggregate((acc, x) => acc |= x);
        }
        return false;
    }
    public void RebuildKeymap() {
        var previous_keymap = transform.Find(DefaultKeyMap.name);
        if (previous_keymap != null) {
            Destroy(previous_keymap.gameObject);
        }
        var go = Instantiate(DefaultKeyMap, transform);
        go.name = DefaultKeyMap.name;
        Entry = go.GetComponent<KeyMap>();
    }
    public static InputController Singleton { get; private set; }
}
