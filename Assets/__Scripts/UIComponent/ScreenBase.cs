using System;
using System.Threading.Tasks;
using UnityEngine;

public class ScreenBase : MonoBehaviour
{
    [SerializeField] protected Animator screenAnimator;

    public bool opened = false;
    
    protected int openAnim;
    protected int closeAnim;

    protected Action FinishCallback;
    
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
        opened = true;
    }

    protected virtual void CloseScreen()
    {
        screenAnimator.Play(closeAnim);
        opened = false;
    }
    
    public virtual void CloseScreen(Action callback)
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
    }
}
