using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Game))]
public class SceneControl : SharedPrefs {
    public static SceneControl Singleton;
    public string InitialScene;
    public MenuControl MenuControl;
    public Game Game;
    void Awake() {
        DontDestroyOnLoad(gameObject);
        if (Singleton == null) {
            Singleton = this;
        }
        Game = GetComponent<Game>();
        LoadScene(InitialScene);
    }
    public void LoadScene(string scene) {
        Game.Data.CurrentScene = scene;
        SceneManager.LoadScene(string.Format("{0}{1}/{2}", sceneRoot, Langauge, scene));
    }
}
