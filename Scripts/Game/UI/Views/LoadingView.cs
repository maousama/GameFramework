using Assets.Scripts.UIFramework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingView : View
{
    public Image progressBar;
    public float progressSpeed = 0.01f;
    [Range(0, 1)]
    public float maxProgress = 0.9f;

    public IEnumerator StartupProgressBar(Operation operation)
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

public class Operation
{
    private bool isDone = true;
    private float progress = 0;
    private Action completed;

    public bool IsDone { get => isDone; set => isDone = value; }
    public float Progress { get => progress; set => progress = value; }
    public Action Completed { get => completed; set => completed = value; }
}