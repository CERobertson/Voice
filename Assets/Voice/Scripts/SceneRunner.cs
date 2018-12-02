using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRunner : MonoBehaviour {
    public string NextScene;
    public DialogueWaitForConfirmation[] Sequence;

    int index = 0;
	void Start () {
        foreach (var s in Sequence) {
            s.OnConfirmation += OnConfirmation;
        }
        OnConfirmation();
	}
    void OnConfirmation() {
        if (index > 0) {
            Sequence[index - 1].gameObject.SetActive(false);
        }
        if (index < Sequence.Length) {
            Sequence[index].gameObject.SetActive(true);
            index++;
        }
        else {
            SceneControl.Singleton.LoadScene(NextScene);
        }
    }
}
