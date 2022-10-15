using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public abstract class PopupBase : MonoBehaviour
{
    [SerializeField] protected PopupIdentifier _identifier;

    [Header("References")] 
    [SerializeField] private TextMeshProUGUI titleTMP;
    [SerializeField] private TextMeshProUGUI bodyTMP;

    [Header("Popup Setup")] 
    [SerializeField] private float fixedDuration;
    [SerializeField] private CanvasGroup canvasGroup;
    public PopupIdentifier Identifier => _identifier;

    private RectTransform myRectTransform;

    private bool opened = false;
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        if(name != _identifier.ToString())
        {
            name = _identifier.ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
#endif
    
    protected virtual void SetupPopup()
    {
        canvasGroup.DOFade(1, fixedDuration != 0 ? fixedDuration / 2 : 1);
    }

    public virtual void SetInformations(AchievementBase achievementInfo)
    {
        titleTMP.text = achievementInfo.name;
        bodyTMP.text = achievementInfo.description;
    }

    protected virtual void Reset()
    {
        canvasGroup.DOFade(0, 0);
        PopupManager.popupRunning = false;
        Destroy(gameObject);
    }

    public virtual void Show()
    {
        if (opened) return;
        opened = true;
        if (myRectTransform == null)
            myRectTransform = GetComponent<RectTransform>();

        PopupManager.popupRunning = true;
        myRectTransform.DOLocalMoveY(myRectTransform.sizeDelta.y + 150, fixedDuration != 0 ? fixedDuration / 2 : 1).OnComplete(SetupPopup);
        
        StartCoroutine(CloseAfterDuration());

    }

    protected virtual void Close()
    {
        myRectTransform.DOLocalMoveY(myRectTransform.position.y + myRectTransform.sizeDelta.y + 150, fixedDuration != 0 ? fixedDuration / 2 : 1).OnComplete(Reset);
    }

    private IEnumerator CloseAfterDuration()
    {
        yield return new WaitForSeconds(fixedDuration != 0 ? fixedDuration / 2 : 3);
        Close();
    }
}
