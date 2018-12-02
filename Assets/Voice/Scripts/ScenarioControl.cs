using System;
using UnityEngine;

public class ScenarioControl : MonoBehaviour {
    private bool pause = false;
    public void TogglePause() {
        pause = !pause;
        Time.timeScale = 1.0f * Convert.ToSingle(pause);
    }
}
