using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Command : MonoBehaviour {
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Values;
    public Button Append;
    public Button Replace;
    public Button Restore;
    public int Index;
    public KeyCode[] Keys;
    public void SetValuesText() {
        Values.text = Keys.Select(x => Enum.GetName(typeof(KeyCode), x)).Aggregate((acc, x) => acc += ";" + x);
    }
}
