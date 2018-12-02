using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : SharedPrefs {
    public string InitialScene;
    public MenuControl MenuControl;
    void Awake() {
        DontDestroyOnLoad(gameObject);
        LoadScene(InitialScene);
    }
    public static void LoadScene(string scene) {
        SceneManager.LoadScene(string.Format("{0}{1}/{2}", sceneRoot, Langauge, scene));
    }
}
