using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenuController : MonoBehaviour
{
    public GameObject ui;
    public GameObject off;
    public Button exit;

    private int clickNum = 0;
    private List<Toggle> btns = new List<Toggle>();

    // Start is called before the first frame update
    void Start()
    {
        Toggle[] toggles = GetComponentsInChildren<Toggle>();

        for (int i = 0; i < toggles.Length; i++)
        {
            btns.Add(toggles[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ui.activeSelf == false)
        {
            for(int i = 0; i < btns.Count; i++) //���콺�� �̵����� �� ���
            {
                if (btns[i].isOn)
                {
                    clickNum = i;
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                clickNum -= 1;
                Selected();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                clickNum += 1;
                Selected();
            }
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                buttonClicked();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                off.SetActive(true);
            }
        } else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                exit.onClick.Invoke();
            }
        }
    }

    void Selected()
    {
        if (clickNum < 0)
        {
            clickNum = 4 + clickNum;
        }

        clickNum = clickNum % 4;

        btns[clickNum].isOn = true;
        SoundManager.instance.PlaySound("cursor_move");
    }

    void buttonClicked()
    {
        GameObject obj = transform.GetChild(clickNum).gameObject;
        obj.GetComponent<TitleMenuSelect>().Selected(clickNum);
        SoundManager.instance.PlaySound("click_bobit");
    }

    public void TurnOff()
    {
        Application.Quit();
    }
}
