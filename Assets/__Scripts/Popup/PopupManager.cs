using UnityEngine;

public static class PopupManager
{
    public static bool popupRunning = false;
    public static PopupBase CallPopup(PopupIdentifier popupIdentifier)
    {
        var popup = GameObject.Instantiate(Resources.Load<GameObject>($"Popups/{popupIdentifier.ToString()}"), GameManager.Instance.PopupCanvas.transform).GetComponent<PopupBase>();
        popup.Show();
        return popup;
    }
}
