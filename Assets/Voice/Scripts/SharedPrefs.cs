using System;
using UnityEngine;

public class SharedPrefs : MonoBehaviour {
    public static readonly string sceneRoot = "Voice/Scenes/";
    public static readonly string langaugePreference = "Langauge";
    public static readonly string defaultLangauge = "English";
    private static string langauge;
    public static string Langauge {
        get {
            return PlayerPrefs.GetString(langaugePreference, defaultLangauge);
        }
        set {
            PlayerPrefs.SetString(langaugePreference, value);
            langauge = value;
        }
    }
    public static readonly string savePreference = "Save";
    public static readonly string defaultSave = DateTime.Now.ToString("yyyMMDDhhmmss");
    private string save;
    public string Save {
        get {
            return PlayerPrefs.GetString(savePreference, defaultSave);
        }
        set {
            PlayerPrefs.SetString(langaugePreference, value);
            save = value;
        }
    }
}
