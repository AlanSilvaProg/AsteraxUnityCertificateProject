using UnityEngine;
using UnityEngine.UI;

public class GameOver : ScreenBase
{
    [SerializeField] private Button goToMenuButton;
    
    protected override void Awake()
    {
        base.Awake();
        goToMenuButton.onClick.AddListener(CloseScreen);
    }

    public override void ShowScreen()
    {
        base.ShowScreen();
        Time.timeScale = 0;
    }

    public override void FinishedAnimation()
    {
        base.FinishedAnimation();
        GameManager.Instance.RestartGame();
    }
}
