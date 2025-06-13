using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryTask : Task
{
    public int totalBatteriesToCollect = 3;
    public GameObject lowBatterySprite;
    public GameObject fullBatterySprite;
    private int collectedBatteries = 0;

    void Awake()
    {
        lowBatterySprite.SetActive(true);
        fullBatterySprite.SetActive(false);
    }

    public void UpdateTaskProgress()
    {
        collectedBatteries++; 
        if (collectedBatteries == totalBatteriesToCollect)
        {
            lowBatterySprite.SetActive(false);
            fullBatterySprite.SetActive(true);
            Complete();
        }
    }
}
