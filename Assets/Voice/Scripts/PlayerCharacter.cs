using System;

[Serializable]
public class PlayerCharacter : Entry<PlayerCharacterData> {
    protected override string Suffix { get { return ".Voice.pc"; } }
}
