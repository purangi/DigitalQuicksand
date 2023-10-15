using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPages : MonoBehaviour
{
    public GameObject setting;
    public GameObject save;
    public GameObject ending;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToNext()
    {
        if(setting.activeSelf == true)
        {
            setting.SetActive(false);
            save.SetActive(true);
        } else if(save.activeSelf == true) 
        {
            save.SetActive(false);
            ending.SetActive(true);
        } else if(ending.activeSelf == true)
        {
            ending.SetActive(false);
            setting.SetActive(true);
        } else
        {
            Debug.Log("TitleMenu Popups button error");
        }
    }

    public void ToPrev()
    {
        if (setting.activeSelf == true)
        {
            setting.SetActive(false);
            ending.SetActive(true);
        }
        else if (save.activeSelf == true)
        {
            save.SetActive(false);
            setting.SetActive(true);
        }
        else if (ending.activeSelf == true)
        {
            ending.SetActive(false);
            save.SetActive(true);
        }
        else
        {
            Debug.Log("TitleMenu Popups button error");
        }
    }
}
