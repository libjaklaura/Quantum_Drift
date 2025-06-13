using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Battery : MonoBehaviour
{
    public BatteryTask batteryTask;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectionSpot"))
        {
            if (batteryTask != null)
            {
                batteryTask.UpdateTaskProgress();
            }

            other.tag = "Untagged";

            XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
            {
                Destroy(grabInteractable);
            }

            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
        }
    }
}
