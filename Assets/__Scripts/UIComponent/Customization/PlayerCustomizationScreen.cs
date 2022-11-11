using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomizationScreen : ScreenBase
{
    [Header("Interface References")]
    [SerializeField] private Button leftBodyArrow;
    [SerializeField] private Button leftTurretArrow;
    [SerializeField] private Button rightBodyArrow;
    [SerializeField] private Button rightTurretArrow;
    [SerializeField] private Button closeButton;

    
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

    private int maxBodyIndex => GameManager.I.GameSettings.CustomizationParts.partUnlockConditions.GroupBy(type => type.partType == PartType.BODY).Count() -1;
    private int maxTurretIndex => GameManager.I.GameSettings.CustomizationParts.partUnlockConditions.GroupBy(type => type.partType == PartType.TURRET).Count() -1;
    
    protected override void Awake()
    {
        base.Awake();
        InitialSetup();

        void InitialSetup()
        {
            indexBody = SaveGameManager.informationsToSave.currentBodyIndex;
            indexTurret = SaveGameManager.informationsToSave.currentTurretIndex;

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
    }

    private void NavigationTurretClick(int direction)
    {
        var some = indexTurret + direction;
        indexTurret = some < 0 ? maxTurretIndex : some > maxTurretIndex ? 0 : indexTurret + direction;
    }

    private void UpdatePlayerPreview()
    {
        
    }

    protected override void CloseScreen()
    {
        CloseScreen(GameManager.Instance.Unpause);
        closeButton.interactable = false;
    }
}
