using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGroupLimited : MonoBehaviour
{
    public int maxToggles = 3;

    private List<Toggle> toggles = new List<Toggle>();

    private void Start()
    {
        // Find all toggles in the toggle group
        Toggle[] allToggles = GetComponentsInChildren<Toggle>();

        for (int i = 0; i < allToggles.Length; i++)
        {
            // Add the toggle to the list
            toggles.Add(allToggles[i]);

            // Add a listener for the toggle's value changed event
            allToggles[i].onValueChanged.AddListener(OnToggleValueChanged);
        }
    }

    private void OnToggleValueChanged(bool isOn)
    {
        int numTogglesOn = 0;
        for (int i = 0; i < toggles.Count; i++)
        {
            if (toggles[i].isOn)
            {
                numTogglesOn++;
            }
        }

        if (numTogglesOn > maxToggles)
        {
            // If the maximum number of toggles is exceeded, disable the last toggled checkbox
            Toggle lastToggled = null;
            for (int i = toggles.Count - 1; i >= 0; i--)
            {
                if (toggles[i].isOn)
                {
                    lastToggled = toggles[i];
                    break;
                }
            }

            if (lastToggled != null)
            {
                lastToggled.isOn = false;
            }
        }
    }
}
