using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : ScreenBase
{
    [SerializeField] private Button closeButton;
    
    protected override void Awake()
    {
        base.Awake();
        closeButton.onClick.AddListener(CloseScreen);
    }

    public override void ShowScreen()
    {
        base.ShowScreen();
        closeButton.interactable = true;
    }

    protected override void CloseScreen()
    {
        CloseScreen(GameManager.Instance.Unpause);
        closeButton.interactable = false;
    }

    public override void CloseScreen(Action callback)
    {
        base.CloseScreen();
        if (callback != null)
            FinishCallback += callback;
    }
}
