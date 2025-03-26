using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Window : MonoBehaviour
{
    [SerializeField] private string windowName;

    [Space(10)]
    [SerializeField] private Animator windowAnimator;

    [SerializeField] protected string openAnimationName;
    [SerializeField] protected string idleAnimationName;
    [SerializeField] protected string closeAnimationName;
    [SerializeField] protected string hiddenAnimationName;

    public bool IsOpened { get; protected set; } = false;

    protected Animator WindowAnimator
    {
        get
        {
            if (windowAnimator == null)
                windowAnimator = GetComponent<Animator>();

            return windowAnimator;
        }
    }

    public virtual void Initialize() { }

    public void Show(bool isImmediately)
    {
        OpenStart();
        WindowAnimator.Play(isImmediately ? idleAnimationName : openAnimationName);
        if (isImmediately)
            OpenEnd();
    }

    public void Hide(bool isImmediately)
    {
        CloseStart();
        WindowAnimator.Play(isImmediately ? hiddenAnimationName : closeAnimationName);
        if (isImmediately)
            CloseEnd();
    }

    protected virtual void OpenStart()
    {
        this.gameObject.SetActive(true);
        IsOpened = true;
    }

    protected virtual void OpenEnd()
    {
    }

    protected virtual void CloseStart()
    {
        IsOpened = false;
    }

    protected virtual void CloseEnd()
    {
        this.gameObject.SetActive(false);
    }
}
