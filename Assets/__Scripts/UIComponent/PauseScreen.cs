using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : ScreenBase
{
    [SerializeField] private Button closeButton;
    
    protected override void Awake()
    {
        base.Awake();
        closeButton.onClick.AddListener(Close);
    }

    public override void ShowScreen()
    {
        base.ShowScreen();
        closeButton.interactable = true;
    }

    private void Close()
    {
        closeButton.interactable = false;
        GameManager.Instance.Unpause();
    }
}
