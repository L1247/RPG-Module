#region

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class ChatController : MonoBehaviour
{
#region Public Variables

    public Scrollbar      ChatScrollbar;
    public TMP_InputField ChatInputField;

    public TMP_Text ChatDisplayOutput;

#endregion

#region Private Methods

    private void AddToChatOutput(string newText)
    {
        // Clear Input Field
        ChatInputField.text = string.Empty;

        var timeNow = DateTime.Now;

        var formattedInput = "[<#FFFF80>" + timeNow.Hour.ToString("d2") + ":" + timeNow.Minute.ToString("d2") + ":" +
                             timeNow.Second.ToString("d2") + "</color>] " + newText;

        if (ChatDisplayOutput != null)
        {
            // No special formatting for first entry
            // Add line feed before each subsequent entries
            if (ChatDisplayOutput.text == string.Empty)
                ChatDisplayOutput.text = formattedInput;
            else
                ChatDisplayOutput.text += "\n" + formattedInput;
        }

        // Keep Chat input field active
        ChatInputField.ActivateInputField();

        // Set the scrollbar to the bottom when next text is submitted.
        ChatScrollbar.value = 0;
    }

    private void OnDisable()
    {
        ChatInputField.onSubmit.RemoveListener(AddToChatOutput);
    }

    private void OnEnable()
    {
        ChatInputField.onSubmit.AddListener(AddToChatOutput);
    }

#endregion
}