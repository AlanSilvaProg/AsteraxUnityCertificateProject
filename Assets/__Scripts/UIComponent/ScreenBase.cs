using System;
using System.Threading.Tasks;
using UnityEngine;

public class ScreenBase : MonoBehaviour
{
    [SerializeField] protected Animator screenAnimator;

    protected int openAnim;
    protected int closeAnim;

    private Action FinishCallback;
    
#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        if (!screenAnimator)
            screenAnimator = GetComponent<Animator>() ?? gameObject.AddComponent<Animator>();
    }
#endif

    protected virtual void Awake()
    {
        openAnim = Animator.StringToHash("ScreenOpen");
        closeAnim = Animator.StringToHash("ScreenClose");
    }

    public virtual void ShowScreen()
    {
        gameObject.SetActive(true);
        screenAnimator.Play(openAnim);
    }

    protected virtual void CloseScreen()
    {
        new NativeShare().Share();
        return;
        screenAnimator.Play(closeAnim);
    }
    
    public void CloseScreen(Action callback)
    {
        CloseScreen();
        if (callback != null)
            FinishCallback += callback;
    }

    public virtual void FinishedAnimation()
    {
        FinishCallback?.Invoke();
        FinishCallback = null;
        gameObject.SetActive(false);
        Debug.LogError("CHAMOU");
    }
}
