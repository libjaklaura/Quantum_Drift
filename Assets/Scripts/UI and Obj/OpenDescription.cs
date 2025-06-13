using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OpenDescription : MonoBehaviour
{
    public GameObject canvas;
    [SerializeField] private Transform canvasParentAssignment;
    private static Transform canvasParent;
    public Material hoverMaterial;

    private Material[] originalMaterials;
    private Renderer objectRenderer;
    private XRBaseInteractable interactable;

    private void Awake()
    {
        if (canvasParent == null && canvasParentAssignment != null)
        {
            canvasParent = canvasParentAssignment;
            Debug.Log("Assigned canvasGroupParent: " + canvasParent.name);
        }
    }

    private void Start()
    {
        if (canvasParent == null)
            Debug.LogWarning("canvasGroupParent was not assigned!");

        if (canvas != null)
            canvas.SetActive(false);

        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            objectRenderer = GetComponentInChildren<Renderer>();
            if (objectRenderer == null)
                return;
            
        }

        originalMaterials = objectRenderer.materials;

        interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.hoverEntered.AddListener(OnHoverEnter);
            interactable.hoverExited.AddListener(OnHoverExit);
        }
    }

    public void ToggleCanvas()
    {
        if (canvas == null || canvasParent == null) return;

        bool willShow = !canvas.activeSelf;

        foreach (Transform child in canvasParent)
        {
            if (child.gameObject != canvas)
                child.gameObject.SetActive(false);
        }

        canvas.SetActive(willShow);
    }

    public void OnHoverEnter(HoverEnterEventArgs args)
    {
        if (hoverMaterial != null && originalMaterials != null)
        {
            Material[] newMaterials = new Material[originalMaterials.Length];
            for (int i = 0; i < originalMaterials.Length; i++)
            {
                newMaterials[i] = hoverMaterial;
            }

            objectRenderer.materials = newMaterials;
        }
    }

    public void OnHoverExit(HoverExitEventArgs args)
    {
        if (originalMaterials != null)
            objectRenderer.materials = originalMaterials;
        
    }
}
