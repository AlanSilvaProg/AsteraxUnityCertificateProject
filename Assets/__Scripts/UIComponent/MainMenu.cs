using UnityEngine;
using UnityEngine.UI;

public sealed class MainMenu : ScreenBase
{
    [SerializeField] private Button startButton;
    
    protected override void Awake()
    {
        base.Awake();
        startButton.onClick.AddListener(CloseScreen);
        ShowScreen();
    }

    public override void FinishedAnimation()
    {
        base.FinishedAnimation();
        GameManager.Instance.Initialize();
    }

    public override void ShowScreen()
    {
        Time.timeScale = 0;
        base.ShowScreen();
    }
}
