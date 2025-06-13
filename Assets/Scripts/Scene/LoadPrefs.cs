using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class LoadPrefs : MonoBehaviour
{
    [Header("Movement & Rotation")]
    public ActionBasedContinuousMoveProvider moveProvider;
    public ActionBasedContinuousTurnProvider turnProvider;

    [Header("Controllers")]
    public GameObject leftControllerRay;
    public GameObject leftControllerDirect;
    public GameObject rightControllerRay;
    public GameObject rightControllerDirect;

    void OnEnable()
    {
        ApplyAllSettings();
    }

    public void ApplyAllSettings()
    {
        // Movement Speed
        if (moveProvider != null)
        {
            float moveSpeed = PlayerPrefs.GetFloat("MovementSpeed", 1f);
            moveProvider.moveSpeed = moveSpeed;
        }

        // Rotation Speed
        if (turnProvider != null)
        {
            float rotationSpeed = PlayerPrefs.GetFloat("RotationSpeed", 100f);
            turnProvider.turnSpeed = rotationSpeed;
        }

        // Language
        int langIndex = PlayerPrefs.GetInt("Language", 0); // 0 = English, 1 = Turkish
        string localeCode = langIndex == 0 ? "en" : "sk";
        StartCoroutine(SetLanguage(localeCode));

        // Raycast Hand Selection
        int raycastHand = PlayerPrefs.GetInt("RaycastHand", 0); // 0 = Left Ray, 1 = Right Ray

        bool useLeftRay = raycastHand == 0;

        if (leftControllerRay && leftControllerDirect)
        {
            leftControllerRay.SetActive(useLeftRay);
            leftControllerDirect.SetActive(!useLeftRay);
        }

        if (rightControllerRay && rightControllerDirect)
        {
            rightControllerRay.SetActive(!useLeftRay);
            rightControllerDirect.SetActive(useLeftRay);
        }

        Debug.Log("Settings applied in scene: " + SceneManager.GetActiveScene().name);
    }

    System.Collections.IEnumerator SetLanguage(string code)
    {
        yield return LocalizationSettings.InitializationOperation;
        var locale = LocalizationSettings.AvailableLocales.Locales.Find(l => l.Identifier.Code == code);
        if (locale != null)
            LocalizationSettings.SelectedLocale = locale;
    }
}
