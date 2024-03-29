﻿using System.Linq;
using UnityEngine;

public class InputController : Registry<KeyMap,KeyMapData> {
    public GameObject DefaultKeyMap;
    protected override string Suffix { get { return ".Voice.KeyMap"; } }
    public delegate void InputHandler();
    public static event InputHandler OnConfirmation;
    public static void Confirmed() {
        if (OnConfirmation != null) {
            OnConfirmation();
        }
    }
    public static event InputHandler OnBack;
    public static void Back() {
        if (OnBack != null) {
            OnBack();
        }
    }
    public static event InputHandler OnMenu;
    public static void Menu() {
        if (OnMenu != null) {
            OnMenu();
        }
    }
    void Awake() {
        if (Singleton == null) {
            Singleton = this;
        }
        Entry.Load(0);
    }
    void Update() {
        if (QueryInput(Entry.Data.Confirmation)) {
            Confirmed();
        }
        if (QueryInput(Entry.Data.Back)) {
            Back();
        }
        if (QueryInput(Entry.Data.Menu)) {
            Menu();
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
