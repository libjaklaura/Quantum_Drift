using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTask : Task
{
    public int fireCount;
    private int count = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            other.gameObject.SetActive(false);
            Debug.Log("jo");
            count++;
            if (count == fireCount)
                Complete();
        }
    }
}
