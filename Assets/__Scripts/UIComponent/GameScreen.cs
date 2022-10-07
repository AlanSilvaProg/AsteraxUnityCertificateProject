using UnityEngine;
using UnityEngine.UI;

public class GameScreen : ScreenBase
{
    [SerializeField] private Button pauseButton;

    protected override void Awake()
    {
        base.Awake();
        pauseButton.onClick.AddListener(PauseAction);
    }

    public override void ShowScreen()
    {
        base.ShowScreen();
        pauseButton.interactable = true;
    }

    private void PauseAction()
    {
        pauseButton.interactable = false;
        GameManager.Instance.Pause();
    }
}
