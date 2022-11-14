using UnityEngine;
using UnityEngine.UI;

public sealed class MainMenu : ScreenBase
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button customizationButton;
    [SerializeField] private Button deleteSaveButton;
    
    protected override void Awake()
    {
        base.Awake();
        startButton.onClick.AddListener(CloseScreen);
        deleteSaveButton.onClick.AddListener(DeleteSave);
        customizationButton.onClick.AddListener(OpenCustomScreen);
        CheckButtonStates();
        ShowScreen();
    }

    private void DeleteSave()
    {
        SaveGameManager.DeleteSave();
        CheckButtonStates();
    }

    private void OpenCustomScreen()
    {
        GameManager.Instance.CallCustomizationScreen();
    }

    private void CheckButtonStates()
    {
        deleteSaveButton.interactable = SaveGameManager.HasSaveData();
        deleteSaveButton.gameObject.SetActive(Application.isEditor);
    }
    
    public override void FinishedAnimation()
    {
        base.FinishedAnimation();
        GameManager.Instance.Initialize();
    }

    public override void ShowScreen()
    {
        Time.timeScale = 0;
        CheckButtonStates();
        base.ShowScreen();
    }
}
