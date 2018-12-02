using TMPro;
using UnityEngine.UI;

public class Save : Entry<SaveData> {
    protected override string Suffix { get { return ".Voice.s"; } }
    public TextMeshProUGUI TitleText;
    public Button DeleteButton;
    public Button OverwriteButton;
    public Button LoadButton;
}
