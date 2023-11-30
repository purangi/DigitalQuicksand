using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleMenuSelect : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int btn_num;
    public GameObject bg;
    public GameObject ui;
    private Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Selected(int num)
    {
        if(toggle.isOn)
        {
            if (num == 0)
            {
                SceneManager.LoadScene("Prologue");
                SoundManager.instance.BGMChange("Prologue");
            }
            else if (num < 4)
            {
                if(bg != null && ui != null)
                {
                    bg.SetActive(true);
                    ui.SetActive(true);
                }
            }
            else
            {
                Debug.Log("TitleMenu btn_name error");
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Selected(btn_num);
        SoundManager.instance.PlaySound("click_bobit");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toggle.isOn = true;
        SoundManager.instance.PlaySound("cursor_move");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toggle.isOn = false;
    }
}
