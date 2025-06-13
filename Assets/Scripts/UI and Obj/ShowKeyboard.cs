using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using UnityEngine.EventSystems;

public class ShowKeyboard : MonoBehaviour
{
    private TMP_InputField inputField;
    public Vector3 keyboardOffset = new Vector3(0, -2, 2);

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(x => OpenKeyboard());
    }

    void OnDisable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (NonNativeKeyboard.Instance != null && NonNativeKeyboard.Instance.InputField == inputField)
        {
            NonNativeKeyboard.Instance.Close();
        }
    }

    public void OpenKeyboard()
    {
        if (NonNativeKeyboard.Instance == null) return;

        Vector3 worldPos = inputField.transform.position;
        NonNativeKeyboard.Instance.transform.position = worldPos + inputField.transform.TransformDirection(keyboardOffset);
        NonNativeKeyboard.Instance.transform.rotation = transform.rotation;

        
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);
        SetCaretColorAlpha(1);

        NonNativeKeyboard.Instance.OnClosed += Instance_OnClosed;
    }

    private void Instance_OnClosed(object sender, System.EventArgs e)
    {
        SetCaretColorAlpha(0);
        NonNativeKeyboard.Instance.OnClosed -= Instance_OnClosed;
    }

    public void SetCaretColorAlpha(float value)
    {
        inputField.customCaretColor = true;
        Color caretColor = new Color(1f, 1f, 1f, Mathf.Clamp01(value));
        caretColor.a = value;
        inputField.caretColor = caretColor;

        inputField.ForceLabelUpdate();
    }
}
