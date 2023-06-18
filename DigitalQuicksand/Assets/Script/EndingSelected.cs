using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndingSelected : MonoBehaviour
{
    public Scrollbar scroll;
    private List<GameObject> groups = new List<GameObject>();
    private List<Toggle> ending_files = new List<Toggle>();
    private int clickNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;
            groups.Add(go);
        }

        for(int i = 0; i < groups.Count; i++)
        {
            Toggle[] toggle = groups[i].GetComponentsInChildren<Toggle>();
            for(int j = 0; j < toggle.Length; j++)
            {
                ending_files.Add(toggle[j]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ending_files.Count; i++) //마우스로 이동했을 시 대비
        {
            if (ending_files[i].isOn)
            {
                clickNum = i;
            }
        }
        if(clickNum < 12)
        {
            scroll.value = 0f;
        }
        else if(clickNum >= 12 && clickNum < 24)
        {
            scroll.value = 0.594f;
        } else if(clickNum >= 24)
        {
            scroll.value = 1f;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(clickNum != 30)
            {
                if(clickNum % 2 == 1)
                {
                    ending_files[clickNum - 1].isOn = true;
                } 
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) //scrollview 이동 필요
        {
            if(clickNum > 1)
            {
                ending_files[clickNum - 2].isOn = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (clickNum != 30)
            {
                if (clickNum % 2 == 0)
                {
                    ending_files[clickNum + 1].isOn = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            if (clickNum < 29)
            {
                ending_files[clickNum + 2].isOn = true;
            } else if(clickNum == 29)
            {
                ending_files[30].isOn = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            buttonClicked();
        }
    }

    void buttonClicked()
    {
        //선택 연결
    }
}
