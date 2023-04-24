using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleMenuController : MonoBehaviour
{
    public GameObject ui;

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
            for(int i = 0; i < btns.Count; i++) //마우스로 이동했을 시 대비
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
    }

    void buttonClicked()
    {
        GameObject obj = transform.GetChild(clickNum).gameObject;
        obj.GetComponent<TitleMenuSelect>().Selected(clickNum);
    }
}
