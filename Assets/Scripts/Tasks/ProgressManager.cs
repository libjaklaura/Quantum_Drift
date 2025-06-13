using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class ProgressManager : MonoBehaviour
{
    public Slider progressBar;
    public TextMeshProUGUI percentageText;
    public GameObject congratsScreen;
    public List<Task> tasks = new List<Task>();

    void Start()
    {
        foreach (var task in tasks)
        {
            task.SetProgressManager(this);
        }

        UpdateProgress();
    }

    public void UpdateProgress()
    {
        int totalTasks = tasks.Count;
        if (totalTasks == 0) return;

        int completedTasks = tasks.Count(task => task.IsCompleted());
        float targetProgress = (float)completedTasks / totalTasks;

        if (progressBar != null)
            progressBar.value = targetProgress;

        if (percentageText != null)
            percentageText.text = (targetProgress * 100).ToString("F0") + "%";

        if (targetProgress == 1)
        {
            if (congratsScreen != null)
                congratsScreen.SetActive(true);
        }
    }

    public void CloseCongratsScreen()
    {
        if (congratsScreen != null)
        {
            congratsScreen.SetActive(false);
        }
    }
}
