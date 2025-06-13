using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class PlugController : MonoBehaviour
{
    public bool isConected = false;
    public UnityEvent OnWirePlugged;
    public Transform plugPosition;

    [HideInInspector] public Transform endAnchor;
    [HideInInspector] public Rigidbody endAnchorRB;
    [HideInInspector] public WireController wireController;

    public void OnPlugged()
    {
        OnWirePlugged.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject == endAnchor.gameObject && !isConected)
        {
            isConected = true;

            // Get the grab interactable component
            XRGrabInteractable grabInteractable = endAnchor.GetComponent<XRGrabInteractable>();

            // Force release if still held
            if (grabInteractable != null && grabInteractable.isSelected)
            {
                IXRSelectInteractor interactor = grabInteractable.firstInteractorSelecting;
                if (interactor != null)
                {
                    grabInteractable.interactionManager.SelectExit(interactor, grabInteractable);
                }
            }

            // Make it kinematic
            endAnchorRB.isKinematic = true;

            // Snap into plug position
            endAnchor.transform.position = plugPosition.position;
            Vector3 eulerRotation = new Vector3(this.transform.eulerAngles.x + 90, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
            endAnchor.transform.rotation = Quaternion.Euler(eulerRotation);

            // Disable grabbing to prevent future interaction
            grabInteractable.enabled = false;

            OnPlugged();
        }
    }
}
