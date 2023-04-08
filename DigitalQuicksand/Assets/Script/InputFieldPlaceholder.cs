using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InputFieldPlaceholder : MonoBehaviour, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    public TMP_InputField inputField;

    private string placeholderText;

    private void Awake()
    {
        // Save the placeholder text
        placeholderText = inputField.placeholder.GetComponent<TMP_Text>().text;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // If the input field is empty, hide the placeholder text
        if (string.IsNullOrEmpty(inputField.text))
        {
            inputField.placeholder.GetComponent<TMP_Text>().text = "";
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        // If the input field is empty, hide the placeholder text
        if (string.IsNullOrEmpty(inputField.text))
        {
            inputField.placeholder.GetComponent<TMP_Text>().text = "";
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        // If the input field is empty, show the placeholder text
        if (string.IsNullOrEmpty(inputField.text))
        {
            inputField.placeholder.GetComponent<TMP_Text>().text = placeholderText;
        }
    }
}
