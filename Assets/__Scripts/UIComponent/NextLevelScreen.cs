using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class NextLevelScreen : ScreenBase
{
    [SerializeField] private int screenDuration = 5;
    [SerializeField] private TextMeshProUGUI titleTmp;
    [SerializeField] private TextMeshProUGUI pointsTmp;

    public override void FinishedAnimation()
    {
        base.FinishedAnimation();
        Time.timeScale = 1;
        GameManager.Instance.GameState = GameStates.RUNNING;
        GameManager.Instance.CurrentStage += 1;
        GameManager.Instance.LifeRemaining++;
    }

    public override void ShowScreen()
    {
        base.ShowScreen();
        Time.timeScale = 0;
        titleTmp.text = $"NEXT LEVEL  {(GameManager.Instance.CurrentStage + 1).ToString("00")}";
        pointsTmp.text = $"Points: {GameManager.Instance.Score}";
        CloseAfterTime();
    }

    private async void CloseAfterTime()
    {
        await Task.Delay(1000 * screenDuration);
        CloseScreen();
    }
}
