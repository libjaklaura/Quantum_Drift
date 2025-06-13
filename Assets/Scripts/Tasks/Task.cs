using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    private bool isCompleted = false;
    private ProgressManager progressManager;

    public void SetProgressManager(ProgressManager manager)
    {
        progressManager = manager;
    }

    public bool IsCompleted() => isCompleted;

    public void Complete()
    {
        if (!isCompleted)
        {
            isCompleted = true;

            if (progressManager != null)
            {
                progressManager.UpdateProgress();
            }
        }
    }
}

