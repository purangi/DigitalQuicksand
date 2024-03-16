using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WindowMode : MonoBehaviour
{
    public TMP_Dropdown m_Dropdown;

    // Start is called before the first frame update
    void Start()
    {
        if(Screen.fullScreen == true)
        {
            m_Dropdown.value = 0;
        } else
        {
            m_Dropdown.value = 1;
        }

        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(m_Dropdown);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DropdownValueChanged(TMP_Dropdown change)
    {
        switch(change.value)
        {
            case 0:
                //full_screen
                Screen.SetResolution(1920, 1080, true);
                break;
            case 1:
                Screen.SetResolution(1600, 900, false);
                break;
        }
    }
}
