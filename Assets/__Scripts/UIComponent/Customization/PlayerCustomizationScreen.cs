using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomizationScreen : ScreenBase
{
    [Header("Feedback References")] 
    [SerializeField] private Image padlockIcon;
    
    [Header("Navigation References")]
    [SerializeField] private Button leftBodyArrow;
    [SerializeField] private Button leftTurretArrow;
    [SerializeField] private Button rightBodyArrow;
    [SerializeField] private Button rightTurretArrow;
    [SerializeField] private Button closeButton;

    public static event Action<int,int> OnSkinChanged;
    
    private int _indexBody = 0;
    private int _indexTurret = 0;

    private int indexBody
    {
        get { return _indexBody; }
        set
        {
            _indexBody = value;
            UpdatePlayerPreview();
        }
    }
    private int indexTurret
    {
        get { return _indexTurret; }
        set
        {
            _indexTurret = value;
            UpdatePlayerPreview();
        }
    }

    private int maxBodyIndex => GameManager.I.GameSettings.CustomizationParts.bodyUnlockConditions.Length -1;
    private int maxTurretIndex => GameManager.I.GameSettings.CustomizationParts.turretUnlockConditions.Length -1;
    
    protected override void Awake()
    {
        base.Awake();
        InitialSetup();

        void InitialSetup()
        {
            indexBody = SaveGameManager.informationsToSave.currentBodyIndex;
            indexTurret = SaveGameManager.informationsToSave.currentTurretIndex;
            padlockIcon.enabled =
                !GameManager.Instance.GameSettings.CustomizationParts.bodyUnlockConditions[indexBody].unlocked || !GameManager
                    .Instance.GameSettings.CustomizationParts.turretUnlockConditions[indexTurret].unlocked;

            leftBodyArrow.onClick.AddListener(() =>
            {
                NavigationBodyClick(-1);
            });
            rightBodyArrow.onClick.AddListener(() =>
            {
                NavigationBodyClick(1);
            });
            leftTurretArrow.onClick.AddListener(() =>
            {
                NavigationTurretClick(-1);
            });
            rightTurretArrow.onClick.AddListener(() =>
            {
                NavigationTurretClick(1);
            });
            
            closeButton.onClick.AddListener(CloseScreen);
        }
    }

    private void NavigationBodyClick(int direction)
    {
        var some = indexBody + direction;
        indexBody = some < 0 ? maxBodyIndex : some > maxBodyIndex ? 0 : indexBody + direction;
        padlockIcon.enabled =
            !GameManager.Instance.GameSettings.CustomizationParts.bodyUnlockConditions[indexBody].unlocked || !GameManager
                .Instance.GameSettings.CustomizationParts.turretUnlockConditions[indexTurret].unlocked;
    }

    private void NavigationTurretClick(int direction)
    {
        var some = indexTurret + direction;
        indexTurret = some < 0 ? maxTurretIndex : some > maxTurretIndex ? 0 : indexTurret + direction;
        padlockIcon.enabled =
            !GameManager.Instance.GameSettings.CustomizationParts.bodyUnlockConditions[indexBody].unlocked || !GameManager
                .Instance.GameSettings.CustomizationParts.turretUnlockConditions[indexTurret].unlocked;
    }

    private void UpdatePlayerPreview()
    {
        OnSkinChanged?.Invoke(indexBody, indexTurret);
    }

    private void CheckAndSaveSetup()
    {
        if (!GameManager.Instance.GameSettings.CustomizationParts.bodyUnlockConditions[indexBody].unlocked)
        {
            indexBody = SaveGameManager.informationsToSave.currentBodyIndex;
        }
        
        if (!GameManager.Instance.GameSettings.CustomizationParts.turretUnlockConditions[indexTurret].unlocked)
        {
            indexTurret = SaveGameManager.informationsToSave.currentTurretIndex;
        }

        SaveGameManager.informationsToSave.currentBodyIndex = indexBody;
        SaveGameManager.informationsToSave.currentTurretIndex = indexTurret;
        SaveGameManager.SaveGame();
        UpdatePlayerPreview();
    }

    public override void ShowScreen()
    {
        base.ShowScreen();
        closeButton.interactable = true;
    }

    protected override void CloseScreen()
    {
        CheckAndSaveSetup();
        SaveGameManager.informationsToSave.currentBodyIndex = indexBody;
        SaveGameManager.informationsToSave.currentTurretIndex = indexTurret;
        CloseScreen(GameManager.Instance.CustomizationScreenClosed);
        closeButton.interactable = false;
    }
    
    public override void CloseScreen(Action callback)
    {
        base.CloseScreen();
        if (callback != null)
            FinishCallback += callback;
    }
}
