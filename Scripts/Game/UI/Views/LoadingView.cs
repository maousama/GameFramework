using Assets.Scripts.UIFramework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingView : View
{
    public IOperation operation;
    public Image progressBar;
    public float progressSpeed = 0.01f;

    [Range(0, 1)]
    public float maxProgress = 0.9f;

    protected override void Awake()
    {
        base.Awake();
        operation = new DefaultOperation();
        operation.Completed = delegate ()
        {
            Manager.Instance.CloseFrame(frame);
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartupProgressBar());
    }

    public IEnumerator StartupProgressBar()
    {
        float curProgress = 0;
        while (curProgress < maxProgress)
        {
            if (curProgress < operation.Progress)
            {
                float nextProgress = curProgress + progressSpeed;
                if (nextProgress < operation.Progress) curProgress = nextProgress;
                else curProgress = operation.Progress;
            }
            progressBar.fillAmount = curProgress / maxProgress;
            yield return null;
        }
        while (!operation.IsDone)
        {
            yield return null;
        }
        operation.Completed?.Invoke();
    }
}

public class DefaultOperation : IOperation
{
    private bool isDone = true;
    private float progress = 1f;
    private Action completed;

    public bool IsDone { get => isDone; set => isDone = value; }
    public float Progress { get => progress; set => progress = value; }
    public Action Completed { get => completed; set => completed = value; }
}

public interface IOperation
{
    /// <summary>
    ///  Has the operation finished? (Read Only)
    /// </summary>
    bool IsDone { get; }
    /// <summary>
    /// What's the operation's progress. (Read Only)
    /// </summary>
    float Progress { get; }
    /// <summary>
    ///  invoke when complete operation
    /// </summary>
    Action Completed { get; set; }
}