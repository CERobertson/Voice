using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
    public delegate void ConfirmationHandler();
    public static event ConfirmationHandler OnConfirmation;
    public string[] JoystickNames;

    private static void Confirmed() {
        if (OnConfirmation != null) {
            OnConfirmation();
        }
    }
    // Update is called once per frame
    void Update () {
        if (Input.anyKeyDown) {
            JoystickNames = Input.GetJoystickNames();
            Confirmed();
        }
	}
}
