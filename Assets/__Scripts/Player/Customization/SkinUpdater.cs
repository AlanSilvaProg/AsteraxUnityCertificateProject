using System;
using UnityEngine;

public sealed class SkinUpdater : PlayerComponent
{
    [SerializeField] private Camera customizationCamera;
    [SerializeField] private GameObject currentTurret;
    [SerializeField] private GameObject currentBody;
    
    private void EnableCustomCamera() => customizationCamera.gameObject.SetActive(true);
    private void DisableCustomCamera() => customizationCamera.gameObject.SetActive(false);
    
    protected override void Awake()
    {
        base.Awake();
        playerGeneral.skinUpdater = this;
        PlayerCustomizationScreen.OnSkinChanged += UpdateSkin;
        GameManager.Instance.EnterOnCustomizationMode += EnableCustomCamera;
        GameManager.Instance.ExitCustomizationMode += DisableCustomCamera;
        if (SaveGameManager.LoadSaveGame())
            UpdateSkin(SaveGameManager.informationsToSave.currentBodyIndex,
                SaveGameManager.informationsToSave.currentTurretIndex);
    }

    private void OnDestroy()
    {
        PlayerCustomizationScreen.OnSkinChanged -= UpdateSkin;
        GameManager.Instance.EnterOnCustomizationMode -= EnableCustomCamera;
        GameManager.Instance.ExitCustomizationMode -= DisableCustomCamera;
    }

    private void UpdateSkin(int body, int turret)
    {
        SetNewBody(body);
        SetNewTurret(turret);
    }

    private void SetNewBody(int index)
    {
        Destroy(currentBody);
        currentBody = Instantiate(GameManager.Instance.GameSettings.CustomizationParts.bodyUnlockConditions[index].part,
            transform);
    }
    
    private void SetNewTurret(int index)
    {
        Destroy(currentTurret);
        currentTurret = Instantiate(GameManager.Instance.GameSettings.CustomizationParts.turretUnlockConditions[index].part,
            transform);
    }
}
