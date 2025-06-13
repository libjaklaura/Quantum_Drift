using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ExtinguisherController : MonoBehaviour
{
    public ParticleSystem foamSpray;
    public InputActionReference sprayAction;
    public XRGrabInteractable grabInteractable;
    public GameObject foamCollider; 

    private bool isSpraying = false;

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        sprayAction.action.performed += OnTriggerPressed;
        sprayAction.action.canceled += OnTriggerReleased;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        sprayAction.action.performed -= OnTriggerPressed;
        sprayAction.action.canceled -= OnTriggerReleased;

        StopSpray();
    }

    private void OnTriggerPressed(InputAction.CallbackContext ctx)
    {
        StartCoroutine(StartSpray());
    }

    private void OnTriggerReleased(InputAction.CallbackContext ctx)
    {
        StartCoroutine(StopSpray());
    }

    private IEnumerator StartSpray()
    {
        if (!isSpraying)
        {
            foamSpray.Play();
            isSpraying = true;
            yield return new WaitForSeconds(0.5f);
            foamCollider.SetActive(true);
            
        }
    }

    private IEnumerator StopSpray()
    {
        if (isSpraying)
        {
            foamSpray.Stop();
            yield return new WaitForSeconds(0.5f);
            foamCollider.SetActive(false);
            isSpraying = false;
        }
    }
}
