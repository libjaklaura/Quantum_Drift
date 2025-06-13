using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject progressBar;
    public InputActionReference openMenuAction;

    private void OnEnable()
    {
        openMenuAction.action.Enable();
        openMenuAction.action.performed += TogglePauseMenu;
    }

     private void OnDisable()
    {
        openMenuAction.action.Disable();
        openMenuAction.action.performed -= TogglePauseMenu;
    }

    private void TogglePauseMenu(InputAction.CallbackContext context)
    {
        TogglePauseMenu();
    }

    public void TogglePauseMenu()
    {
        bool isActive = !pauseMenuPanel.activeSelf;
        pauseMenuPanel.SetActive(isActive);

        if (progressBar != null)
        {
            progressBar.SetActive(!isActive);
        } 
    }
}
