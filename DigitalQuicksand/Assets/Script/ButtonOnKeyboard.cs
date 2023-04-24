using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonOnKeyboard : MonoBehaviour
{
    public GameObject sign;
    public string btn_name;

    public GameObject ui;
    public GameObject tab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseOver()
    {
        if (ui.activeSelf == false)
        {
            sign.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }

    public void OnMouseExit()
    {
        if (ui.activeSelf == false)
        {
            sign.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
    }

    public void OnMouseDown()
    {
        if (ui.activeSelf == false)
        {
            if (btn_name == "start")
            {
                SceneManager.LoadScene("Prologue");
            }
            else
            {
                ui.SetActive(true);
                tab.SetActive(true);
            }
        }
    }
}
