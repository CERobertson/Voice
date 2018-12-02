using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRunner : MonoBehaviour {
    public ScenarioControl Scenario;
    public MenuControl Menu;
    public SaveControl Save;
    public LoadControl Load;
    private bool toggle;
    private Stack<GameObject> BackStack;

    void Awake() {
        BackStack = new Stack<GameObject>();
        InputController.OnMenu += InputController_OnMenu;
        InputController.OnBack += InputController_OnBack;
        toggle = Menu.gameObject.activeSelf;
        Menu.Save.onClick.AddListener(() => SaveMenu());
        Menu.Load.onClick.AddListener(() => LoadMenu());
    }
    private void OnDestroy() {
        InputController.OnMenu -= InputController_OnMenu;
        InputController.OnBack -= InputController_OnBack;
    }

    private void InputController_OnBack() {
        if (toggle && BackStack.Count == 0) {
            Toggle();
        }
        else if (toggle) {
            BackStack.Pop().SetActive(false);
        }
    }
    private void InputController_OnMenu() {
        Toggle();
    }
    private void Toggle() {
        toggle = !toggle;
        Menu.gameObject.SetActive(toggle);
        Save.gameObject.SetActive(false);
        Load.gameObject.SetActive(false);
        BackStack.Clear();
        Scenario.TogglePause();
    }
    private void SaveMenu() {
        BackStack.Push(Save.gameObject);
        Save.gameObject.SetActive(true);
    }
    private void LoadMenu() {
        BackStack.Push(Load.gameObject);
        Load.gameObject.SetActive(true);
    }
}
