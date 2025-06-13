using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SettingsManager : MonoBehaviour
{
    [Header("Sliders")]
    public Slider movementSpeedSlider;
    public Slider rotationSpeedSlider;

    [Header("Language Toggle Group")]
    public ToggleGroup languageToggleGroup;

    [Header("Raycast Hand Toggle Group")]
    public ToggleGroup raycastHandToggleGroup;

    void OnEnable()
    {
        LoadSettings();
    }

    public void ApplySettings()
    {
        PlayerPrefs.SetFloat("MovementSpeed", movementSpeedSlider.value);
        PlayerPrefs.SetFloat("RotationSpeed", rotationSpeedSlider.value);

        int languageIndex = GetSelectedToggleIndex(languageToggleGroup);
        PlayerPrefs.SetInt("Language", languageIndex);

        int handIndex = GetSelectedToggleIndex(raycastHandToggleGroup);
        PlayerPrefs.SetInt("RaycastHand", handIndex);

        PlayerPrefs.Save();
        Debug.Log("Settings applied.");
    }

    void LoadSettings()
    {
        movementSpeedSlider.value = PlayerPrefs.GetFloat("MovementSpeed", 1f);
        rotationSpeedSlider.value = PlayerPrefs.GetFloat("RotationSpeed", 100f);

        int savedLanguageIndex = PlayerPrefs.GetInt("Language", 0);
        SetToggleGroupSelection(languageToggleGroup, savedLanguageIndex);

        int savedHandIndex = PlayerPrefs.GetInt("RaycastHand", 0);
        SetToggleGroupSelection(raycastHandToggleGroup, savedHandIndex);
        Debug.Log(savedHandIndex);
    }

    int GetSelectedToggleIndex(ToggleGroup group)
    {
        var toggles = group.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
                return i;
        }
        return 0;
    }

    void SetToggleGroupSelection(ToggleGroup group, int index)
    {
        var toggles = group.GetComponentsInChildren<Toggle>();
        foreach (var toggle in toggles)
        {
            toggle.isOn = false;
        }
        
        if (index >= 0 && index < toggles.Length)
        {
            toggles[index].isOn = true;
        }     
    }
}
