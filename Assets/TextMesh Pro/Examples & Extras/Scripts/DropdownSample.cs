#region

using TMPro;
using UnityEngine;

#endregion

public class DropdownSample : MonoBehaviour
{
#region Private Variables

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private TMP_Dropdown dropdownWithoutPlaceholder;

    [SerializeField]
    private TMP_Dropdown dropdownWithPlaceholder;

#endregion

#region Public Methods

    public void OnButtonClick()
    {
        text.text = dropdownWithPlaceholder.value > -1
                        ? "Selected values:\n" + dropdownWithoutPlaceholder.value + " - " + dropdownWithPlaceholder.value
                        : "Error: Please make a selection";
    }

#endregion
}