using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacterControl : MonoBehaviour {
    public Dropdown Characters;
    void Awake() {
        var os = new List<Dropdown.OptionData>();
        foreach (var pc in PlayerCharacters.Singleton.Entries.Where(x => x.id != 0)) {
            var o = new Dropdown.OptionData {
                text = pc.name
            };
            os.Add(o);
        }
        Characters.options = os;
    }
    public void CharacterChanged(string value) {
        PlayerCharacters.Singleton.NewCharacter(value);
    }
    public void OnValueChanged(int i) {
        PlayerCharacters.Singleton.ChangeCharacter(i + 1);
        InputController.Confirmed();
    }
}
