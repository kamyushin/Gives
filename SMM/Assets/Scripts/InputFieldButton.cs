using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldButton : Button
{

    public InputField inputField
    {
        get
        {
            if (inputField_ == null)
            {
                inputField_ = GetComponentInParent<InputField>();
            }
            return inputField_;
        }
    }
    private InputField inputField_;

    private TouchScreenKeyboard keyboard_ = null;
    private string originalText_ = "";
    private bool wasCanceled_ = false;

    void CreateKeyboard()
    {
        if (keyboard_ != null) return;
        if (!IsInteractable()) return;

        if (!inputField.textComponent || inputField.textComponent.font == null)
            return;

        originalText_ = inputField.text;
        keyboard_ = TouchScreenKeyboard.Open(inputField.text, TouchScreenKeyboardType.Default, true);
    }

    void DestroyKeyboard()
    {
        if (!inputField.textComponent || !inputField.IsInteractable())
            return;

        if (wasCanceled_)
        {
            inputField.text = originalText_;
            wasCanceled_ = false;
        }

        if (keyboard_ != null)
        {
            keyboard_.active = false;
            keyboard_ = null;
            originalText_ = "";
        }
    }

    void LateUpdate()
    {
        if (keyboard_ == null) return;

        inputField.text = keyboard_.text;

        if (keyboard_.wasCanceled)
        {
            wasCanceled_ = true;
            DestroyKeyboard();
        }

        if (keyboard_.done)
        {
            DestroyKeyboard();
        }
    }

    public override void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        CreateKeyboard();
        base.OnPointerDown(eventData);
    }

    public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        CreateKeyboard();
        base.OnPointerClick(eventData);
    }
}
