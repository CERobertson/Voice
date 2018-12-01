using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ControllerProbe : MonoBehaviour {
    public string Script;
    public int MaxIndex;
    public int MinIndex;
    public KeyCode[] KeyCodes;
    public Dictionary<int, KeyCode> DefaultKeyCodes;
    void Start() {
        DefaultKeyCodes = new Dictionary<int, KeyCode>();
        var KeyCodeType = Enum.GetUnderlyingType(typeof(KeyCode));
        var KeyCodeValues = Enum.GetValues(typeof(KeyCode));
        foreach (var kcv in KeyCodeValues) {
            var name = Enum.GetName(typeof(KeyCode), kcv);
            var kc = (KeyCode)Enum.Parse(typeof(KeyCode), name, false);
            var index = (int)Convert.ChangeType(kcv, KeyCodeType);
            DefaultKeyCodes[index] = kc;
        }
        MaxIndex = DefaultKeyCodes.Keys.Max();
        MinIndex = DefaultKeyCodes.Keys.Min();
        if (MinIndex >= 0) {
            KeyCodes = new KeyCode[MaxIndex + 1];
            foreach (var dfkc in DefaultKeyCodes) {
                KeyCodes[dfkc.Key] = dfkc.Value;
            }
        }
    }
    private string KeyCode() {
        var sb = new StringBuilder();
        var KeyCodeType = Enum.GetUnderlyingType(typeof(KeyCode));
        var KeyCodeValues = Enum.GetValues(typeof(KeyCode));
        foreach (var kcv in KeyCodeValues) {
            sb.AppendLine(string.Format("{0} = {1}", Enum.GetName(typeof(KeyCode), kcv), Convert.ChangeType(kcv, KeyCodeType)));
        }
        return sb.ToString();
    }
}
