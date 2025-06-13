using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTask : Task
{
    public int wireCount;
    private int connectedCount = 0;

    public void WireComplete(){
        connectedCount++;
        if (connectedCount == wireCount)
        {
            Complete();
        }
    }
}
