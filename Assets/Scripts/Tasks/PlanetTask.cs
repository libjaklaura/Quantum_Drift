using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlanetTask : Task
{
    private int correctCount = 0;
    public int totalPlanets = 8;

    public void IncrementCorrect()
    {
        correctCount++;

        if (correctCount >= totalPlanets)
        {
            Complete();
            StartCoroutine(LockAllPlanets());
            
        }
    }

    public void DecrementCorrect()
    {
        correctCount = Mathf.Max(0, correctCount - 1);
    }

    private IEnumerator LockAllPlanets()
    {
        yield return new WaitForSeconds(1);
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

        foreach (GameObject planet in planets)
        {
            XRGrabInteractable grab = planet.GetComponent<XRGrabInteractable>();
            if (grab != null)
            {
                Destroy(grab);
            }

            Rigidbody rb = planet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
    }
}
