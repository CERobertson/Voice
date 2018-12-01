using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWaitForConfirmation : MonoBehaviour {
    public event InputController.InputHandler OnConfirmation;
    void Start() {
        InputController.OnConfirmation += OnConfirmation;
    }
    void OnDestroy() {
        InputController.OnConfirmation -= OnConfirmation;
    }
}
